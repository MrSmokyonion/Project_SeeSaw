using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    [Header("Objects")]
    public GameObject menu;
    public GameObject btn;
    public GameObject camera;

    [Header("Menu")]
    public GameObject sharpMenu;
    public GameObject contourMenu;
    public GameObject reversalMenu;
    public GameObject colorMenu;
    public GameObject focusMenu;

    //[Space(10)]
    //public GameObject Menu1;
    //public GameObject Menu2;

    [Header("float"), Range(0.2f, 0.8f)]
    public float distance;


    //Menu's Animators
    private Animator menuAni;
    private Animator sharpAni;
    private Animator contourAni;
    private Animator reversalAni;
    private Animator colorAni;
    private Animator focusAni;

    private bool isActive;
    private bool menuActive; // 세부 메뉴가 켜져있는지 나타내는값. true => 하나라도 떠있음. false => 아무것도 안 떠있음.
    private bool delay; // 메뉴 입력 딜레이. true => 딜레이중. false => 입력 가능
    private int State; //현재 몇번 메뉴에 있는지 나타내는 값. 0 = 처음.


    private void Awake()
    {
        menuAni = menu.GetComponent<Animator>();
        sharpAni = sharpMenu.GetComponent<Animator>();
        contourAni = contourMenu.GetComponent<Animator>();
        reversalAni = reversalMenu.GetComponent<Animator>();
        colorAni = colorMenu.GetComponent<Animator>();
        focusAni = focusMenu.GetComponent<Animator>();
    }

    void Start()
    {
        isActive = false;
        menuActive = false;
        delay = false;
        State = 0;
        btn.SetActive(false);
        menu.SetActive(false);
    }

    //=======================================================
    //버튼 활/비활성화. 손에 생기는 버튼에 대한 메소드.
    public void ShowBtn()
    {
        if (menuActive)
            return;

        btn.SetActive(true);
    }

    public void HideBtn()
    {
        btn.SetActive(false);
    }
    //=======================================================


    //=======================================================
    //온/오프 버튼. 메뉴가 띄워질지 안띄워질지 여기서 갈림.
    public void OnOff()
    {
        if (delay)
            return;
        StartCoroutine(DelayForMenu());

        if (isActive == false)
        {
            TurnOnMenu();
            isActive = true;
        }
        else if(isActive == true)
        {
            TurnOffMenu();
            isActive = false;
        }
    }

    //Menu 띄우기 활/비활성화
    public void TurnOnMenu()
    {
        menu.transform.position = (camera.transform.forward * distance) + new Vector3(0, camera.transform.position.y, 0);
        Debug.Log(camera.transform.forward);
        menu.transform.rotation = Quaternion.Euler(-90, camera.transform.eulerAngles.y, 0);
        menu.SetActive(true);
        menuAni.SetTrigger("Menu On");
    }

    public void TurnOffMenu()
    {
        menuAni.SetTrigger("Menu Off");
        State = 0;
        StartCoroutine(WaitForAnimation(menuAni));
    }

    IEnumerator WaitForAnimation(Animator animator)
    {
        while(false == animator.IsInTransition(0))
        {
            yield return new WaitForEndOfFrame();
        }
        //menu.SetActive(false);
    }
    //=======================================================


    //=======================================================
    //다음, 이전버튼 메소드들
    public void NextMenu()
    {
        if (delay)
            return;
        StartCoroutine(DelayForMenu());

        switch (State)
        {
            case 0: menuAni.SetTrigger("State 2"); State = 2; break;
            case 2: menuAni.SetTrigger("State 3"); State = 3; break;
            case 3: menuAni.SetTrigger("State 1"); State = 1; break;
            case 1: menuAni.SetTrigger("State 2"); State = 2; break;
        }
    }
    public void PrevMenu()
    {
        if (delay)
            return;
        StartCoroutine(DelayForMenu());

        switch (State)
        {
            case 0: menuAni.SetTrigger("State 3"); State = 3; break;
            case 2: menuAni.SetTrigger("State 1"); State = 1; break;
            case 3: menuAni.SetTrigger("State 2"); State = 2; break;
            case 1: menuAni.SetTrigger("State 3"); State = 3; break;
        }
    }
    //=======================================================
    //메인 메뉴 안에 기능들에 대한 메소드들. MenuAction에 뭐가 보내지는지에 따라서 결정됨.

    public void MenuOn(string who)
    {
        if (delay)
            return;
        StartCoroutine(DelayForMenu());

        isActive = false;
        menuActive = true;

        switch (who)
        {
            case "Sharp": MenuAction(sharpMenu, sharpAni); break;
            case "Contour": MenuAction(contourMenu, contourAni); break;
            case "Reversal": MenuAction(reversalMenu, reversalAni); break;
            case "Color": MenuAction(colorMenu, colorAni); break;
            case "Focus": MenuAction(focusMenu, focusAni); break;
        }


    }
    public void MenuOff(string who)
    {
        if (delay)
            return;
        StartCoroutine(DelayForMenu());

        isActive = false;
        menuActive = false;

        switch (who)
        {
            case "Sharp": sharpAni.SetTrigger("Off"); break;
            case "Contour": contourAni.SetTrigger("Off"); break;
            case "Reversal": reversalAni.SetTrigger("Off"); break;
            case "Color": colorAni.SetTrigger("Off"); break;
            case "Focus": focusAni.SetTrigger("Off"); break;
        }
    }

    private void MenuAction(GameObject menu, Animator ani)
    {
        TurnOffMenu();

        menu.transform.position = (camera.transform.forward * distance) + new Vector3(0, camera.transform.position.y, 0);
        menu.transform.rotation = Quaternion.Euler(-90, camera.transform.eulerAngles.y, 0);
        menu.SetActive(true);
        ani.SetTrigger("On");
    }

    //=============================================================
    private IEnumerator DelayForMenu()
    {
        delay = true;
        yield return new WaitForSeconds(1.0f);
        delay = false;
    }
}
