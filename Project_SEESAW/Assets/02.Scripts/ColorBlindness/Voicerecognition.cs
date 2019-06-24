using UnityEngine;
using UnityEngine.UI;
using Microsoft.CognitiveServices.Speech;
using TMPro;
using System.Collections;

public class Voicerecognition : MonoBehaviour
{
    //텍스트 판
    public GameObject[] Tests = new GameObject[7];
    //몇번째 테스트중인가
    private int count;
    //정상
    private string[] NomalNumber = new string[] { "16", "5", "15", "6", "97", "8", "26" };
    private int[] NomalResult = new int[] { 16, 5, 15, 6, 97, 8, 26 };
    //적녹색
    //private string[] RedGreenNumber = new string[] { "0", "0", "17", "5", "0", "3" };
    //녹색
    private string[] GreenNumber = new string[] { "0", "0", "17", "5", "0", "3", "2" };
    private int[] GreenResult = new int[] { 0, 0, 17, 5, 0, 3, 2 };
    //적색
    private string[] RedNumber = new string[] { "0", "0", "17", "5", "0", "3", "6" };
    private int[] RedResult = new int[] { 0, 0, 17, 5, 0, 3, 6 };
    //입력 결과값
    public static int ResultNumber;
    //색맹의 결과
    public static int NomalScore;
    public static int GreenScore;
    public static int RedScore;

    public TextMeshPro textMeshPro;
    // Hook up the two properties below with a Text and Button object in your UI.
    //쓰레드 락
    private object threadLocker = new object();

    private bool waitingForReco;
    private string message;
    string tmessage;
    //for의 변수=========================================
    int i;

    [Space(10)]
    public ResultObj resultObj;
    public GameObject btn;

    void Start()
    {
        count = 0;
        ResultNumber = 0;
        i = 0;
        NomalScore = 173;
        GreenScore = 27;
        RedScore = 31;
        btn.SetActive(false);
        //==================================================================================
        Voice();
    }

    private void Update()
    {
        if(ResultNumber == NomalScore)
        {
            resultObj.result = new Vector3(1.0f, 1.0f, 1.0f);
            textMeshPro.text = "정상입니다.";
            btn.SetActive(true);
            return;
        }
        if (ResultNumber == GreenScore)
        {
            resultObj.result = new Vector3(1.0f, 5.0f, 1.0f);
            textMeshPro.text = "녹생맹입니다.";
            btn.SetActive(true);
            return;
        }
        if (ResultNumber == RedScore)
        {
            resultObj.result = new Vector3(5.0f, 1.0f, 1.0f);
            textMeshPro.text = "적색맹입니다.";
            btn.SetActive(true);
            return;
        }

        if (message != null)
        {
            WaitFor();
            Check();
        }
            
    }

    IEnumerable WaitFor()
    {
        yield return new WaitForSeconds(.1f);
    }

    void Check()
    {
        //정상인
        if (tmessage.Equals(NomalNumber[count]))
        {
            ResultNumber += NomalResult[count];
            textMeshPro.text = tmessage;
            Debug.Log("입력 받은 정보>>" + tmessage);
            message = null;
            Voice();
            if (textMeshPro.text != null)
            {
                count++;            //몇번째 이미지를 보여줄 변수
                message = null;
                textMeshPro.text = null;
            }
        }
        //녹색
        else if (tmessage.Equals(GreenNumber[count]))
        {
            ResultNumber += GreenResult[count];
            textMeshPro.text = tmessage;
            Debug.Log("입력 받은 정보>>" + tmessage);
            message = null;
            Voice();
            if (textMeshPro.text != null)
            {
                count++;            //몇번째 이미지를 보여줄 변수
                message = null;
                textMeshPro.text = null;
            }
        }
        //적색
        else if (tmessage.Equals(RedNumber[count]))
        {
            ResultNumber += RedResult[count];
            textMeshPro.text = tmessage;
            Debug.Log("입력 받은 정보>>" + tmessage);
            message = null;
            Voice();
            if (textMeshPro.text != null)
            {
                count++;            //몇번째 이미지를 보여줄 변수
                message = null;
                textMeshPro.text = null;
            }
        }
        //정상 or 녹색을 입력하지 않을 경우
        else
        {
            Debug.Log("잘못된 입력 정보>>" + message);
            message = null;
            Voice();
        }

        //이미지 변환
        for (i = 0; i < 7; i++)
        {
            //이미지를 보여주는 함수
            if (i == count)
            {
                Tests[i].SetActive(true);
            }
            else
            {
                Tests[i].SetActive(false);
            }
        }
        //================================================================================
    }

    public async void Voice()
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
            //else if (result.Reason == ResultReason.NoMatch)
            //{
            //    newMessage = "NOMATCH: Speech could not be recognized.";
            //}
            //else if (result.Reason == ResultReason.Canceled)
            //{
            //    var cancellation = CancellationDetails.FromResult(result);
            //    newMessage = $"CANCELED: Reason={cancellation.Reason} ErrorDetails={cancellation.ErrorDetails}";
            //}

            lock (threadLocker)
            {
                message = newMessage;
                tmessage = message.Trim('.');
                waitingForReco = false;
            }
        }        
    }
}
