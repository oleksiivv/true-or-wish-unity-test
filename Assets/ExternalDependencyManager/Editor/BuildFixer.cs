using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System;
 using UnityEditor;
 using UnityEngine;

[InitializeOnLoad]
public static class BuildFixer
{
    static BuildFixer(){
        string newJDKPath = EditorApplication.applicationPath.Replace("Unity.exe", "Data/PlaybackEngines/AndroidPlayer/OpenJDK");
 
        if (Environment.GetEnvironmentVariable("JAVA_HOME") != newJDKPath)
        {
            Environment.SetEnvironmentVariable("JAVA_HOME", newJDKPath);
        }
    }
}
