using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMouseDrag : MonoBehaviour
{
    public static GameObject selector;

    bool flag = false; 

    private void Start() {
        if (selector != null)
            selector.GetComponent<csMouseDrag>().SetSelector(false);
        selector = gameObject;
    }

    public void SetSelector(bool visible) {
        transform.GetChild(0).gameObject.active = visible;
    }

    private void OnMouseDrag() {
        float distance = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        Debug.Log(distance);
        Vector3 v = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance));
        transform.position = v;
    }

    private void OnMouseDown() {
        flag = true;

        if (selector != null) 
            selector.GetComponent<csMouseDrag>().SetSelector(false);
        selector = gameObject;
        SetSelector(true);
    }

    private void OnMouseUp() {
        flag = false;
    }

    private void Update() {
        transform.GetChild(0).transform.Rotate(0, 0, 100 * Time.deltaTime);
        if (flag) {
            float w = Input.GetAxis("Mouse ScrollWheel");
            transform.Rotate(0, w * 30, 0);
        }
    }
}
