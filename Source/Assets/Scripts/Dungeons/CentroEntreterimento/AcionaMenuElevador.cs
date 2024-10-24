using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcionaMenuElevador : MonoBehaviour
{
    public GameObject MenuElevador;
    public AudioSource Source;
    public AudioClip Som;
    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Diretor.DesativarMenuPlayer();
            collision.GetComponent<Walk>().PararDeAndar();
            collision.GetComponent<Animator>().Rebind();
            collision.GetComponent<Animator>().Play("IdleFrente");
            MenuElevador.SetActive(true);
            Source.PlayOneShot(Som);
        }
    }   
}
