using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaoCaverna : MonoBehaviour
{
    public int ID;
    public GameObject PortaoFechado;
    public GameObject PortaoAberto;
    public AudioClip SomAbrir;
    [HideInInspector]
    public bool PodeAbrir = false;
    bool abriu = false;
    public Dialogo AbrirPortao;
    [HideInInspector]
    public CaixaDialogo CaixaDeDialogo;
    // Start is called before the first frame update
    void Start()
    {
        AbrirPortao.LerOTexto(ManagerGame.Instance.Idm);
        CaixaDeDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        PortaoFechado.gameObject.SetActive(false);
        PortaoAberto.gameObject.SetActive(false);
        if (StoryEvents.BoolCaverna[ID])
        {
            PortaoAberto.SetActive(true);
            abriu = true;
        }
        else
        {
            PortaoFechado.SetActive(true);
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
            StoryEvents.BoolCaverna[ID] = true;
            PortaoFechado.gameObject.SetActive(false);
            PortaoAberto.gameObject.SetActive(true);
            GetComponent<AudioSource>().PlayOneShot(SomAbrir);
            CaixaDeDialogo.ReceberDialogo(AbrirPortao);
        }
    }
}
