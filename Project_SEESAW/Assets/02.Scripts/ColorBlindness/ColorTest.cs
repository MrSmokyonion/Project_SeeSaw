using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorTest : MonoBehaviour
{
    public Button NormalButton;
    public Button NoNumberButton;
    public Button RedGreenButton;
    public Button RedButton;
    public Button GreenButton;

    public bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        //List 초기화
        ColorResult.ResultNumber.Clear();
        //InputField를 찾는다
        //InputFieldFind = GameObject.Find("InputField");
        //inputFieldObject = InputFieldFind.GetComponent<InputField>();
       
        //버튼클릭시 이벤트
        NormalButton.onClick.AddListener(NormalButtonClick);
        NoNumberButton.onClick.AddListener(NoNumberButtonClick);
        RedGreenButton.onClick.AddListener(RedGreenBlinessButton);
        RedButton.onClick.AddListener(RedBlinessButton);
        GreenButton.onClick.AddListener(GreenBlinessButton);
        
        //SetActive활성화
        gameObject.SetActive(state);
    }
    //정상적인 번호 선택
    public void NormalButtonClick()
    {
        ColorResult.ResultNumber.Add(NormalButton.GetComponentInChildren<Text>().text);
        //Debug.Log(ColorResult.ResultNumber.Count);
    }
    //숫자 없는 버튼 클릭
    public void NoNumberButtonClick()
    {
        ColorResult.ResultNumber.Add("0");
        //Debug.Log(ColorResult.ResultNumber.Count);
    }
    //적녹색맹 버튼 클릭
    public void RedGreenBlinessButton()
    {
        ColorResult.ResultNumber.Add(RedGreenButton.GetComponentInChildren<Text>().text);
        //Debug.Log(ColorResult.ResultNumber.Count);
    }
    //적색맹 버튼 클릭
    public void RedBlinessButton()
    {
        ColorResult.ResultNumber.Add(RedButton.GetComponentInChildren<Text>().text);
        //Debug.Log(ColorResult.ResultNumber.Count);
    }
    //녹색맹 버튼 클릭
    public void GreenBlinessButton()
    {
        ColorResult.ResultNumber.Add(GreenButton.GetComponentInChildren<Text>().text);
        //Debug.Log(ColorResult.ResultNumber.Count);
    }

    public void Change()
    {
        state = !state;

        if (state == true)
        {
            gameObject.SetActive(true);
        }
        else if (state == false)
        {
            gameObject.SetActive(false);
        }
    }
}
