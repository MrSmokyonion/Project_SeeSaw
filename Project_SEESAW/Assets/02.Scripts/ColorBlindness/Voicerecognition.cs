using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
using TMPro;
using System.Collections;
using System;
using System.Collections.Generic;

public class Voicerecognition : MonoBehaviour
{
    // Hook up the two properties below with a Text and Button object in your UI.
    //=====================================================
    private object threadLocker = new object();
    private bool waitingForReco;
    private string message;
    private string tmessage;
    //=====================================================================
    private enum State { Normal, RedGreen, BlueYellow, Defualt };
    private State state;
    #region 테스트 정답
    private int[] NormalTestNormalAnswer            = { 5, 2, 0, 75, 48 };
    private int[] NormalTestRedGreenAnswer          = { 3, 4, 0, 16, 13 };
    private int[] NormalTestBlueYellowAnswer        = { -1, -1, 4, -1, -1 };
    private int[] NormalTestAllColorAnswer          = { 0, -1, -1, 0, 0 };
    //=====================================================================
    private int[] RedGreenTestNormalAnswer          = { 74, 5, 6, 42 };
    private int[] RedGreenTestRedGreenAnswer        = { 21, 2, 5, 4 };
    private int[] RedGreenTestBlueYellowAnswer      = { -1, -1, -1, 0 };
    private int[] RedGreenTestAllColorAnswer        = { 0, 0, 0, -1 };
    //=====================================================================
    private int[] BlueYellowTestNormalAnswer        = { 66, 42 };
    private int[] BlueYellowTestRedGreenAnswer      = { -1, -1 };
    private int[] BlueYellowTestBlueYellowAnswer    = { 0, 0 };
    private int[] BlueYellowTestAllColorAnswer      = { -1, -1 };
    //=====================================================================
    private int[] DefualtTestNormalAnswer           = { 16, 5, 15, 97, 8 };
    private int[] DefualtTestRedGreenAnswer         = { 0, 2, 17, 0, 0 };
    private int[] DefualtTestBlueYellowAnswer       = { -1, -1, -1, -1, -1 };
    private int[] DefualtTestAllColorAnswer         = { 0, 0, 17, 0, 3 };
    #endregion
    #region 테스트판
    [Header("Test Object")]
    public GameObject[] NormalTesting = new GameObject[5];
    public GameObject[] RedGreenTesting = new GameObject[4];
    public GameObject[] BlueYellowTesting = new GameObject[2];
    public GameObject[] DefualtTesting = new GameObject[5];
    #endregion
    #region 테스트 부모
    [Header("Parents")]
    public GameObject NormalTest;
    public GameObject RedGreenTest;
    public GameObject BlueYellowTest;
    public GameObject DefualtTest;
    #endregion
    #region 이미지 보여주는 카운트
    private int NormalCount;
    private int RedGreenCount;
    private int BlueYellowCount;
    private int AllColorCount;
    private int DefualtCount;
    #endregion
    public TextMeshPro textMeshPro;

    [Space(10)]
    public ResultObj resultObj;

    private List<int> TestResult = new List<int>();
    private int tmp;
    void Start()
    {
        tmp = 0;
        state = State.Normal;
        NormalCount = 0; RedGreenCount = 0; BlueYellowCount = 0; AllColorCount = 0; DefualtCount = 0;

        Voice();
    }

    private void Update()
    {
        lock (threadLocker)
        {
            if (Result())
            {
                return;
            }

            if (message != null)
            {
                Check();
                message = null;
            }
        }
    }

    private void ResultImage()
    {
        NormalTest.SetActive(false);
        RedGreenTest.SetActive(false);
        BlueYellowTest.SetActive(false);
        DefualtTest.SetActive(false);
    }

    private bool Result()
    {

        if (NormalCount == NormalTesting.Length)
        {
            ResultImage();
            textMeshPro.text = "정상입니다";
            resultObj.PrepareToLoadMain(new Vector3(1.0f, 1.0f, 1.0f));
            return true;
        }

        else if (RedGreenCount == RedGreenTesting.Length)
        {
            ResultImage();
            textMeshPro.text = "적록색맹입니다";
            resultObj.PrepareToLoadMain(new Vector3(5.0f, 5.0f, 1.0f));
            return true;
        }

        else if (BlueYellowCount == BlueYellowTesting.Length)
        {
            ResultImage();
            textMeshPro.text = "청황색맹입니다.";
            resultObj.PrepareToLoadMain(new Vector3(3.0f, 1.0f, 5.0f));
            return true;
        }

        else if (DefualtCount == DefualtTesting.Length)
        {
            ResultImage();
            textMeshPro.text = "전색맹입니다.";
            resultObj.PrepareToLoadMain(new Vector3(1.0f, 1.0f, 1.0f));
            return true;
        }

        return false;
    }

    void Check()
    {
#pragma warning disable CS0184 // 'is' 식의 지정된 식이 제공된 형식이 아닙니다.
        if (int.TryParse(tmessage, out tmp))
#pragma warning restore CS0184 // 'is' 식의 지정된 식이 제공된 형식이 아닙니다.
        {
            Testing();
            ImageShow();
            Voice();
        }
        else
        {
            textMeshPro.text = tmessage;
            Voice();
            tmessage = null;
        }

    }

