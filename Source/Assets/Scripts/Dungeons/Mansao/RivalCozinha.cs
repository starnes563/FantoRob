using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalCozinha : MonoBehaviour
{
    public CaixaDialogo CaixaDialogo;
    public Dialogo NaoTemRoupa;
    public Dialogo TemRoupa;
    public Vector3 PosicInst;
    bool pode;
    bool transitar;
    // Start is called before the first frame update
    void Start()
    {
        NaoTemRoupa.LerOTexto(ManagerGame.Instance.Idm);
        TemRoupa.LerOTexto(ManagerGame.Instance.Idm);
    }

    // Update is called once per frame
    void Update()
    {
        if(pode && Input.GetButtonDown("Fire1") && !CaixaDialogo.gameObject.activeSelf)
        {
            Falar();
        }
        if(transitar && !CaixaDialogo.gameObject.activeSelf)
        {
            TrocarCena();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pode = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pode = false;
        }
    }
    void Falar()
    {
        if(StoryEvents.DesafiosCamp[5].Itemdesafio)
        {
            transitar = true;
            CaixaDialogo.ReceberDialogo(TemRoupa);            
        }
        else
        {
            CaixaDialogo.ReceberDialogo(NaoTemRoupa);
        }
    }
    void TrocarCena()
    {
        transitar = false;
        Diretor.DesativarMenuPlayer();
        PlayerStatus.ProximaAnimacao = "IdleFrente";
        PlayerStatus.NextHeroPosition = PosicInst;
        ManagerGame.Instance.SceneToLoad = 86;
        GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();
    }
}
