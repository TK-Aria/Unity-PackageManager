#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

using System;
using UnityEngine;


[Serializable][CreateAssetMenu(menuName="ScriptableObject/PackageModel")]
public class PackageModel : ScriptableObject
{

    #region Type

    [Serializable]
    public class Package
    {
        public string name;
        public string downloadUrl;
    }

    [Serializable]
    public class PackageList
    {
        public Package[] packages = new Package[] { };
    }

    #endregion // Type End.

    #region Field

    [Header("PackageInfo")]
    public PackageList packageList = new PackageList();

    #endregion // Field End.


    public static PackageModel Create()
    {
        return ScriptableObject.CreateInstance<PackageModel>();
    }
    
    public static PackageModel LoadRuntime(string path)
    {
        return Resources.Load<PackageModel>("PackageModel/" + path);
    }

    public static PackageModel Instantiate(string path)
    {
        return UnityEngine.Object.Instantiate<PackageModel>(LoadRuntime(path));
    }
    
    #if UNITY_EDITOR
    public static PackageModel LoadEditor(string path)
    {
        return AssetDatabase.LoadAssetAtPath<PackageModel>("PackageModel/" + path);
    }
    #endif // UNITY_EDITOR
    
}


