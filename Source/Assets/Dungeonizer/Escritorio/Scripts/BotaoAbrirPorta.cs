using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoAbrirPorta : MonoBehaviour
{
    [HideInInspector]
    public bool possuichave = false;
    public Dialogo AbriuPorta;
    public SpriteRenderer SpriteRenderer;
    public Sprite CaixaAberta;
    [HideInInspector]
    public CaixaDialogo CaixaDeDialogo;
    private Elevador porta;
    [HideInInspector]
    public bool PodeAbrir = false;
    bool mostrou = false;
    public bool UltimoAndar = false;
    // Start is called before the first frame update
    void Start()
    {
        CaixaDeDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        AbriuPorta.LerOTexto(CaixaDeDialogo.Idioma);       
        PodeAbrir = false;
        mostrou = false;
    }
    // Update is called once per frame
    public void Clicou()
    {
        PodeAbrir = false;
        mostrou = true;
        if (possuichave)
        {
            possuichave = false;
            CaixaDeDialogo.ReceberDialogo(AbriuPorta);
            porta = GameObject.FindWithTag("PortaSubida").GetComponent<Elevador>();
            porta.AbrirPorta();
            SpriteRenderer.sprite = CaixaAberta;
        }    
        if(UltimoAndar)
        {
            StoryEvents.UltimoandarLiberado = true;           
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
        if (Input.GetMouseButtonDown(0) && PodeAbrir && !mostrou)
        {
            Clicou();
        }
    }

}
