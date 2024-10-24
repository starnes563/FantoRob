using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoNpc : MonoBehaviour
{
    public NPCBattle MeuNpc;
    bool podefalar = false;
    Walk Player;
    public string Animacao;
    [HideInInspector]
    public CaixaDialogo CaixaDialogo;
    // Update is called once per frame
    private void Start()
    {
        CaixaDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
    }
    void Update()
    {
        if(podefalar && Input.GetButtonDown("Fire1") && !CaixaDialogo.gameObject.activeSelf&&!ManagerGame.Instance.EmBatalha && !ManagerGame.Instance.Transitando
            || podefalar && Input.GetButtonDown("Fire1") && !CaixaDialogo.gameObject.activeSelf&&!ManagerGame.Instance.EmBatalha&&!ManagerGame.Instance.Transitando)
        {
            Diretor.DesativarMenuPlayer();
            MeuNpc.Falar(Player);
            if(Animacao != "")
            {
                MeuNpc.TocarAnimacao(Animacao);
            }           
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {       
        if (!MeuNpc.Persegue && other.tag == "Player" && other.GetComponent<Walk>().CanIWalk)
        {
            Player = other.GetComponent<Walk>();
            podefalar = true;            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (podefalar) { podefalar = false; }
    }
}
