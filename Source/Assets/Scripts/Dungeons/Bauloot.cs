using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bauloot : MonoBehaviour
{
   public Sprite SpriteAberto;
    public AudioClip SomBau;
    Loot MeuLoot;
    public Dialogo PrimeiraPalavra;
    [HideInInspector]
    public bool PodeAbrir = false;
    bool abriu = false;
    [HideInInspector]
    public CaixaDialogo CaixaDeDialogo;    

    // Start is called before the first frame update
    void Start()
    {        
        CaixaDeDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        if (Random.Range(0, 101) > 30)
        {
            PodeAbrir = false;
            abriu = false;
            PrimeiraPalavra.LerOTexto(ManagerGame.Instance.Idm);
            //gerar loot
            MeuLoot = GameObject.FindWithTag("Regiao").GetComponent<RegionData>().AllLoot[Random.Range(0, ManagerGame.Instance.Regiao.PossibleLoot.Count)];
            //gerarnome
            string nome = "";
            switch (MeuLoot.MeuTipo)
            {
                case Loot.TipodeLoot.ITEMCONSTRUIR:
                    nome = Constructor.RetornarNome(6, 0, 0, 0, MeuLoot.Propriedade, 0);
                    break;
                case Loot.TipodeLoot.PENTEVAZIO:
                    nome = Constructor.RetornarNome(1, 0, 0, 0, 0, 0);
                    break;
                case Loot.TipodeLoot.PENTECHEIO:
                    nome = Constructor.RetornarNome(1, 0, 0, 0, 0, 0);
                    break;
                case Loot.TipodeLoot.CIRCUITO:
                    nome = Constructor.RetornarNome(5, 0, 0, MeuLoot.Propriedade, 0, 0);
                    break;
                case Loot.TipodeLoot.SILICIO:
                    nome = Constructor.RetornarNome(0, 0, 0, 0, 0, 0);
                    break;
                case Loot.TipodeLoot.PARTEROBO:
                    nome = Constructor.RetornarNome(7, 0, 0, 0, 0, MeuLoot.Propriedade);
                    break;
            }
            PrimeiraPalavra.Sentencas[0] = PrimeiraPalavra.Sentencas[0] + " " + nome;
        }
        else
        {
            PodeAbrir = false;
            abriu = true;
            GetComponent<SpriteRenderer>().sprite = SpriteAberto;
        }
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
        if (Input.GetButtonDown("Fire1") && PodeAbrir && !abriu)
        {
            Clicou();
        }
    }
    public void Clicou()
    {
        if (!CaixaDeDialogo.gameObject.activeSelf && !ManagerGame.Instance.Transitando && !ManagerGame.Instance.EmBatalha)
        {
            PodeAbrir = false;
            abriu = true;
            GetComponent<SpriteRenderer>().sprite = SpriteAberto;
            GetComponent<AudioSource>().PlayOneShot(SomBau);
            MeuLoot.MeADicionaAoInventario();               
            CaixaDeDialogo.ReceberDialogo(PrimeiraPalavra);               
        }
    }
}
