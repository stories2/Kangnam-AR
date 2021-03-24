using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csWebCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        WebCamTexture web = new WebCamTexture(1280, 720, 60);
        GetComponent<MeshRenderer>().material.mainTexture = web;
        web.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
