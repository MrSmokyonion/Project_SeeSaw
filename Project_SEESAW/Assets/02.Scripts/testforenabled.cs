using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testforenabled : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject tmp = GameObject.Find("FindMePlz");
        if (tmp == null)
            Debug.Log("null");
        else
            Debug.Log("Found!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
