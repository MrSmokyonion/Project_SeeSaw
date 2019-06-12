using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCard : MonoBehaviour
{
    Sprite[] cardSprites;
    // Start is called before the first frame update
    void Start()
    {
        cardSprites = Resources.LoadAll<Sprite>("Card");
        StartCoroutine(changeCard());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator changeCard()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        while(true)
        {
            spriteRenderer.sprite = cardSprites [ Random.Range (0,cardSprites.Length) ];
            yield return new WaitForSeconds(1.0f);
        }
    }
}
