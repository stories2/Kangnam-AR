using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine;
using System.IO;

public class NativeAdapter
{
    #if !UNITY_EDITOR
        [DllImport("OpenCVPlugin")]
        private static extern int FooTestFunction_Internal();

        [DllImport("OpenCVPlugin")]
        private static extern int ResultPicBufferRows();

        [DllImport("OpenCVPlugin")]
        private static extern int ResultPicBufferCols();

        [DllImport("OpenCVPlugin")]
        private static extern IntPtr ExportPicFromDoc(int width, int height, IntPtr bufferAddr);
    #elif UNITY_EDITOR
        [DllImport ("UnityPlugin")]
        private static extern int FooTestFunction_Internal();

        [DllImport ("UnityPlugin")]
        private static extern int ResultPicBufferRows();

        [DllImport ("UnityPlugin")]
        private static extern int ResultPicBufferCols();

        [DllImport ("UnityPlugin")]
        private static extern IntPtr ExportPicFromDoc(int width, int height, IntPtr bufferAddr);
    #endif

    public static string dllPath;

    static NativeAdapter() {
        string currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
        dllPath = Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar + "Plugins";
        #if UNITY_ANDROID
            dllPath = Application.dataPath + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar  + "Assets" + Path.DirectorySeparatorChar + "Plugins";
        #endif
        Debug.Log("dllPath " + dllPath);
        Debug.Log("currentPath " + currentPath);
        Debug.Log("process " + EnvironmentVariableTarget.Process);

        if(currentPath.Contains(dllPath) == false)
        {
            #if !UNITY_EDITOR
                string[] dict = new string[]{
                    "Assets" + Path.DirectorySeparatorChar + "Plugins",
                    "Assets" + Path.DirectorySeparatorChar + "Plugins" + Path.DirectorySeparatorChar + "lib",
                    Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar + "Plugins",
                    Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar + "Plugins" + Path.DirectorySeparatorChar + "lib",
                    Application.dataPath,
                    Application.dataPath + Path.DirectorySeparatorChar + "lib",
                    Application.dataPath + Path.DirectorySeparatorChar + "Assets",
                    Application.dataPath + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar  + "Assets",
                    Application.dataPath + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar  + "Assets" + Path.DirectorySeparatorChar + "Plugins",
                    Application.dataPath + Path.DirectorySeparatorChar + "Plugins",
                    Application.dataPath + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar + "Plugins",
                    Application.dataPath.Replace("/base.apk", ""),
                    Application.dataPath.Replace("/base.apk", "") + Path.DirectorySeparatorChar + "lib",
                    Application.dataPath.Replace("/base.apk", "") + Path.DirectorySeparatorChar + "Assets",
                    Application.dataPath.Replace("/base.apk", "") + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar  + "Assets",
                    Application.dataPath.Replace("/base.apk", "") + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar + "Plugins",
                    Application.dataPath.Replace("/base.apk", "") + Path.DirectorySeparatorChar + "Assets" + Path.DirectorySeparatorChar  + "Assets" + Path.DirectorySeparatorChar + "Plugins",
                    Application.dataPath.Replace("/base.apk", "") + Path.DirectorySeparatorChar + "Plugins",
                };
                foreach(string path in dict) {
                    Environment.SetEnvironmentVariable("PATH", currentPath + Path.PathSeparator + path, EnvironmentVariableTarget.Process);
                    currentPath = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Process);
                }
                Debug.Log("env " + Environment.GetEnvironmentVariable("PATH"));
            #elif UNITY_EDITOR
                Environment.SetEnvironmentVariable("PATH", currentPath + Path.PathSeparator + dllPath, EnvironmentVariableTarget.Process);
                Debug.Log("env " + Environment.GetEnvironmentVariable("PATH"));
            #endif
        }
    }

    public static int FooTest() {
        return FooTestFunction_Internal();
    }
}
