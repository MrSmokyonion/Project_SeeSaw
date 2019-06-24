using Leap.Unity;
using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Menu_Talk : MonoBehaviour
{
    List<Thread> tmp;

    private void Start()
    {
        
    }

    public void ButtonSay(int num)
    {
        Debug.Log(num + "Button Clicked!");
    }

    public void SpawnCube(GameObject obj)
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        cube.transform.position = obj.transform.position;
    }

    public void SliderSay(InteractionSlider tmp)
    {
        Debug.Log(tmp.HorizontalSliderValue);
    }
}
