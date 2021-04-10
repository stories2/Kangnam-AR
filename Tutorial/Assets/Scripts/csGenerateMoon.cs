using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csGenerateMoon : MonoBehaviour
{
    public GameObject moon;
    private void OnMouseDown() {
        GameObject p = Instantiate(moon, Vector3.zero, Quaternion.identity);
        p.transform.SetParent(transform, false);
    }
}
