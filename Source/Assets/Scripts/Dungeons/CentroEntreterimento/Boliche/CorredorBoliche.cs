using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorredorBoliche : MonoBehaviour
{
    public GameObject ColliderTrava;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bola")
        {
            ColliderTrava.SetActive(true);
        }
    }
}
