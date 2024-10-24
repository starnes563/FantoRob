using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BauDespensa : MonoBehaviour
{
    public Sprite SpriteAberto;
    public SpriteRenderer SpriteRenderer;
    public AudioClip SomBau;
    public Dialogo FraseAbriu;
    [HideInInspector]
    public bool PodeAbrir = false;
    bool abriu = false;
    [HideInInspector]
    public CaixaDialogo CaixaDeDialogo;
    // Start is called before the first frame update
    void Start()
    {
        CaixaDeDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();       
    }

    // Update is called once per frame
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
        if (Input.GetMouseButtonDown(0) && PodeAbrir && !abriu)
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
            SpriteRenderer.sprite = SpriteAberto;
            GetComponent<AudioSource>().PlayOneShot(SomBau);
            if(!StoryEvents.BauDespensa)
            {
                CaixaDeDialogo.ReceberDialogo(FraseAbriu);
                StoryEvents.BauDespensa = true;
                PlayerObjects.EsqueletoEspecial++;
            }
           
        }
    }
}
