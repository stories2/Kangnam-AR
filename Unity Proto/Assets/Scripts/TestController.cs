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
            // IntPtr results = NativeAdapter.PicFromDoc(rawImageTexture.width, rawImageTexture.height, ref pixels);
            // IntPtr results = NativeAdapter._TestMat(rawImageTexture.width, rawImageTexture.height, pixelHandle.AddrOfPinnedObject());
            NativeAdapter._FlipImage(ref pixels, rawImageTexture.width, rawImageTexture.height);

            rawImageTexture.SetPixels32(pixels);
            rawImageTexture.Apply();

            Debug.Log("Result w: " + NativeAdapter.PicBufferRows().ToString() + " h: " + NativeAdapter.PicBufferCols().ToString());
            txt.text = "Result w: " + NativeAdapter.PicBufferRows().ToString() + " h: " + NativeAdapter.PicBufferCols().ToString();

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
