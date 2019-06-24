using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpansionCube : MonoBehaviour
{
    public Camera arcamera;

    public GameObject cube;

    private void OnMouseDown()
    {
        arcamera.transform.localScale += new Vector3(0.1f, 0.1f);
    }
}
