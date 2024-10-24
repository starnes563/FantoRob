using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bote : MonoBehaviour
{
    [HideInInspector]
    public CaixaDialogo CaixaDeDialogo;
    public bool BoteBaixo;
    public Vector3 PosicaoDesembarque;
    public int CenaDesembarque;
    public string AnimacaoDesembarque;
    bool PodeAbrir = false;
    public AudioClip Usar;
    // Start is called before the first frame update
    void Start()
    {
        CaixaDeDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        if (BoteBaixo != StoryEvents.ChavePortaoFazenda) { gameObject.SetActive(false); }
    }

    // Update is called once per frame    
    public void Clicou()
    {
        if (!CaixaDeDialogo.gameObject.activeSelf && !ManagerGame.Instance.Transitando && !ManagerGame.Instance.EmBatalha)
        {            
            Diretor.DesativarMenuPlayer();
            PlayerStatus.ProximaAnimacao = AnimacaoDesembarque;
            PlayerStatus.NextHeroPosition = PosicaoDesembarque;
            ManagerGame.Instance.SceneToLoad = CenaDesembarque;
            if (!StoryEvents.ChavePortaoFazenda) { StoryEvents.ChavePortaoFazenda = true; }
            GetComponent<AudioSource>().PlayOneShot(Usar);            
            PodeAbrir = false;
            TrocarCena();
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
        if (Input.GetButtonDown("Fire1") && PodeAbrir)
        {
            Clicou();
        }
    }
    public void TrocarCena()
    {
        GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();
    }
}
