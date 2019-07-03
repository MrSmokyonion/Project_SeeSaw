using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisionResultObj : MonoBehaviour
{
    public GameObject Btn;

    [HideInInspector]
    public float result;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Btn.SetActive(false);
        SceneManager.sceneLoaded += WhenLoadScene;
    }

    public void PrepareResult(float val)
    {
        result = val;
        Btn.SetActive(true);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    private void WhenLoadScene(Scene s, LoadSceneMode m)
    {
        if (s.name == "Main")
        {
            //메인 돌아왔을 때 할 작업들.
            Debug.Log(result);

            Destroy(gameObject);
        }
    }
}
