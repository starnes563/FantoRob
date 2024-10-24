using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossuiChave : MonoBehaviour
{
    public bool possuichave = false;
    public Dialogo NaoPossui;
    public Dialogo Possui;    
    [HideInInspector]
    public CaixaDialogo CaixaDeDialogo;
    private Elevador porta;
    [HideInInspector]
    public bool PodeAbrir = false;
    bool mostrou = false;
    // Start is called before the first frame update
    void Start()
    {
        CaixaDeDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();        
        NaoPossui.LerOTexto(ManagerGame.Instance.Idm);
        Possui.LerOTexto(ManagerGame.Instance.Idm);
        PodeAbrir = false;
        mostrou = false;
    }
    // Update is called once per frame
    public void Clicou()
    {
        if (!CaixaDeDialogo.gameObject.activeSelf && !ManagerGame.Instance.Transitando &&!ManagerGame.Instance.EmBatalha)
        {
            PodeAbrir = false;
            mostrou = true;
            if (possuichave)
            {
                possuichave = false;
                CaixaDeDialogo.ReceberDialogo(Possui);
                GameObject.FindWithTag("BotaoAbrir").GetComponent<BotaoAbrirPorta>().possuichave = true;
            }
            else
            {
                CaixaDeDialogo.ReceberDialogo(NaoPossui);
            }
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
        if (Input.GetButtonDown("Fire1") && PodeAbrir && !mostrou)
        {
            Clicou();
        }
    }

}
