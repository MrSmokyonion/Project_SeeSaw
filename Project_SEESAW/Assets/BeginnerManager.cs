using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginnerManager : MonoBehaviour
{
    public AnswerManager answerManager;
    public TextMeshPro txt;

    private void Start()
    {
        int tmp = PlayerPrefs.GetInt("isFirst");
        if (tmp >= 1)
        {
            answerManager.StartProcess();
        }
        else
        {
            StartCoroutine(FadeInOut(2, new string[] { "색약 테스트를\n시작합니다.", "정면에 보이는 그림을\n읽어주세요!" }));
        }

    }

    IEnumerator FadeInOut(int num, string[] arr)
    {
        for (int i = 0; i < num; i++)
        {
            txt.text = arr[i];

            txt.alpha = 0;
            while (txt.alpha < 1)
            {
                txt.alpha += 0.05f;
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(3.0f);

            while (txt.alpha > 0)
            {
                txt.alpha -= 0.05f;
                yield return new WaitForEndOfFrame();
            }
        }
        answerManager.StartProcess();
    }
}
