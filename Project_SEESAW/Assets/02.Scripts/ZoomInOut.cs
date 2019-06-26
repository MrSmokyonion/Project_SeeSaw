using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInOut : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject rightHand;

    [Space(10)]
    public GameObject zoomObject;

    private Transform leftPos;
    private Transform rightPos;
    private Transform zoomObjTrans;
    private bool leftPinch;
    private bool rightPinch;
    private bool palm;
    private bool init;
    private float originDis;
    private Vector3 originScale;

    private void Awake()
    {
        leftPinch = false;
        rightPinch = false;
        palm = false;
        init = false;

        leftPos = leftHand.GetComponent<Transform>();
        rightPos = rightHand.GetComponent<Transform>();
        zoomObjTrans = zoomObject.GetComponent<Transform>();
    }

    private void Update()
    {
        if (!palm)
            return;

        //왼손, 오른손 둘다 Pinch 일때
        if (leftPinch && rightPinch)
        {
            if (!init)
            {
                originScale = zoomObjTrans.localScale;
                originDis = Vector3.Distance(leftPos.position, rightPos.position);
                init = true;
            }

            float distance = -1 * (Vector3.Distance(leftPos.position, rightPos.position) - originDis);
            Vector3 check = originScale - new Vector3(distance, distance);

            if (check.x < 0.1f)
                return;

            zoomObjTrans.localScale = check;
        }
        else if (!leftPinch || !rightPinch)
        {
            init = false;
        }
    }

    public void SetLeftPinch(bool val)
    {
        leftPinch = val;
    }
    public void SetRightPinch(bool val)
    {
        rightPinch = val;
    }

    public void SetPalm(bool val)
    {
        palm = val;
    }
}
