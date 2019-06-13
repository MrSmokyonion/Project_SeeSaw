using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHand : MonoBehaviour
{
    public GameObject target;

    private Transform tr;
    private Transform tartr;
    private Vector3 originPos;
    private Vector3 tarPos;

    void Start()
    {
        tr = GetComponent<Transform>();
        tartr = target.GetComponent<Transform>();
        originPos = tr.position;
    }

    void Update()
    {
        //tarPos = tartr.position;
        //tr.transform.position = new Vector3(-0.04f, -0.001f, 0.08f);
        //transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
    }
}
