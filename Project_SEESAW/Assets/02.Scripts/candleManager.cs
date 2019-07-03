using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class candleManager : MonoBehaviour
{
    [Header("Button")]
    public GameObject btn;

    [Header("Candle")]
    public ParticleSystem flame;
    public Light flameLight;

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
        SceneManager.LoadScene("Main");
    }

    public void FlameOnOff()
    {
        if(flame.isPlaying)
        {
            flame.Stop();
            flame.Clear();
            flameLight.enabled = false;
        }
        else if(!flame.isPlaying)
        {
            flame.Play();
            flameLight.enabled = true;
        }
    }
}
