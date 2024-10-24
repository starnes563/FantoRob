using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoAcionarVagoneta : MonoBehaviour
{
    public Vagoneta Vagoneta;
    bool podeAcionar = false;
    [Range(-1, 1)]
    public int X;
    [Range(-1, 1)]
    public int Y;
    GameObject player;
    public bool Estacionado = false;   
    // Update is called once per frame
    void Update()
    {
        if(podeAcionar && Input.GetButtonDown("Fire1")&&!ManagerGame.Instance.EmBatalha && !ManagerGame.Instance.Transitando)
        {
            acionar();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            podeAcionar = true;
            player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            podeAcionar = false;
            player = null;
        }
    }
    void acionar()
    {
        if(Estacionado&& StoryEvents.BoolCaverna[1])
        {
            player.GetComponent<Walk>().PararDeAndar();
            player.GetComponent<BoxCollider2D>().isTrigger = true;
            Vagoneta.X = X;
            Vagoneta.Y = Y;
            Vagoneta.Jogador = player;
            Estacionado = false;                
        }
     
    }
}
