using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultObj : MonoBehaviour
{
    [HideInInspector]
    public Vector3 result;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += whenLoaded;

        result = new Vector3(5.0f, 1.0f, 1.0f);
    }

    public void SetResult(Vector3 val)
    {
        result = val;
    }

    void whenLoaded(Scene s, LoadSceneMode m)
    {
        if(result != null)
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
        SceneManager.LoadScene("Leap Motion Test");
    }

    public void say()
    {
        Debug.Log("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");
    }
}