    private void StateCheck()
    {
        switch (state)
        {
            case State.Normal:
                if (tmp == NormalTestNormalAnswer[NormalCount])
                {
                    state = State.Normal;
                    NormalCount++;
                }
                else if (tmp == NormalTestRedGreenAnswer[NormalCount])
                {
                    state = State.RedGreen;
                    NormalCount++;
                }
                else if (tmp == NormalTestBlueYellowAnswer[NormalCount])
                {
                    state = State.BlueYellow;
                    NormalCount++;
                }
                else
                {
                    state = State.Defualt;
                    NormalCount++;
                }
                break;
            //================================================================================
            case State.RedGreen:
                if (tmp == RedGreenTestNormalAnswer[RedGreenCount])
                {
                    state = State.Normal;
                    RedGreenCount++;
                }
                else if (tmp == RedGreenTestRedGreenAnswer[RedGreenCount])
                {
                    state = State.RedGreen;
                    RedGreenCount++;
                }
                else if (tmp == RedGreenTestBlueYellowAnswer[RedGreenCount])
                {
                    state = State.BlueYellow;
                    RedGreenCount++;
                }
                else
                {
                    state = State.Defualt;
                    RedGreenCount++;
                }
                break;
            //================================================================================
            case State.BlueYellow:
                if (tmp == BlueYellowTestNormalAnswer[BlueYellowCount])
                {
                    state = State.Normal;
                    BlueYellowCount++;
                }
                else if (tmp == BlueYellowTestRedGreenAnswer[BlueYellowCount])
                {
                    state = State.RedGreen;
                    BlueYellowCount++;
                }
                else if (tmp == BlueYellowTestBlueYellowAnswer[BlueYellowCount])
                {
                    state = State.BlueYellow;
                    BlueYellowCount++;
                }
                else
                {
                    state = State.Defualt;
                    BlueYellowCount++;
                }
                break;
            //================================================================================
            case State.Defualt:
                if (tmp == DefualtTestNormalAnswer[DefualtCount])
                {
                    state = State.Normal;
                    DefualtCount++;
                }
                else if (tmp == DefualtTestRedGreenAnswer[DefualtCount])
                {
                    state = State.RedGreen;
                    DefualtCount++;
                }
                else if (tmp == DefualtTestBlueYellowAnswer[DefualtCount])
                {
                    state = State.RedGreen;
                    DefualtCount++;
                }
                else if (tmp == DefualtTestAllColorAnswer[DefualtCount])
                {
                    state = State.RedGreen;
                    DefualtCount++;
                }
                else
                {
                    state = State.Defualt;
                    DefualtCount++;
                }
                break;
        }
    }

    private void Testing()
    {
        textMeshPro.text = tmessage;
        StateCheck();
        TestResult.Add(tmp);
        tmessage = null;
        tmp = 0;
    }

    private void ImageShow()
    {
        switch (state)
        {
            case State.Normal:
                NormalTest.SetActive(true);
                RedGreenTest.SetActive(false);
                BlueYellowTest.SetActive(false);
                DefualtTest.SetActive(false);
                for (int i = 0; i < NormalTesting.Length; i++)
                {
                    if (i == NormalCount)
                        NormalTesting[i].SetActive(true);
                    else
                        NormalTesting[i].SetActive(false);
                }
                break;
            case State.RedGreen:
                NormalTest.SetActive(false);
                RedGreenTest.SetActive(true);
                BlueYellowTest.SetActive(false);
                DefualtTest.SetActive(false);
                for (int i = 0; i < RedGreenTesting.Length; i++)
                {
                    if (i == RedGreenCount)
                        RedGreenTesting[i].SetActive(true);
                    else
                        RedGreenTesting[i].SetActive(false);
                }
                break;
            case State.BlueYellow:
                NormalTest.SetActive(false);
                RedGreenTest.SetActive(false);
                BlueYellowTest.SetActive(true);
                DefualtTest.SetActive(false);
                for (int i = 0; i < BlueYellowTesting.Length; i++)
                {
                    if (i == BlueYellowCount)
                        BlueYellowTesting[i].SetActive(true);
                    else
                        BlueYellowTesting[i].SetActive(false);
                }
                break;
            case State.Defualt:
                NormalTest.SetActive(false);
                RedGreenTest.SetActive(false);
                BlueYellowTest.SetActive(false);
                DefualtTest.SetActive(true);
                for (int i = 0; i < DefualtTesting.Length; i++)
                {
                    if (i == DefualtCount)
                        DefualtTesting[i].SetActive(true);
                    else
                        DefualtTesting[i].SetActive(false);
                }
                break;
        }
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
                message = newMessage;
                Debug.Log("입력된 값: " + message);
                tmessage = message.Trim('.');
                Debug.Log("트림된 값: " + tmessage);
                waitingForReco = false;
            }
        }
    }
}