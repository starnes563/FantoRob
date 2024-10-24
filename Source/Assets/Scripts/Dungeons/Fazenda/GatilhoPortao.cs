using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoPortao : MonoBehaviour
{
    public Animator AnimatorPortao;    
    [HideInInspector]
    public bool PodeAbrir = false;
    bool abriuportao = false;
    public AudioClip SomPortaoAbrindo;
    // Start is called before the first frame update
    void Start()
    {       
        abriuportao = false;
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
        if (Input.GetButtonDown("Fire1") && PodeAbrir && !abriuportao && StoryEvents.ChavePortaoFazenda)
        {
            Clicou();
        }
    }
    public void Clicou()
    {
        if (!ManagerGame.Instance.Transitando && !ManagerGame.Instance.EmBatalha)
        {
            PodeAbrir = false;
            abriuportao = true;
            AnimatorPortao.SetTrigger("Moverportao");
            GetComponent<AudioSource>().PlayOneShot(SomPortaoAbrindo);
        }
    }
}
