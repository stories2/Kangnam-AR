using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMoon : MonoBehaviour
{
    float speed;
    private void Start() {
        speed = Random.Range(0.1f, 0.9f);
        float dis = Random.Range(0.7f, 2.0f);
        transform.GetChild(0).localPosition = new Vector3(dis, 0, 0);

        Color newColor = new Color(Random.value, Random.value, Random.value, 1.0f);
        transform.GetChild(0).GetComponent<Renderer>().material.color = newColor;

    }

    private void Update() {
        transform.Rotate(0, speed, 0);
    }
}
