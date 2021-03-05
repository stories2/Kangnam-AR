using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine;
using System.IO;

public class NativeAdapter
{
    #if !UNITY_EDITOR
        [DllImport("native-lib")]
        private static extern int FooTestFunction_Internal();

        [DllImport("native-lib")]
        private static extern int ResultPicBufferRows();

        [DllImport("native-lib")]
        private static extern int ResultPicBufferCols();

        [DllImport("native-lib")]
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
        Debug.Log("dllPath " + dllPath);
        Debug.Log("currentPath " + currentPath);
        Debug.Log("process " + EnvironmentVariableTarget.Process);
        if(currentPath.Contains(dllPath) == false)
        {
            #if !UNITY_EDITOR
                Environment.SetEnvironmentVariable("PATH", currentPath + Path.PathSeparator + dllPath, EnvironmentVariableTarget.Process);
            #elif UNITY_EDITOR
                Environment.SetEnvironmentVariable("PATH", currentPath + Path.PathSeparator + dllPath, EnvironmentVariableTarget.Process);
            #endif
        }
        Debug.Log("env " + Environment.GetEnvironmentVariable("PATH"));
    }

    public static int FooTest() {
        return FooTestFunction_Internal();
    }
}
