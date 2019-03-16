using UnityEngine;
using UnityEditor;
using M.S.Project;

[InitializeOnLoad]
public class PackageLauncher {


    static PackageLauncher()
    {

        if (EditorApplication.isPlayingOrWillChangePlaymode){
            return;
        }

        PackageManager.Create();
    }



}
