using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorTest : MonoBehaviour
{
    private int ColorBlindnessNumber = 0;

    public Button NormalButton;
    public Button BlindnessButton;

    public bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(state);
        NormalButton.onClick.AddListener(NormalButtonClick);
        BlindnessButton.onClick.AddListener(ColorBindnessButtonClick);
    }

    public void NormalButtonClick()
    {
        ColorResult.ResultNumber.Add(1);
        Debug.Log("정상");
        Debug.Log("카운트" + ColorResult.ResultNumber.Count);
    }

    public void ColorBindnessButtonClick()
    {
        ColorResult.ResultNumber.Add(0);
        Debug.Log("색맹");
        Debug.Log("카운트" + ColorResult.ResultNumber.Count);
    }

    public void Change()
    {
        state = !state;

        if (state == true)
        {
            gameObject.SetActive(true);
        }
        else if (state == false)
        {
            gameObject.SetActive(false);
        }
    }


}
