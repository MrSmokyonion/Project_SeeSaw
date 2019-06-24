using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorBlindnessScenesMove : MonoBehaviour
{
    public Button ColorTestScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ColorTestScene.onClick.AddListener(ColorTestSceneChange);
    }

    public void ColorTestSceneChange()
    {
        SceneManager.LoadScene("ColorBlindness");
    }
}
