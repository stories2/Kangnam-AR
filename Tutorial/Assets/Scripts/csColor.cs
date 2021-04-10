using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csColor : MonoBehaviour
{
    void OnMouseDown() {
        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        GetComponent<Renderer>().material.color = newColor;
    }
}
