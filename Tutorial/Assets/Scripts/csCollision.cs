using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        GetComponent<Animator>().SetTrigger("Run");
    }

    private void OnTriggerExit(Collider other) {
        GetComponent<Animator>().SetTrigger("Idle");
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    int count = 0;
    void OnMouseDown()
    {
        switch(count) {
            case 0:
                GetComponent<Animator>().SetTrigger("run");
            break;
            case 1:
                GetComponent<Animator>().SetTrigger("idle");
            break;
            case 2:
                GetComponent<Animator>().SetTrigger("walk");
            break;
            case 3:
                GetComponent<Animator>().SetTrigger("dance");
            break;
            case 4:
                GetComponent<Animator>().SetTrigger("swing");
            break;
        }
        count = (count + 1) % 5;
    }
}
