using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casecomchave : MonoBehaviour
{
    public Dialogo ReceberAChave;
    public Sprite SpriteAberto;
    public SpriteRenderer SpriteRenderer;
    public AudioClip SomAbrir;
    [HideInInspector]
    public CaixaDialogo CaixaDeDialogo;
    [HideInInspector]
    public bool PodeAbrir = false;
    bool mostrou = false;
    // Start is called before the first frame update
    void Start()
    {
        CaixaDeDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        PodeAbrir = false;
        mostrou = false;
        ReceberAChave.LerOTexto(ManagerGame.Instance.Idm);
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
    public void Clicou()
    {
        if (!CaixaDeDialogo.gameObject.activeSelf && !ManagerGame.Instance.Transitando && !ManagerGame.Instance.EmBatalha)
        {
            PodeAbrir = false;
            mostrou = true;
            SpriteRenderer.sprite = SpriteAberto;
            GetComponent<AudioSource>().PlayOneShot(SomAbrir);
            if (!StoryEvents.ChavePortaoFazenda)
            {
                StoryEvents.ChavePortaoFazenda = true;
                CaixaDeDialogo.ReceberDialogo(ReceberAChave);               
            }            
        }
    }
}
