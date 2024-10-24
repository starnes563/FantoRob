using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoTrocarObjeto : MonoBehaviour
{
    public GameObject Ativo;
    public GameObject Inativo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Ativo.SetActive(true);
            Inativo.SetActive(false);
        }
    }

}
