using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorResult : MonoBehaviour
{
    public static List<string> ResultNumber = new List<string>();
    //private List<string> NormalJumsu = new List<string>();

    private string[] NormalResult = { "16", "5", "15", "6", "97" ,"8", "26" }; //정상
    private string[] RedGreenColor = { "0", "0", "17", "0", "0", "3", "26" };  //적녹색
    private string[] RedColor = { "0", "0", "17", "0", "0", "3", "6" };        //빨강
    private string[] GreenColor = { "0", "0", "17", "0", "0", "3", "2" };      //녹색 

    private int[] CheckLists = new int[7];
    private int TotalNumber = 0;

    public bool state = false;
    //결과 이벤트
    public GameObject NormalUI;
    public GameObject RedGreenUI;
    public GameObject RedUI;
    public GameObject GreenUI;

    //정상적인 사람들의 점수
    private int NormalHumanScore = 173;
    private int RedGreenHumanScore = 46;
    private int RedHumanScore = 26;
    private int GreenHumanScore = 22;

    //Main카메라 이동
    public Button MainButton;

    private int i;

    private void Start()
    {
        i = 0;
    }

    public void Result()
    {
        foreach(string InspectionResults in ResultNumber)
        {
            string tmp = InspectionResults.Trim('\n');  //list의 \n을 짤랐다
            if (tmp.Equals(NormalResult[i]))
            {
                CheckLists[i] = int.Parse(tmp);
            }
            //적녹색맹
            if (tmp.Equals(RedGreenColor[i]))
            {
                CheckLists[i] = int.Parse(tmp);
            }
            //적색맹
            if (tmp.Equals(RedColor[i]))
            {
                CheckLists[i] = int.Parse(tmp);
            }
            //녹색맹
            if (tmp.Equals(GreenColor[i]))
            {
                CheckLists[i] = int.Parse(tmp);
            }
            i++;
        }
        //=====================================================================================
        for(int j = 0; j < CheckLists.Length; j++)
        {
            TotalNumber += CheckLists[j];
        }
        Debug.Log(TotalNumber);
        //=====================================================================================
        if (TotalNumber == NormalHumanScore)
        {
            NormalUI.SetActive(true);
            CameraFilterPack_Color_RGB.ColorRGB = new Color(1.0f, 1.0f, 1.0f);
        }
        else if (TotalNumber == RedGreenHumanScore)
        {
            RedGreenUI.SetActive(true);
            CameraFilterPack_Color_RGB.ColorRGB = new Color(255.0f, 255.0f, 1.0f);
        }
        else if (TotalNumber == RedHumanScore)
        {
            RedUI.SetActive(true);
            CameraFilterPack_Color_RGB.ColorRGB = new Color(255.0f, 0.0f, 1.0f);
        }
        else if (TotalNumber == GreenHumanScore)
        {
            GreenUI.SetActive(true);
            CameraFilterPack_Color_RGB.ColorRGB = new Color(1.0f, 255.0f, 1.0f);
        }
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
