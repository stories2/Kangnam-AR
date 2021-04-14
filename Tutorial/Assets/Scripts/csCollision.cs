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
}
