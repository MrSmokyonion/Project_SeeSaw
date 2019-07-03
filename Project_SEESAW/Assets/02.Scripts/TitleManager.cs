using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public TextMeshPro txt;

    void Start()
    {
        txt.text = "환영합니다!";
        StartCoroutine(FadeInOut("시작 전,\n테스트를 진행합니다!"));
    }

    private void Update()
    {
        
    }

    IEnumerator FadeInOut(string str)
    {
        txt.alpha = 0;
        while (txt.alpha < 1)
        {
            txt.alpha += 0.02f;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(3.0f);

        while (txt.alpha > 0)
        {
            txt.alpha -= 0.02f;
            yield return new WaitForEndOfFrame();
        }

        int tmp = PlayerPrefs.GetInt("isFirst");
        if (tmp >= 1)
        {
            SceneManager.LoadScene("Main");
        }

        if (str != string.Empty)
        {
            txt.text = str;
            StartCoroutine(FadeInOut(string.Empty));
        }
        else
        {
            PlayerPrefs.SetInt("isFirst", 1);
            SceneManager.LoadScene("ColorBlind");
        }
    }
}