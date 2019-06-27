
using System.Collections;
using System.Collections.Generic;
using Microsoft.CognitiveServices.Speech;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class rotateImage : MonoBehaviour
{
    private enum State {UP, DOWN, LEFT, RIGHT};
    private State ERotateState;
    private Transform m_transform;
    private RectTransform TMPtransform;
    public TextMeshPro TMPEyeSight;

    private string RandNum;
    public TextMeshPro TMPNumber;

    float sceneChangeRemaningTime = 5.0f;
    bool IsFinishTest = false;
    private double eyeSight = 0.1;
    private int wrongAnswer = 0;

    private object threadLocker = new object();
    private bool waitingForReco;
    private string message;

    public VisionResultObj resultObj;

    public RectTransform TMPRectTransform
    {
        get
        {
            if (TMPtransform == null)
                TMPtransform = GetComponent<RectTransform>();
            return TMPtransform;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        CheckEyeSight();
        TMPEyeSight.text = eyeSight.ToString("0.0");
        //        if (outputText == null)
        //        {
        //            UnityEngine.Debug.LogError("outputText property is null! Assign a UI Text element to it.");
        //        }

        //        else
        //        {
        //            // Continue with normal initialization, Text and Button objects are present.

        //#if PLATFORM_ANDROID
        //            // Request to use the microphone, cf.
        //            // https://docs.unity3d.com/Manual/android-RequestingPermissions.html
        //            message = "Waiting for mic permission";
        //            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        //            {
        //                Permission.RequestUserPermission(Permission.Microphone);
        //            }
        //#else
        //            micPermissionGranted = true;
        //            message = "Click button to recognize speech";
        //#endif
        //            startRecoButton.onClick.AddListener(ButtonClick);
        //        }

    }

    // Update is called once per frame
    void Update()
    {
#if PLATFORM_ANDROID
        if (!micPermissionGranted && Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            micPermissionGranted = true;
            message = "Click button to recognize speech";
        }
#endif
        if (message != null)
        {
            Check();
            message = null;
        }

        if (IsFinishTest)
        {
            TMPNumber.text = eyeSight.ToString() ;
            sceneChangeRemaningTime -= Time.deltaTime;
            resultObj.PrepareResult(float.Parse(TMPNumber.text));
        }

        if (sceneChangeRemaningTime <= 0)
        {
            //씬전환
        }

    }

    private void FixedTMPScale(float size, bool isCorrect)
    {

        TMPRectTransform.localScale = new Vector3(1 * size, 1 * size, TMPRectTransform.localScale.z);
        TMPEyeSight.text = eyeSight.ToString();
    }

    private void Check()
    {
        if(wrongAnswer < 5 &&  eyeSight != 0.1)
        {
            if (message != TMPNumber.text + ".")
            {
                wrongAnswer += 1;
                eyeSight -= 0.1;
                float size = 1 / ((float)eyeSight);
                Debug.Log(size);
                FixedTMPScale(Temp(size), true);
            }
            else
            {
                eyeSight += 0.1;
                float size = 1 / ((float)eyeSight);
                FixedTMPScale(Temp(size), false);
            }
            CheckEyeSight();
        }
        else
        {
            IsFinishTest = true;
        }
    }

    private float Temp(float value)
    {
        string[] split = value.ToString().Split('.');
        if (split.Length != 2) { return value; }
        int bLength = split[1].Length;
        float ceil = Mathf.Ceil(float.Parse(split[1]));

        var ret = float.Parse(string.Format("{0}.{1}", split[0], split[1]));

        Debug.Log(ret);

        return ret;
    }

    private void CheckEyeSight()
    {
        RandNum = UnityEngine.Random.Range(1, 10).ToString();
        TMPNumber.text = RandNum;
        RecordVoice();
    }

    public async void RecordVoice()
    {
        // Creates an instance of a speech config with specified subscription key and service region.
        // Replace with your own subscription key and service region (e.g., "westus").
        var config = SpeechConfig.FromSubscription("eeeef68ccdef44588e2c2e896e26fceb", "westus");

        // Make sure to dispose the recognizer after use!
        using (var recognizer = new SpeechRecognizer(config))
        {
            lock (threadLocker)
            {
                waitingForReco = true;
            }

            // Starts speech recognition, and returns after a single utterance is recognized. The end of a
            // single utterance is determined by listening for silence at the end or until a maximum of 15
            // seconds of audio is processed.  The task returns the recognition text as result.
            // Note: Since RecognizeOnceAsync() returns only a single utterance, it is suitable only for single
            // shot recognition like command or query.
            // For long-running multi-utterance recognition, use StartContinuousRecognitionAsync() instead.
            var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

            // Checks result.
            string newMessage = string.Empty;
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                newMessage = result.Text;
            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                newMessage = "NOMATCH: Speech could not be recognized.";
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = CancellationDetails.FromResult(result);
                newMessage = $"CANCELED: Reason={cancellation.Reason} ErrorDetails={cancellation.ErrorDetails}";
            }

            lock (threadLocker)
            {
                message = newMessage;
                waitingForReco = false;
            }
        }
        //Check();
    }
}
