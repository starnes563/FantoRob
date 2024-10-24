using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GatilhoCutscene : MonoBehaviour
{
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
    public bool PassaAnim = false;
    public string Anim;
    //variaveis desafios
    public enum Condicao
    {
        SEM,
        CLASSIFICADO,
        DESCLASSIFICADO,
        DIA,
        MARCADORTRUE,
        MARCADORFALSE,
    }
    public Condicao MinhaCondicao = Condicao.SEM;
    public List<int> MeusDias = new List<int>();
    public int MeuMarcador;
    //se estiver marcado irá passar a cena mesmo sem dar play no Sequecia
    public bool AvancaCena;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerStatus.ControleDeCena != NumeroDaCena) 
        { 
            this.gameObject.SetActive(false); 
        }
        else
        {
            if (Personagens.Count > 0)
            {
               Personagens[0].gameObject.SetActive(true);
            }
        }
    }
    void Update()
    {        
        if(entrou && !ManagerGame.Instance.EmBatalha && AoEntrar)
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
        if(AoClicar && Input.GetButtonDown("Fire1") && !ManagerGame.Instance.EmBatalha && entrou)
        {           
            Iniciar();
        }
    }
   public void Iniciar()
    {
        if(Camera.PodeIniciar && PlayerStatus.ControleDeCena == NumeroDaCena)
        {
            if (posso())
            {
                if (PassaAnim) { player.GetComponent<Animator>().Play(Anim); }
                PlayerStatus.ControleDeCena++;
                Diretor.DesativarMenuPlayer();
                GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().MinhaSetaGPS.gameObject.SetActive(false);
                mostrou = true;
                Director.Começar(Playable);
                ManagerGame.Instance.AnalisarGatilho();
                Desativar();
            }
            else if(AvancaCena)
            {
                PlayerStatus.ControleDeCena++;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player = collision.GetComponent<Walk>();
            entrou = true;           
            //if(Vector3.Distance(player.transform.position, PosicaodeInicio) > 0.5f)
            //{
               // player.AndarParaEssaPosicao(PosicaodeInicio, 0.5f);
            //}            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {           
            entrou = false;                 
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
    bool posso()
    {
        bool pd = false;
        switch (MinhaCondicao)
        {
            case Condicao.SEM:
                pd = true;
                break;
            case Condicao.CLASSIFICADO:
                if (PlayerStatus.Posicao <= 8) { pd = true; }               
                break;
            case Condicao.DESCLASSIFICADO:
                if (PlayerStatus.Posicao > 8) { pd = true; }
                break;
            case Condicao.DIA:
                foreach(int d in MeusDias)
                {
                    if (d == PlayerStatus.DaysLeft) { pd = true;break; }
                }
                break;
            case Condicao.MARCADORTRUE:
                if (StoryEvents.MarcadoresDesafio[MeuMarcador]) { pd = true; }
                break;
            case Condicao.MARCADORFALSE:
                if (!StoryEvents.MarcadoresDesafio[MeuMarcador]) { pd = true; }
                break;
        }
        return pd;
    }

}
