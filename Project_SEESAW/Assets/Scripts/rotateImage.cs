
using System.Collections;
using System.Collections.Generic;
using Microsoft.CognitiveServices.Speech;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class rotateImage : MonoBehaviour
{
    private enum State {UP, DOWN, LEFT, RIGHT};
    private State ERotateState;
    private Transform m_transform;
    public Text m_text;

    private string RandNum;
    public TextMeshPro TMP;

    private double eyeSight = 0.1;
    private int wrongAnswer = 0;

    private object threadLocker = new object();
    private bool waitingForReco;
    private string message;
    private bool micPermissionGranted = false;

    public Transform LocalTransform
    {
        get {
            if (m_transform == null)
                m_transform = GetComponent<Transform>();
            return m_transform;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        CreateRandomTMP();
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

        m_text.text = eyeSight.ToString();
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
        if(message != null)
        {
            Check();
            message = null;
        }
    
    }

    private void Check()
    {
        
        if(message == TMP.text + ".")
        {
            Debug.Log("맞음");
        }
        else
        {
            Debug.Log("틀림");
        }
    }

    private void CreateRandomTMP()
    {
        RandNum = Random.Range(1, 9).ToString();
        TMP.text = RandNum;
        RecordVoice();

    }

    public void RotateImage(int direction)
    {
        CreateRandomTMP();
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
    }
}
