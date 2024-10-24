using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcionarMenu : MonoBehaviour
{
    bool podeabrir = false;
    public GameObject Menu;
    public GameObject Comprar2;
    public AudioSource AudioSource;
    public AudioClip SomConfirmar;
    private Walk Player;
    public bool Iracema;
    // Update is called once per frame
    void Update()
    {
        if (podeabrir && Input.GetButtonDown("Fire1") && !ManagerGame.Instance.EmBatalha && !Menu.activeSelf && !Comprar2.activeSelf)
        {            
                Diretor.DesativarMenuPlayer();
                AudioSource.PlayOneShot(SomConfirmar);
                Player.PararDeAndar();
                Menu.SetActive(true);                        
        }        
    }   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            podeabrir = true;
            Player = collision.GetComponent<Walk>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(podeabrir)
        {
            podeabrir = false;
        }
    }
    public void LiberarAndarPlayer()
    {
        Player.LiberarAndar();
    }
}
