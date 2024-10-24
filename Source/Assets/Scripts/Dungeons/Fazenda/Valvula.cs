using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valvula : MonoBehaviour
{
    public Animator Valve;
    public GameObject SomValvula;
    public GameObject Motor;
    public GameObject Agua;   
    bool pode;
    Walk player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Walk>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && pode && Input.GetButtonDown("Fire1"))
        {
            ativaValvula();
            pode = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && StoryEvents.DesafiosCamp[1].Itemdesafio)
        {
            pode = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (pode) { pode = false; }
    }
    void ativaValvula()
    {
        Diretor.DesativarMenuPlayer();
        player.PararDeAndar();
        pode = false;        
        Valve.SetTrigger("Ativar");
    }
    public void TocarSomValvula()
    {
        SomValvula.SetActive(true);        
    }
    public void SomMotor()
    {
        Motor.SetActive(true);       
    }
    public void SomAgua()
    {
        Agua.SetActive(true);     
    }
    public void Finalizar()
    {
        switch(StoryEvents.NivelAguaBaixo)
        {
            case true:
                StoryEvents.NivelAguaBaixo = false;
                break;
            case false:
                StoryEvents.NivelAguaBaixo = true;
                break;
        }
        SomValvula.SetActive(false);
        Motor.SetActive(false);
        Agua.SetActive(false);
        pode = true;
        player.CanIWalk = true;
    }
}
