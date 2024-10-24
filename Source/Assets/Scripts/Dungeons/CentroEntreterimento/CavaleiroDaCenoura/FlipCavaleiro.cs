using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCavaleiro : MonoBehaviour
{
    public GameObject JogoCavaleiro;
    public GameObject CameraCenario;
    bool pode;
    bool abriu;
    Walk jogador;
    public AudioClip SomAbrir;
    public GameObject EscondeTransicao;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && !abriu && pode)
        {
            Diretor.DesativarMenuPlayer();
            EscondeTransicao.SetActive(true);
            JogoCavaleiro.SetActive(true);
            CameraCenario.SetActive(false);
            abriu = true;
            jogador.PararDeAndar();
            GetComponent<AudioSource>().PlayOneShot(SomAbrir);
            CaixaDeSom.Instancia.GetComponent<AudioSource>().Pause();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            pode = true;
            jogador = collision.GetComponent<Walk>();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pode = false;
            jogador = null;
            abriu = false;
        }
    }
}
