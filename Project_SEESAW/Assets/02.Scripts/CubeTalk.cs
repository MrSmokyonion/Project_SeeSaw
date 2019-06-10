using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTalk : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            Debug.Log("Hello GitHub");

        if (Input.GetKeyDown(KeyCode.RightArrow))
            Debug.Log("Hello World");
    }
}
