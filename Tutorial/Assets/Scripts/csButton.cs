using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.Video;

public class csButton : MonoBehaviour, IVirtualButtonEventHandler
{
    public GameObject quad;
    VideoPlayer vp;
    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        // throw new System.NotImplementedException();
        if (vp.isPlaying) {
            vp.Pause();
        } else {
            vp.Play();
        }
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        // throw new System.NotImplementedException();
        Debug.Log("Btn released" + vb.name);
    }

    // Start is called before the first frame update
    void Start()
    {
        vp = quad.GetComponent<VideoPlayer>();
        VirtualButtonBehaviour[] vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; i ++) {
            vbs[i].RegisterEventHandler(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
