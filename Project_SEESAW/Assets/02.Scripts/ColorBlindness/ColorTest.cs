using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorTest : MonoBehaviour
{
<<<<<<< HEAD
<<<<<<< HEAD
    public static Button NormalButton;
    public Button NoNumberButton;
    public Button RedGreenButton;
    public Button RedButton;
    public Button GreenButton;
=======
=======
>>>>>>> parent of 51a1bcd... ColorBliness Completion
    private int ColorBlindnessNumber = 0;

    public Button NormalButton;
    public Button BlindnessButton;
<<<<<<< HEAD
>>>>>>> parent of 51a1bcd... ColorBliness Completion
=======
>>>>>>> parent of 51a1bcd... ColorBliness Completion

    public bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(state);
        NormalButton.onClick.AddListener(NormalButtonClick);
        BlindnessButton.onClick.AddListener(ColorBindnessButtonClick);
    }
<<<<<<< HEAD
<<<<<<< HEAD
    //정상적인 번호 선택
    public static void NormalButtonClick()
=======
=======
>>>>>>> parent of 51a1bcd... ColorBliness Completion

    public void NormalButtonClick()
>>>>>>> parent of 51a1bcd... ColorBliness Completion
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
