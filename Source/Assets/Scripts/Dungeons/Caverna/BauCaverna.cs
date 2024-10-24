using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BauCaverna : MonoBehaviour
{
    public Sprite SpriteAberto;
    public AudioClip SomBau;
    int MeuItem;
    public Dialogo PrimeiraPalavra;
    public Dialogo InventarioCheio;
    [HideInInspector]
    public bool PodeAbrir = false;
    bool abriu = false;
    [HideInInspector]
    public CaixaDialogo CaixaDeDialogo;

    // Start is called before the first frame update
    void Start()
    {
        CaixaDeDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        if (Random.Range(0, 101) > 25)
        {
            PodeAbrir = false;
            abriu = false;
            PrimeiraPalavra.LerOTexto(ManagerGame.Instance.Idm);
            //gerar loot
            MeuItem = Random.Range(0,8);
            //gerarnome
            string nome = Constructor.RetornarNome(2, MeuItem, 0, 0,0, 0);
            
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
            bool posso = true;
            int invent = 0;
            foreach (GameObject item in PlayerObjects.PlayerObjectsStatic.Itens)
            {
                invent += item.GetComponent<Item>().Quantidade;
            }
            if (invent > 9) { posso = false; }
            if(posso)
            {
                PodeAbrir = false;
                abriu = true;
                GetComponent<SpriteRenderer>().sprite = SpriteAberto;
                GetComponent<AudioSource>().PlayOneShot(SomBau);
                PlayerObjects.PlayerObjectsStatic.Itens[MeuItem].GetComponent<Item>().Quantidade += 1;
                CaixaDeDialogo.ReceberDialogo(PrimeiraPalavra);
            }
            else
            {
                CaixaDeDialogo.ReceberDialogo(InventarioCheio);
            }
            
        }
    }
}
