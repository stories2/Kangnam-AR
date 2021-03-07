using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARTestController : MonoBehaviour
{
    public ARCameraBackground arCam;
    Texture2D texture2d;
    RenderTexture renderTexture;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Graphics.Blit(null, renderTexture, arCam.material);
        this.texture2d = GetRTPixels(renderTexture);
        Resources.UnloadUnusedAssets();
    }

    Texture2D GetRTPixels(RenderTexture rt) {

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
