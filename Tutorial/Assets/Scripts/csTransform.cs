using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csTransform : MonoBehaviour
{
    float speed = 20.0f;
    float rotSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");

        h = h * speed * Time.deltaTime;
        v = v * speed * Time.deltaTime;
        mouseX = mouseX * rotSpeed;

        transform.Translate(Vector3.right * h);
        transform.Translate(Vector3.forward * v);
        transform.Rotate(Vector3.up * mouseX);
    }
}
