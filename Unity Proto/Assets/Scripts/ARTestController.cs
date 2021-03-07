﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.XR.ARSubsystems;

public class ARTestController : MonoBehaviour
{
    public ARCameraBackground arCam;
    public ARCameraManager arCamManager;
    public RawImage InImage;
    public RawImage OutImage;
    public Text txt;

    Texture2D texture2d;
    RenderTexture renderTexture;
    XRCpuImage xrCpuImage;
    // Start is called before the first frame update
    void Start()
    {
        arCamManager.frameReceived += OnFrameReceived;
    }

    // Update is called once per frame
    void Update()
    {
        try {
            // Graphics.Blit(null, renderTexture, arCam.material);
 
            // Copy the RenderTexture from GPU to CPU
            // var activeRenderTexture = RenderTexture.active;
            // RenderTexture.active = renderTexture;
            // if (!arCamManager.TryAcquireLatestCpuImage(out xrCpuImage)) {
            //     throw new System.Exception("Failed to get TryAcquireLatestCpuImage.");
            // }
            
            // if (xrCpuImage == null) {
            //     throw new System.Exception("xrCpuImage is null.");
            // }

            // if (renderTexture == null) {
            //     throw new System.Exception("renderTexture is null.");
            // }
            // if (texture2d == null)
            //     texture2d = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            // texture2d.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            // texture2d.Apply();
            // RenderTexture.active = activeRenderTexture;
            // // this.texture2d = GetRTPixels(renderTexture);
            // InImage.texture = this.texture2d;
        } catch (System.Exception e) {
            txt.text = e.Message;
        } finally {
            Resources.UnloadUnusedAssets();
        }
    }

    void OnFrameReceived(ARCameraFrameEventArgs eventArgs)
    {
        if (arCamManager.TryAcquireLatestCpuImage(out XRCpuImage xrCpuImage))
        {
            using (xrCpuImage)
            {
                try {
                    unsafe {
                        if (xrCpuImage == null) {
                            throw new System.Exception("xrCpuImage is null.");
                        }

                        var conversionParams = new XRCpuImage.ConversionParams
                        {
                            // Get the entire image.
                            inputRect = new RectInt(0, 0, xrCpuImage.width, xrCpuImage.height),

                            // Downsample by 2.
                            outputDimensions = new Vector2Int(xrCpuImage.width, xrCpuImage.height),

                            // Choose RGBA format.
                            outputFormat = TextureFormat.ARGB32,

                            // Flip across the vertical axis (mirror image).
                            transformation = XRCpuImage.Transformation.MirrorY
                        };

                        // See how many bytes you need to store the final image.
                        int size = xrCpuImage.GetConvertedDataSize(conversionParams);

                        // Allocate a buffer to store the image.
                        var buffer = new NativeArray<byte>(size, Allocator.Temp);

                        // Extract the image data
                        xrCpuImage.Convert(conversionParams, new IntPtr(buffer.GetUnsafePtr()), buffer.Length);

                        // The image was converted to RGBA32 format and written into the provided buffer
                        // so you can dispose of the XRCpuImage. You must do this or it will leak resources.

                        // At this point, you can process the image, pass it to a computer vision algorithm, etc.
                        // In this example, you apply it to a texture to visualize it.

                        // You've got the data; let's put it into a texture so you can visualize it.
                        texture2d = new Texture2D(
                            conversionParams.outputDimensions.x,
                            conversionParams.outputDimensions.y,
                            conversionParams.outputFormat,
                            false);

                        texture2d.LoadRawTextureData(buffer);
                        texture2d.Apply();

                        texture2d = rotateTexture(texture2d, false);

                        // Done with your temporary data, so you can dispose it.
                        buffer.Dispose();

                        InImage.texture = this.texture2d;
                        txt.text = $"w: {xrCpuImage.width} h: {xrCpuImage.height} wt: {texture2d.width} ht: {texture2d.height}";
                    }
                } catch (System.Exception e) {
                    txt.text = e.Message;
                } finally {
                    Resources.UnloadUnusedAssets();
                }
            }
        }
        else
        {
            Debug.Log($"{Time.realtimeSinceStartup} Could not acquire cpu image.");
        }
    }

    Texture2D rotateTexture(Texture2D originalTexture, bool clockwise)
    {
        Color32[] original = originalTexture.GetPixels32();
        Color32[] rotated = new Color32[original.Length];
        int w = originalTexture.width;
        int h = originalTexture.height;

        int iRotated, iOriginal;

        for (int j = 0; j < h; ++j)
        {
            for (int i = 0; i < w; ++i)
            {
                iRotated = (i + 1) * h - j - 1;
                iOriginal = clockwise ? original.Length - 1 - (j * w + i) : j * w + i;
                rotated[iRotated] = original[iOriginal];
            }
        }

        Texture2D rotatedTexture = new Texture2D(h, w);
        rotatedTexture.SetPixels32(rotated);
        rotatedTexture.Apply();
        return rotatedTexture;
    }

    public static Texture2D GetRTPixels(RenderTexture rt) {

        // Remember currently active render texture
        RenderTexture currentActiveRT = RenderTexture.active;

        // Set the supplied RenderTexture as the active one
        RenderTexture.active = rt;

        // Create a new Texture2D and read the RenderTexture image into it
        Texture2D tex = new Texture2D(rt.width, rt.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);

        // Restorie previously active render texture
        RenderTexture.active = currentActiveRT;
        return tex;
    }
}