using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSwitch : MonoBehaviour
{
    public GameObject BackGround;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            BackGround.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            BackGround.SetActive(false);
        }
    }
}

