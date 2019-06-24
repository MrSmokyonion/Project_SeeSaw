using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEffect : MonoBehaviour
{
    public GameObject camera;

    private BravoX_L colorfilter;
    private CameraFilterPack_Drawing_Toon contour;
    private CameraFilterPack_Color_GrayScale reversal1;
    private CameraFilterPack_Color_Invert reversal2;
    private CameraFilterPack_Sharpen_Sharpen Sharp1;
    private CameraFilterPack_Color_BrightContrastSaturation Sharp2;

    private void Awake()
    {
        colorfilter = camera.GetComponent<BravoX_L>();
        contour = camera.GetComponent<CameraFilterPack_Drawing_Toon>();
        reversal1 = camera.GetComponent<CameraFilterPack_Color_GrayScale>();
        reversal2 = camera.GetComponent<CameraFilterPack_Color_Invert>();
        Sharp1 = camera.GetComponent<CameraFilterPack_Sharpen_Sharpen>();
        Sharp2 = camera.GetComponent<CameraFilterPack_Color_BrightContrastSaturation>();
    }

    //색약 필터 ================================================
    public void RedFilter(InteractionSlider inter)
    {
        colorfilter.Saturation_Red = inter.HorizontalSliderValue;
    }
    public void GreenFilter(InteractionSlider inter)
    {
        colorfilter.Saturation_Green = inter.HorizontalSliderValue;
    }
    public void BlueFilter(InteractionSlider inter)
    {
        colorfilter.Saturation_Blue = inter.HorizontalSliderValue;
    }

    //외곽선 =================================================
    public void ContourFilter(bool onoff)
    {
        contour.enabled = onoff;
    }

    //색반전 =================================================
    public void ReversalFilter(bool onoff)
    {
        reversal1.enabled = onoff;
        reversal2.enabled = onoff;
    }

    //선명도
    public void SharpFilter(bool onoff)
    {
        Sharp1.enabled = onoff;
        Sharp2.enabled = onoff;
    }
}
