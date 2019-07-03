using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultObj : MonoBehaviour
{
    public GameObject btn;

    private Vector3 result;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        btn.SetActive(false);
        SceneManager.sceneLoaded += whenLoaded;
    }

    public void PrepareToLoadMain(Vector3 val)
    {
        result = val;
        btn.SetActive(true);
    }

    void whenLoaded(Scene s, LoadSceneMode m)
    {
        if(s.name == "Main")
        {
            BravoX_L tmp = GameObject.Find("CenterEyeAnchor").GetComponent<BravoX_L>();
            tmp.Saturation_Red = result.x;
            tmp.Saturation_Blue = result.y;
            tmp.Saturation_Green = result.z;
            Destroy(gameObject);
        }
    }

    public void LoadToMain()
    {
        SceneManager.LoadScene("Main");
    }
}
