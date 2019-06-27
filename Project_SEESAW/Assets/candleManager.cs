using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class candleManager : MonoBehaviour
{
    public GameObject btn;

    private void Start()
    {
        btn.SetActive(false);
    }

    public void OnOffGameObject(bool b)
    {
        btn.SetActive(b);
    }

    public void LoadToMain()
    {
        SceneManager.LoadScene("Leap Motion Test");
    }
}
