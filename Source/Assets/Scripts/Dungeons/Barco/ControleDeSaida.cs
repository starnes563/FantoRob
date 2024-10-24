using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleDeSaida : MonoBehaviour
{
    public List<Quadrado> Paneis = new List<Quadrado>();
    public int numeroDeNotas = 4;
    private List<int> padrao = new List<int>();
    public Dialogo Instrucao;
    public Dialogo ErrouNota;
    public Dialogo AcertouNota;
    private bool jadisseDialogo = false;
    private CaixaDialogo CaixaDialogo;
    [HideInInspector]
    public bool TurnoPlayer = false;
    [HideInInspector]
    public bool TurnoComputador = false;    
    private IEnumerator tocar;
    private bool acertou = false;
    int controle = 0;
    int proximoQuadrado = 0;
    int ultimoquadrado = 0;
    public int Ordem;
    Walk Player;
    bool tocou;
    public GameObject PainelSemNada;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Walk>();
        CaixaDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        Instrucao.LerOTexto(ManagerGame.Instance.Idm);
        ErrouNota.LerOTexto(ManagerGame.Instance.Idm);
        AcertouNota.LerOTexto(ManagerGame.Instance.Idm);       
        StartCoroutine(CriarPadrao());
    }
    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator CriarPadrao()
    {
        for (int i = 0; i < numeroDeNotas; i++)
        {            
            while(proximoQuadrado == ultimoquadrado)
            {
                proximoQuadrado = Random.Range(0, 5);
                yield return null;
            }
            padrao.Add(proximoQuadrado);
            ultimoquadrado = proximoQuadrado;
        }
    }
    IEnumerator TocarPadrao()
    {
        Player.PararDeAndar();
        TurnoComputador = true;
        TurnoPlayer = false;
        int p = 0;
        while (CaixaDialogo.gameObject.activeSelf == true)
        {
            yield return null;
        }
        while(p<numeroDeNotas)
        {
            Paneis[padrao[p]].Acender();
            p++;
            yield return new WaitForSeconds(1.2f);
        }
        TurnoComputador = false;
        TurnoPlayer = true;
        Player.CanIWalk = true;
        tocou = true;
    }
    void FalarInstruncao()
    {
        if (!jadisseDialogo) { CaixaDialogo.ReceberDialogo(Instrucao); jadisseDialogo = true; }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !acertou && StoryEvents.PortasBarco==Ordem && !tocou)
        {
            Diretor.DesativarMenuPlayer();
            FalarInstruncao();
            if (tocar != null) { StopCoroutine(tocar); }
            tocar = TocarPadrao();
            StartCoroutine(tocar);
        }

    }  
    public void ReceberValor(int valor)
    {
        if(valor == padrao[controle])
        {
            controle++;
            if(controle==numeroDeNotas)
            {
                controle = 1;
                AcertouTodos();
            }
        }
        else
        {
            controle = 1;
            StartCoroutine(Errou());
        }
    }
    void AcertouTodos()
    {       
        CaixaDialogo.ReceberDialogo(AcertouNota);
        acertou = true;
        StoryEvents.PortasBarco++;
        TurnoComputador = false;
        TurnoPlayer = false;
    }
   IEnumerator Errou()
   {
        Walk p = GameObject.FindWithTag("Player").GetComponent<Walk>();
        p.PararDeAndar();
        CaixaDialogo.ReceberDialogo(ErrouNota);
        while (CaixaDialogo.gameObject.activeSelf == true)
        {
            yield return null;
        }
        atacar();
        p.CanIWalk = true;
        TurnoComputador = false;
        TurnoPlayer = false;
    }
    void atacar()
    {
        PainelSemNada.SetActive(true);
        ManagerGame.Instance.StartBattle(GetComponent<NPCBattle>().Tipo, GetComponent<NPCBattle>());        
    }
}
