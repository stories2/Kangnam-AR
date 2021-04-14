using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csBallManager : MonoBehaviour
{
    public GameObject ball;
    public GameObject[] boards;
    public GameObject target;

    int index = 0;

    private void Start() {
        for (int i = 0; i < boards.Length; i ++) {
            if (i != 0) boards[i].SetActive(false);
            boards[i].transform.position = Vector3.zero;
            boards[i].transform.SetParent(target.transform);
        }
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 40), "Gravity")) {
            ball.GetComponent<Rigidbody>().useGravity = !ball.GetComponent<Rigidbody>().useGravity;
        }
        if (GUI.Button(new Rect(10, 50, 200, 40), "+")) {
            ball.GetComponent<Rigidbody>().angularDrag += 0.1f;
        }
        if (GUI.Button(new Rect(10, 90, 200, 40), "-")) {
            ball.GetComponent<Rigidbody>().angularDrag -= 0.1f;
        }

        if (GUI.Button(new Rect(10, 130, 200, 40), "Change")) {
            boards[index++ % boards.Length].SetActive(false);
            boards[index % boards.Length].SetActive(true);
        }
    }
}
