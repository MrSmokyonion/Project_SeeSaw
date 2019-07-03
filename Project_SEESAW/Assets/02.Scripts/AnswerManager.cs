using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnswerManager : MonoBehaviour
{
    [Header("Parents")]
    public ParentsAnswer normalParents;
    public ParentsAnswer redGreenParents;
    public ParentsAnswer blueYellowParents;

    [Header("VoiceManager")]
    public VoiceManager voiceManager;

    [Header("TextMeshPro")]
    public TextMeshPro tmp;

    [Header("Result Object")]
    public ResultObj result;

    [HideInInspector]
    public int input; //마이크 입력 값
    private int count;
    private AnswerState state; //현재 이미지 상태

    private void Start()
    {
        //StartProcess();
    }

    public void StartProcess()
    {
        state = AnswerState.normal;
        normalParents.RefreshAnswer();
        tmp.text = string.Empty;
        count = 0;

        StartCoroutine(ListenAndCarefully());
    }

    public IEnumerator ListenAndCarefully()
    {
        AnswerState previousState = state;
        input = -999;

        StartCoroutine(voiceManager.GetVoiceInt(this)); //음성 입력. (int 값 받음.)
        yield return new WaitUntil(() => input >= -2);
        ParentsAnswer now = StateToClass(state); // 현재 state의 클래스 가져오기.

        AnswerState nextState = now.CompareToAnswer(input);  //입력 값과 현재 이미지와 비교.

        if (nextState != AnswerState.Empty) //잘못된 값이 아닐 경우.
        {
            if (nextState == state) //이전과 같으면 +1
                count++;
            else //이전과 다르면 초기화 & 다음 state 로드.
            {
                state = nextState;
                count = 1;
            }
        }

        if (count >= 5)
        {
            ResultOutput(previousState);
            yield break;
        }

        StateToClass(previousState).TurnOffPreviousImage(); //이전 state 이미지 가리기
        StateToClass(state).RefreshAnswer(); //다음 state 이미지 준비.

        yield return null;
        StartCoroutine(ListenAndCarefully());
    }

    public ParentsAnswer StateToClass(AnswerState val)
    {
        switch (val)
        {
            case AnswerState.normal: return normalParents;
            case AnswerState.redGreen: return redGreenParents;
            case AnswerState.blueYellow: return blueYellowParents;
            default: return null;
        }
    }

    public void ResultOutput(AnswerState previousState)
    {
        StateToClass(previousState).TurnOffPreviousImage();

        Vector3 val;
        switch (state)
        {
            case AnswerState.normal:    tmp.text = "당신은\n정상입니다.";     val = new Vector3(1.0f, 1.0f, 1.0f); break;
            case AnswerState.redGreen:  tmp.text = "당신은\n적록색약입니다."; val = new Vector3(3.0f, 3.0f, 1.0f); break;
            case AnswerState.blueYellow:tmp.text = "당신은\n청황색약입니다."; val = new Vector3(2.0f, 2.0f, 3.5f); break;
            default: return;
        }
        result.PrepareToLoadMain(val);
    }
}

public enum AnswerState { Empty = 0, normal, redGreen, blueYellow}