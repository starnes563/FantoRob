using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Presente : MonoBehaviour
{
    public Sprite SpriteAberto;
    public GameObject MeuPresente;
    public bool PodeAbrir = false;
    public FantoRob MeuFantorob;
    public GameObject MenuMostrar;
    public GameObject MainCamera;
    public Sprite SpriteDaCaixa;
    bool aberto = false;
    void Start()
    {
        MainCamera = GameObject.FindWithTag("MainCamera");        
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !MainCamera.transform.GetChild(0).gameObject.activeSelf && PodeAbrir)
        {
            Clicou();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            PodeAbrir = true;
        }
    }
    public void Clicou()        
    {
        if (!aberto)
        {
            Diretor.DesativarMenuPlayer();
            GameObject.FindWithTag("Player").GetComponent<Walk>().PararDeAndar();
            GameObject.FindWithTag("Player").GetComponent<Animator>().Play("Usando");
            MeuPresente.GetComponent<SpriteRenderer>().sprite = SpriteAberto;
            GameObject m = Instantiate(MenuMostrar) as GameObject;
            m.transform.position = GameObject.FindWithTag("Player").transform.position;
            FantoRob fanto = Instantiate(MeuFantorob) as FantoRob;
            fanto.Fisico = Instantiate(fanto.Fisico) as Weapon;
            //Montar Arma
            fanto.Fisico.MontarArma(fanto.Fisico.AttacksMin, 2);
           
            fanto.Fisico.ReceberAtaque(fanto.MovimentoJogador[0], 0);
            
            fanto.Fisico.ReceberAtaque(fanto.Fisico.MovimentosAmbos[0], 1);
            
            PlayerObjects.RobotsInUse.Add(fanto);
           
            PodeAbrir = false;
            Time.timeScale = 0f;
            aberto = true;
        }
    }
   
}
