using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//===================================
using Microsoft.Speech.Recognition;
using Microsoft.Speech.Synthesis;
//===================================

public class VoiceRecognition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        switch (e.Result.Text)
        {
            case "다음":
                ColorTest.NormalButtonClick();
                break;
        }
    }
}
