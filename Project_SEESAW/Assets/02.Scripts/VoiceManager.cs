using Microsoft.CognitiveServices.Speech;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    //음성 입력을 위한 변수들.
    private object threadLocker { get; set; }
    private bool waitingForReco { get; set; }
    private string tmessage { get; set; }

    private void Awake()
    {
        threadLocker = new object();
    }

    private void Start()
    {
    }

    public IEnumerator GetVoiceInt(AnswerManager manager)
    {
        int input = 0;
        tmessage = string.Empty;
        Voice();
        yield return new WaitUntil(() => tmessage != string.Empty);
        if (int.TryParse(tmessage, out input) == false)
            input = -2;

        manager.input = input;
    }

    public async void Voice()
    {
        // Creates an instance of a speech config with specified subscription key and service region.
        // Replace with your own subscription key and service region (e.g., "westus").
        var config = SpeechConfig.FromSubscription("f144913cab064320bb3d59a3795ea276", "westus");

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
                newMessage = "말씀을 해주십시오";
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = CancellationDetails.FromResult(result);
                newMessage = $"CANCELED: Reason={cancellation.Reason} ErrorDetails={cancellation.ErrorDetails}";
            }

            lock (threadLocker)
            {
                tmessage = newMessage.Trim('.');
                Debug.Log("입력된 값: " + tmessage);
                if (tmessage == string.Empty)
                    tmessage = "Empty";
                waitingForReco = false;
            }
        }
    }
}
