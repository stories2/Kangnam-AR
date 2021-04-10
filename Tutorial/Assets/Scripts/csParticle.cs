using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csParticle : MonoBehaviour
{
    public GameObject particle;

    private void OnMouseDown() {
        GameObject obj = Instantiate(particle, transform.position, Quaternion.identity);
        Destroy(obj, 3);
    }
}
