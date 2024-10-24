using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoEntradaAlternativa : MonoBehaviour
{
    public GameObject MenuEntrada;
    bool PodeAbrir = false;
    public Animator Animator;
    public string Animacao;
    public void Clicou()
    {        
        Animator.Play(Animacao);
        Diretor.DesativarMenuPlayer();
        GameObject.FindWithTag("Player").GetComponent<Walk>().PararDeAndar();
        MenuEntrada.SetActive(true);        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            PodeAbrir = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            PodeAbrir = false;
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && PodeAbrir && !MenuEntrada.activeSelf)
        {
            Clicou();
        }
    }
}
