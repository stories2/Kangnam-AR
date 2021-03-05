using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{
    public Text txt, txt2;
    // Start is called before the first frame update
    void Start()
    {
        txt.text = "Loading native lib";
        txt2.text = NativeAdapter.dllPath;
        try {
            txt.text = "Native: " + NativeAdapter.FooTest().ToString();
        } catch (System.Exception e) {
            txt.text = e.Message;
            Debug.Log(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
