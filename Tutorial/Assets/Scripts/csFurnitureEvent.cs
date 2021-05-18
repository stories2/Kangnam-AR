using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class csFurnitureEvent : MonoBehaviour
{
    public GameObject preGroundPlane;
    public GameObject preObj;
    public GameObject[] preObjs;
    public Texture[] btns;

    bool flag = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            GameObject plane = Instantiate(preGroundPlane);
            GameObject obj = Instantiate(preObj, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(plane.transform, true);
            obj.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            GetComponent<ContentPositioningBehaviour>().AnchorStage = plane.GetComponent<AnchorBehaviour>();
        }

        // if (Input.GetKeyDown(KeyCode.LeftArrow)) {
        //     GetComponent<ContentPositioningBehaviour>().AnchorStage.transform.GetChild(0).Translate(0.005f, 0, 0, GetComponent<ContentPositioningBehaviour>().AnchorStage.transform);
        // }
        // if (Input.GetKeyDown(KeyCode.RightArrow)) {
        //     GetComponent<ContentPositioningBehaviour>().AnchorStage.transform.GetChild(0).Translate(-0.005f, 0, 0, GetComponent<ContentPositioningBehaviour>().AnchorStage.transform);
        // }
        // if (Input.GetKeyDown(KeyCode.UpArrow)) {
        //     GetComponent<ContentPositioningBehaviour>().AnchorStage.transform.GetChild(0).Translate(0, 0, -0.005f, GetComponent<ContentPositioningBehaviour>().AnchorStage.transform);
        // }
        // if (Input.GetKeyDown(KeyCode.DownArrow)) {
        //     GetComponent<ContentPositioningBehaviour>().AnchorStage.transform.GetChild(0).Translate(0, 0, 0.005f, GetComponent<ContentPositioningBehaviour>().AnchorStage.transform);
        // }
    }

    void AddObj(int index, float scale) {
            GameObject plane = Instantiate(preGroundPlane);
            GameObject obj = Instantiate(preObjs[index], Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(plane.transform, true);
            obj.transform.localScale = new Vector3(scale, scale, scale);
            GetComponent<ContentPositioningBehaviour>().AnchorStage = plane.GetComponent<AnchorBehaviour>();
            flag = true;
    }

    private void OnGUI() {
        GUIStyle styButton = new GUIStyle(GUI.skin.button);
        styButton.fontSize = 15;
        if (GUI.Button(new Rect(10, 10, 100, 50), btns[0])) {
            AddObj(0, 0.04f);
        }
        if (GUI.Button(new Rect(110, 10, 100, 50), btns[1])) {
            AddObj(1, 0.04f);
        }
        if (GUI.Button(new Rect(210, 10, 100, 50), btns[2])) {
            AddObj(2, 0.04f);
        }
        // if (GUI.Button(new Rect(10, 70, 100, 50), "Rotate", styButton)) {
        //     GetComponent<ContentPositioningBehaviour>().AnchorStage.transform.GetChild(0).Rotate(0, 20, 0);
        // }
        // if(GUI.Button(new Rect(110, 70, 100, 50), "+", styButton)) {
        //     GetComponent<ContentPositioningBehaviour>().AnchorStage.transform.GetChild(0).localScale *= 1.1f;
        // }
        // if (GUI.Button(new Rect(210, 70, 100, 50), "-", styButton)) {
        //     GetComponent<ContentPositioningBehaviour>().AnchorStage.transform.GetChild(0).localScale *= 0.9f;
        // }
        if (GUI.Button(new Rect(310, 70, 100, 50), "Delete", styButton)) {
            // Destroy(GetComponent<ContentPositioningBehaviour>().AnchorStage);
            Destroy(csMouseDrag.selector.transform.parent.gameObject);
            csMouseDrag.selector = null;
        }
    }

    public void AutomaticHitTest(HitTestResult result) {
        // Debug.Log("OnAutomaticHitTest");
        if (flag)
            GetComponent<ContentPositioningBehaviour>().PositionContentAtPlaneAnchor(result);
        flag = false;
    }

    public void InteractiveHitTest(HitTestResult result) {
        Debug.Log("interactive hit test");
        if (GetComponent<ContentPositioningBehaviour>().AnchorStage != null)
            GetComponent<ContentPositioningBehaviour>().PositionContentAtPlaneAnchor(result);
    }

    public void ConentPlaced(GameObject gameObject) {
        Debug.Log("content placed");
        GetComponent<ContentPositioningBehaviour>().AnchorStage = null;
    }
}
