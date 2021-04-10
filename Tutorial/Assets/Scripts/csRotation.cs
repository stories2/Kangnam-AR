using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csRotation : MonoBehaviour
{
    float speed = 0.5f;

    void Update() {
        transform.GetChild(0).Rotate(0, speed, 0);
    }

    void OnMouseDown() {
       speed += 0.1f; 
    }
}
