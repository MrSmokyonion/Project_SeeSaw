using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomGenerator : MonoBehaviour
{
    public GameObject TextBox;
    public int TheNumber;
    
    public void RandomGenerate()
    {
        TheNumber = Random.Range(1, 100);
        TextBox.GetComponent<Text>().text = "" + TheNumber;
    }
}

