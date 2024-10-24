using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertaStealth : MonoBehaviour
{
    public bool Aleatorio;
    public Animator AnimatorFantomascara;
    private Walk player;
    public CaixaDialogo CaixadeDialogo;
    public Dialogo FalaAvistou;
    bool avistou = false;
    // Start is called before the first frame update
    // Update is called once per frame
    private void Start()
    {
        FalaAvistou.LerOTexto(ManagerGame.Instance.Idm) ;
    }
    void Update()
    {
        if(avistou && !CaixadeDialogo.gameObject.activeSelf)
        {
            avistou = false;
            ManagerGame.Instance.SceneToLoad = 133;
            GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player = collision.GetComponent<Walk>();
            avistado();
        }
    }
    private void avistado()
    {
        //parar o jogador
        player.PararDeAndar();
        //se precisar mudar o animator
        if (!Aleatorio) { AnimatorFantomascara.Play("IdleCostas"); }
        //falar
        CaixadeDialogo.ReceberDialogo(FalaAvistou);
        //botar em status para carregar os dados salvo
        avistou = true;
    }
}
