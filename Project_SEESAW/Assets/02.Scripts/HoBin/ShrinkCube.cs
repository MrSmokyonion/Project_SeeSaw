using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkCube : MonoBehaviour
{
    public Camera arcamera;

    public GameObject cube;

    public Vector3 Minimum;

    private void Start()
    {
        Minimum = new Vector3(0.09999993f, 0.09999993f);
    }

    private void OnMouseDown()
    {
        if(arcamera.transform.localScale.x > Minimum.x || arcamera.transform.localScale.y > Minimum.y)
        {
            arcamera.transform.localScale -= new Vector3(0.1f, 0.1f);
        }        
    }
}
