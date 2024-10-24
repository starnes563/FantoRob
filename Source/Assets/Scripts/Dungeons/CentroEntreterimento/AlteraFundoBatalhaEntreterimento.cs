using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlteraFundoBatalhaEntreterimento : MonoBehaviour
{
    public SpriteRenderer Oval;
    public SpriteRenderer Fundo;
    public Sprite OvalBase;
    public Sprite FundoBase;
    public Sprite OvalAltera;
    public Sprite FundoAltera;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Oval.sprite = OvalAltera;
            Fundo.sprite = FundoAltera;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Oval.sprite = OvalBase;
            Fundo.sprite = FundoBase;
        }
    }
}
