using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csAnimation : MonoBehaviour
{
    bool state = false;
    private void OnMouseDown() {
        if (state)
            GetComponent<Animator>().SetTrigger("Run");
        else
            GetComponent<Animator>().SetTrigger("Idle");
        state = !state;
    }
}
