using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSystem : MonoBehaviour
{
    [Header("Objects")]
    public GameObject menu;
    public GameObject ui;
    public GameObject toggle;
    public GameObject camera;

    [Header("float"), Range(0.2f, 0.8f)]
    public float distance;

    private Animator menuAni;

    void Start()
    {
        menuAni = menu.GetComponent<Animator>();

        ToggleMenu(false);
        menu.SetActive(false);
    }

    //UI Toggle 이벤트
    public void ToggleMenu(bool tog)
    {
        if(tog == true)
        {
            toggle.SetActive(false);
            ui.SetActive(true);
        }
        else if(tog == false)
        {
            toggle.SetActive(true);
            ui.SetActive(false);
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
}
