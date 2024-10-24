using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonde : MonoBehaviour
{       
    [HideInInspector]
    public CaixaDialogo CaixaDeDialogo;
    [HideInInspector]
    public bool PodeAbrir = false;
    public Animator AnimatorBonde;
    public string AnimacaoEmbarque;
    public Vector3 PosicaoDesembarque;
    public int CenaDesembarque;
    public string AnimacaoDesembarque;
    // Start is called before the first frame update
    void Start()
    {
        CaixaDeDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        PodeAbrir = false;
    }
    public void Clicou()
    {
        if (!CaixaDeDialogo.gameObject.activeSelf && !ManagerGame.Instance.Transitando && !ManagerGame.Instance.EmBatalha)
        {
            Diretor.DesativarMenuPlayer();
            PlayerStatus.ProximaAnimacao = AnimacaoDesembarque;
            PlayerStatus.NextHeroPosition = PosicaoDesembarque;
            ManagerGame.Instance.SceneToLoad = CenaDesembarque;
            if (!StoryEvents.BondeLibeardo) { StoryEvents.BondeLibeardo = true; }
            PodeAbrir = false;
            AnimatorBonde.Play(AnimacaoEmbarque);
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
