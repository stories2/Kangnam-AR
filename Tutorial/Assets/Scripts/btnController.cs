using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnController : MonoBehaviour
{
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onChangeColorBtnClicked() {
        obj.GetComponent<Renderer>().material.color = Color.green;
    }
}
