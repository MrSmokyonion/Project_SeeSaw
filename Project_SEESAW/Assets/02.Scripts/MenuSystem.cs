using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    [Header("Objects")]
    public GameObject menu;
    public GameObject btn;
    public GameObject camera;

    [Space(10)]
    public GameObject Menu1;
    public GameObject Menu2;

    [Header("float"), Range(0.2f, 0.8f)]
    public float distance;

    private Animator menuAni;
    private bool isActive;
    private bool isChange; //menu1, menu2 전환을 위한 논리값

    void Start()
    {
        menuAni = menu.GetComponent<Animator>();
        isActive = false;
        isChange = false;
        btn.SetActive(false);
        menu.SetActive(false);
    }

    //버튼 활/비활성화
    public void ShowBtn()
    {
        btn.SetActive(true);
    }

    public void HideBtn()
    {
        btn.SetActive(false);
    }

    //온/오프 버튼
    public void OnOff()
    {
        if(isActive == false)
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

    //Menu 활/비활성화
    public void TurnOnMenu()
    {
        menu.transform.position = (camera.transform.forward * distance) + new Vector3(0, camera.transform.position.y, 0);
        Debug.Log(camera.transform.forward);
        menu.transform.rotation = Quaternion.Euler(-90, camera.transform.eulerAngles.y, 0);
        menu.SetActive(true);
        menuAni.SetBool("isTurnOn", true);
    }

    public void TurnOffMenu()
    {
        menuAni.SetBool("isTurnOn", false);
        menuAni.SetBool("changeMenu", false);
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

    public void changeMenu()
    {
        if(isChange == false)
        {
            Menu1.SetActive(false);
            Menu2.SetActive(true);
            menuAni.SetBool("changeMenu", true);
        }
        else if( isChange == true)
        {
            Menu1.SetActive(true);
            Menu2.SetActive(false);
            menuAni.SetBool("changeMenu", false);
        }
        isChange = !isChange;
    }
}
