using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GatilhoTesourado : MonoBehaviour
{
    public int ID;
    public Diretor Camera;
    public bool AoComecar = false;
    public bool AoClicar = false;
    public bool AoEntrar = false;
    bool entrou = false;
    public int NumeroDaCena;
    [HideInInspector]
    public bool mostrou = false;
    public List<GameObject> Personagens;
    public Vector3 PosicaodeInicio;
    [HideInInspector]
    public Walk player;
    public PlayableAsset Playable;
    public SequenciaCena Director;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerStatus.ControleDeCena < NumeroDaCena || StoryEvents.Tesourado[ID])
        {
            this.gameObject.SetActive(false);            
        }
        else
        {
            if(ID == 1 && !StoryEvents.Tesourado[0])
            {
                this.gameObject.SetActive(false);               
            }
            if (Personagens.Count > 0)
            {
                Personagens[0].gameObject.SetActive(true);
            }           
        }
    }
    void Update()
    {
        if (entrou && !ManagerGame.Instance.EmBatalha)
        {
            // if(Vector3.Distance(player.transform.position, PosicaodeInicio) < 0.5f)
            //{
            Iniciar();
            // }           
        }
        if (AoComecar && !mostrou && !ManagerGame.Instance.EmBatalha)
        {
            Iniciar();
        }
        if (AoClicar && Input.GetMouseButtonDown(0) && !ManagerGame.Instance.EmBatalha || AoClicar && Input.GetKeyDown("Fire1") && !ManagerGame.Instance.EmBatalha)
        {
            Iniciar();
        }
    }
    public void Iniciar()
    {
        if (Camera.PodeIniciar && !StoryEvents.Tesourado[ID])
        {
            if(ID ==0 || ID == 1 && StoryEvents.Tesourado[0])
            {
                StoryEvents.Tesourado[ID] = true;
                mostrou = true;
                Director.Começar(Playable);
                ManagerGame.Instance.AnalisarGatilho();
                Desativar();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && AoEntrar)
        {
            player = collision.GetComponent<Walk>();
            entrou = true;
            //if(Vector3.Distance(player.transform.position, PosicaodeInicio) > 0.5f)
            //{
            // player.AndarParaEssaPosicao(PosicaodeInicio, 0.5f);
            //}            
        }
    }
    public void Ativar()
    {
        if (PlayerStatus.ControleDeCena == NumeroDaCena)
        {

            this.gameObject.SetActive(true);
            if (Personagens.Count > 0)
            {
                Personagens[0].gameObject.SetActive(true);
            }
        }
    }
    public void Desativar()
    {
        this.gameObject.SetActive(false);

    }

}
