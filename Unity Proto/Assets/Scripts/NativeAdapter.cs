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

        [DllImport ("OpenCVPlugin")]
        private static extern void FlipImage(ref Color32[] rawImage, int width, int height);

        [DllImport ("OpenCVPlugin")]
        private static extern void ReturnGlobalMat(IntPtr data);
    #elif UNITY_EDITOR
        [DllImport ("UnityPlugin")]
        private static extern int FooTestFunction_Internal();

        [DllImport ("UnityPlugin")]
        private static extern int ResultPicBufferRows();

        [DllImport ("UnityPlugin")]
        private static extern int ResultPicBufferCols();

        [DllImport ("UnityPlugin")]
        private static extern IntPtr ExportPicFromDoc(int width, int height, IntPtr bufferAddr);

        [DllImport ("UnityPlugin")]
        private static extern void TestMat(int width, int height, IntPtr bufferAddr);

        [DllImport ("UnityPlugin")]
        private static extern void FlipImage(ref Color32[] rawImage, int width, int height);

        [DllImport ("UnityPlugin")]
        private static extern void ReturnGlobalMat(IntPtr data);

        [DllImport ("UnityPlugin")]
        private static extern IntPtr GetResultPicBuffer();
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

    public static int PicBufferRows() {
        return ResultPicBufferRows();
    }

    public static int PicBufferCols() {
        return ResultPicBufferCols();
    }

    public static IntPtr PicFromDoc(int width, int height, IntPtr bufferAddr) {
        return ExportPicFromDoc(width, height, bufferAddr);
    }

    public static void _TestMat(int width, int height, IntPtr bufferAddr) {
        #if UNITY_EDITOR
            TestMat(width, height, bufferAddr);
        #endif
    }

    public static void _FlipImage(ref Color32[] rawImage, int width, int height) {
        FlipImage(ref rawImage, width, height);
    }

    public static void _ReturnGlobalMat(IntPtr bufferAddr) {
        ReturnGlobalMat(bufferAddr);
    }

    public static IntPtr _GetResultPicBuffer() {
        #if UNITY_EDITOR
            return GetResultPicBuffer();
        #else
            return IntPtr.Zero;
        #endif
    }
}
