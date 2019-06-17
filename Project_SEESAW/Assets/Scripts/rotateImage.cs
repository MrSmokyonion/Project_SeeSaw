
using System.Collections;
using System.Collections.Generic;
using Microsoft.CognitiveServices.Speech;

using UnityEngine;
using UnityEngine.UI;


public class rotateImage : MonoBehaviour
{
    private enum State {UP, DOWN, LEFT, RIGHT};
    private State ERotateState;
    private Transform m_transform;
    public Text m_text;

    private double eyeSight = 0.1;
    private int wrongAnswer = 0;

    public Text outputText;
    public Button startRecoButton;

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
        if (outputText == null)
        {
            UnityEngine.Debug.LogError("outputText property is null! Assign a UI Text element to it.");
        }
        else if (startRecoButton == null)
        {
            message = "startRecoButton property is null! Assign a UI Button to it.";
            UnityEngine.Debug.LogError(message);
        }
        else
        {
            // Continue with normal initialization, Text and Button objects are present.

#if PLATFORM_ANDROID
            // Request to use the microphone, cf.
            // https://docs.unity3d.com/Manual/android-RequestingPermissions.html
            message = "Waiting for mic permission";
            if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                Permission.RequestUserPermission(Permission.Microphone);
            }
#else
            micPermissionGranted = true;
            message = "Click button to recognize speech";
#endif
            startRecoButton.onClick.AddListener(ButtonClick);
        }
        RandomRotate();
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

        lock (threadLocker)
        {
            if (startRecoButton != null)
            {
                startRecoButton.interactable = !waitingForReco && micPermissionGranted;
            }
            if (outputText != null)
            {
                outputText.text = message;
            }
        }
    }

    private void Check(int direction)
    {
        if(wrongAnswer <= 5)
        {
            if (ERotateState == (State)direction)
            {
                LocalTransform.localPosition =
                    new Vector3(LocalTransform.localPosition.x, LocalTransform.localPosition.y, LocalTransform.localPosition.z + 2);
                eyeSight += 0.1;
                m_text.text = eyeSight.ToString();
            }
            else
            {
                wrongAnswer += 1;
                LocalTransform.localPosition =
                    new Vector3(LocalTransform.localPosition.x, LocalTransform.localPosition.y, LocalTransform.localPosition.z - 2);
                eyeSight -= 0.1;
                m_text.text = eyeSight.ToString();
            }
        }
      else
        {
            Debug.Log(eyeSight.ToString());
        }
    }

    private void RandomRotate()
    {
        ERotateState = (State)Random.Range(0, 3);

        switch (ERotateState)
        {
            case State.DOWN:
                LocalTransform.localRotation = Quaternion.Euler(LocalTransform.localRotation.x, LocalTransform.localRotation.y, 0);
                break;
            case State.RIGHT:
                LocalTransform.localRotation = Quaternion.Euler(LocalTransform.localRotation.x, LocalTransform.localRotation.y, 90);
                break;
            case State.UP:
                LocalTransform.localRotation = Quaternion.Euler(LocalTransform.localRotation.x, LocalTransform.localRotation.y, 180);
                break;
            case State.LEFT:
                LocalTransform.localRotation = Quaternion.Euler(LocalTransform.localRotation.x, LocalTransform.localRotation.y, 270);
                break;
        }
    }

    public void RotateImage(int direction)
    {
        Check(direction);
        RandomRotate();
    }

    public async void ButtonClick()
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
