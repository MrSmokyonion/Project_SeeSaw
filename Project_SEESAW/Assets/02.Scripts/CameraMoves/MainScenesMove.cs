using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScenesMove : MonoBehaviour
{
    //mainscenes으로 가는 변수
    public Button MainScene;
    //버튼 보이는 변수
    public bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(state);
    }

    // Update is called once per frame
    void Update()
    {
        MainScene.onClick.AddListener(MainSceneChange);
    }

    public void MainSceneChange()
    {
        SceneManager.LoadScene("Main");
    }
    //보이는 함수
    public void SetActiveChange()
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
