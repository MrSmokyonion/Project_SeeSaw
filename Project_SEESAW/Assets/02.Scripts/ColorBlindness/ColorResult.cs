using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorResult : MonoBehaviour
{
    public static List<int> ResultNumber = new List<int>();

    int TotalNumber = 0;

    public bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Result()
    {
        foreach (int result in ResultNumber)
        {
            TotalNumber += result;
        }
        Debug.Log("결과 :" + TotalNumber);
    }

    public void Change()
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
