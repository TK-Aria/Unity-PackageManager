using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

namespace M.S.Project
{

    [CreateAssetMenu(menuName = "ScriptableWizard/PackageManager")]
    public class PackageManager : CodeTemplates.UnitySerializeWindow
    {

        private const string editorConfAssetName = "Assets/bin/PackageManagerEditorConf.asset";

        #region Type

        [System.Serializable]
        public class PackageView
        {
            public bool isCheck;
            public string name;
            public string downloadUrl { get; set; }
        }

        #endregion // Type End.

        #region Field
        [SerializeField]
        [Header("PackageManager Settings")]
        [Space(16)]
        private AssetOperation.FileDownloader fileDownloader;
        [SerializeField]
        private PackageModel packageModel;

        [SerializeField][HideInInspector]
        [Header("Packages")]
        [Space(16)]
        private PackageView[] packageView = new PackageView[] { };

        #endregion // Field End.


        [MenuItem("M.S-Project/PackageManager")]
        public static void Create()
        {

            var editorConf = new EditorConf()
            {
                editorTitle = "M.S-Project PackageManager",
                editorCaption = "",
                editorVersion = 1.01,
                footerButtons = new FooterButton[]{

                    new FooterButton(){
                        name = "Update"
                    }
                }
            };
            //var editorConf = AssetDatabase.LoadAssetAtPath<EditorConf>(editorConfAssetName);

            var instance = ShowDisplay(typeof(PackageManager), editorConf) as PackageManager;
            editorConf.footerButtons[0].onClickHandler += instance.OnClickInstallButton;
            instance.editorConf = editorConf;

        }

        public override void OnOpen()
        {

            fileDownloader.ConnectDownloadCompletedHandler = OnCompletedDownload;

            System.Array.Resize<PackageView>(ref packageView, packageModel.packageList.packages.Length);
            int index = 0;
            foreach (var content in packageModel.packageList.packages)
            {
                packageView[index] = new PackageView()
                {
                    name = content.name,
                    downloadUrl = content.downloadUrl,
                    isCheck = false,
                };
                index++;
            }

        }

        public override void OnClose()
        {

        }

        public override void OnUpdate()
        {
            int index = 0;
            foreach(var packge in packageView)
            {
                if(packge.isCheck){
                    fileDownloader.Download(packge.downloadUrl);
                    packageView[index].isCheck = false;
                }
                index++;
            }

        }

        public override void OnRender()
        {

            int index = 0;
            foreach(var package in packageView)
            {
                packageView[index].isCheck = EditorGUILayout.ToggleLeft(package.name, package.isCheck);
                index++;
            }
        }

        protected virtual void OnCompletedDownload(AssetOperation.FileDownloader.DownloadInfo downloadInfo)
        {
            AssetDatabase.ImportPackage(downloadInfo.storeFilePath, true);
        }

        public void OnClickInstallButton()
        {

            System.Array.Resize<PackageView>(ref packageView, packageModel.packageList.packages.Length);
            int index = 0;
            foreach (var content in packageModel.packageList.packages)
            {
                packageView[index] = new PackageView()
                {
                    name = content.name,
                    downloadUrl = content.downloadUrl,
                    isCheck = false,
                };
                index++;
            }
        }

    }

}
