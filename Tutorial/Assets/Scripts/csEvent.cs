using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class csEvent : MonoBehaviour
{
    public AnchorBehaviour[] anchors;
    
    int index = 0;
    bool flag = true;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            GetComponent<ContentPositioningBehaviour>().DuplicateStage = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            GetComponent<ContentPositioningBehaviour>().DuplicateStage = false;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            GetComponent<ContentPositioningBehaviour>().AnchorStage = anchors[++index % anchors.Length];
        }
    }

    public void HitTest(HitTestResult result) {
        Debug.Log("HitTest");

        if(flag) {
            GetComponent<ContentPositioningBehaviour>().AnchorStage = anchors[0];
        } else {
            GetComponent<ContentPositioningBehaviour>().AnchorStage = anchors[1];
        }

        GetComponent<ContentPositioningBehaviour>().PositionContentAtPlaneAnchor(result);
        flag = !flag;
    }
}
