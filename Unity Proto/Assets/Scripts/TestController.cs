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
        GCHandle pixelHandle;
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
            Debug.Log("testPtr" + testPtr.ToString());
            // NativeAdapter._TestMat(rawImageTexture.width, rawImageTexture.height, pixelPtr);
            // NativeAdapter._TestMat(rawImageTexture.width, rawImageTexture.height, pixelPtr);
            // rawImageTexture.SetPixels32(pixels);
            // rawImageTexture.Apply();

            // NativeAdapter._FlipImage(ref pixels, rawImageTexture.width, rawImageTexture.height);

            rawImageTexture.SetPixels32(pixels);
            rawImageTexture.Apply();
            InImage.texture = rawImageTexture;

            int w = NativeAdapter.PicBufferRows();
            int h = NativeAdapter.PicBufferCols();

            Debug.Log($"Result w: {w} h: {h}");
            txt.text = $"Result w: {w} h: {h}";

            Texture2D resultTexture = new Texture2D(w, h, TextureFormat.ARGB32, false);

            int bufferSize = w * h * 4;
            byte[] rawData = new byte[bufferSize];

            if (pixelPtr != IntPtr.Zero)
            {
                Marshal.Copy(pixelPtr, rawData, 0, bufferSize);

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
