using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.InteropServices;

public class TestController : MonoBehaviour
{
    public Text txt, txt2;
    public RawImage InImage;
    public RawImage OutImage;
    // Start is called before the first frame update
    void Start()
    {
        txt.text = "Loading native lib";
        txt2.text = NativeAdapter.dllPath;
        GCHandle pixelHandle, resultPixelHandle;
        try {
            txt.text = "Native: " + NativeAdapter.FooTest().ToString();

            Texture2D rawImageTexture = (Texture2D)InImage.texture;
            Color32[] pixels = rawImageTexture.GetPixels32();
            Debug.Log("Pixels: " + pixels.Length.ToString());

            pixelHandle = GCHandle.Alloc(pixels, GCHandleType.Pinned);
            Debug.Log("Addr: " + pixelHandle.ToString());
            IntPtr pixelPtr = pixelHandle.AddrOfPinnedObject();
            Debug.Log("pixelPtr" + pixelPtr.ToString());
            IntPtr testPtr = NativeAdapter.PicFromDoc(rawImageTexture.width, rawImageTexture.height, pixelPtr);
            Debug.Log("pixelPtr" + pixelPtr.ToString());
            Debug.Log("testPtr" + testPtr.ToString() + " test2: " + NativeAdapter._GetResultPicBuffer());
            // NativeAdapter._TestMat(rawImageTexture.width, rawImageTexture.height, pixelPtr);
            // NativeAdapter._TestMat(rawImageTexture.width, rawImageTexture.height, pixelPtr);
            // rawImageTexture.SetPixels32(pixels);
            // rawImageTexture.Apply();

            // NativeAdapter._FlipImage(ref pixels, rawImageTexture.width, rawImageTexture.height);

            rawImageTexture.SetPixels32(pixels);
            rawImageTexture.Apply();
            InImage.texture = rawImageTexture;

            int nativeH = NativeAdapter.PicBufferRows();
            int nativeW = NativeAdapter.PicBufferCols();
            int w = rawImageTexture.width;
            int h = rawImageTexture.height;
            w = nativeW;
            h = nativeH;

            Debug.Log($"Result w: {w} h: {h} nativeW: {nativeW} nativeH: {nativeH}");
            txt.text = $"Result w: {w} h: {h} nativeW: {nativeW} nativeH: {nativeH}";

            Texture2D resultTexture = new Texture2D(w, h, TextureFormat.ARGB32, false);
            // Color32[] resultPixels = resultTexture.GetPixels32();
            // resultPixelHandle = GCHandle.Alloc(resultPixels, GCHandleType.Pinned);
            // IntPtr resultPixelPtr = resultPixelHandle.AddrOfPinnedObject();
            // NativeAdapter._ReturnGlobalMat(resultPixelPtr);
            // resultTexture.SetPixels32(resultPixels);
            // resultTexture.Apply();

            int bufferSize = w * h * 4;
            byte[] rawData = new byte[bufferSize];

            if (testPtr != IntPtr.Zero)
            {
                Marshal.Copy(testPtr, rawData, 0, bufferSize);

                resultTexture.LoadRawTextureData(rawData);
                resultTexture.Apply();
            }

            OutImage.texture = resultTexture;

        } catch (System.Exception e) {
            txt.text = e.Message;
            Debug.Log(e);
        } finally {
            if (pixelHandle != null) {
                pixelHandle.Free();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
