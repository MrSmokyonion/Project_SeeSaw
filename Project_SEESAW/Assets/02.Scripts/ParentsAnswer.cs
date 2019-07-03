using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentsAnswer : MonoBehaviour
{
    [HideInInspector]
    public OwnAnswer[] childsArray;
    [HideInInspector]
    public int childsCount; //이 오브젝트가 가지고 있는 자식의 갯수

    private int current;

    private int Normal;
    private int RedGreen;
    private int BlueYellow;

    private void Start()
    {
        childsCount = transform.childCount;
        childsArray = new OwnAnswer[childsCount];
        for (int i = 0; i <= childsCount - 1; i++)
        {
            childsArray[i] = transform.GetChild(i).gameObject.GetComponent<OwnAnswer>();
        }

        current = -1;
    }

    public AnswerState CompareToAnswer(int val)
    {
        AnswerState state;

        if (val == Normal)
            state = AnswerState.normal;
        else if (val == RedGreen)
            state = AnswerState.redGreen;
        else if (val == BlueYellow)
            state = AnswerState.blueYellow;
        else
            state = AnswerState.Empty;

        return state;
    }

    public void TurnOffPreviousImage()
    {
        gameObject.transform.GetChild(current).gameObject.SetActive(false);
    }

    public void RefreshAnswer()
    {
        if (current >= (childsCount - 1))
            current = 0;
        else
            current++;

        OwnAnswer tmp = childsArray[current];
        Normal = tmp.normal;
        RedGreen = tmp.redGreen;
        BlueYellow = tmp.blueYellow;

        gameObject.transform.GetChild(current).gameObject.SetActive(true);
    }

}