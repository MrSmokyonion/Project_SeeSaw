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

    public void SliderSay(GameObject tmp)
    {
        Debug.Log(123);
    }
}
