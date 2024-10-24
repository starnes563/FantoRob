using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaquinaCartao : MonoBehaviour
{
    public int NivelNecessario;
    public int NivelProximo;
    Animator animator;
    AudioSource Source;
    public AudioClip Colocando;
    public AudioClip Cartao;
    Walk Player;
    bool pode;
    bool usou;
    public List<Text> Numeros = new List<Text>();
    public Dialogo ComecoFrase;
    public Dialogo Impossivel;
    public Dialogo Quebrado;
    public bool EuQuebrado;
    [HideInInspector]
    public CaixaDialogo CaixaDeDialogo;
    // Start is called before the first frame update
    void Start()
    {
        CaixaDeDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
        animator = GetComponent<Animator>();
        Source = GetComponent<AudioSource>();
        if (Numeros.Count > 0)
        {
            foreach (Text t in Numeros)
            {
                t.text = NivelProximo.ToString();
            }
        }
        ComecoFrase.LerOTexto(ManagerGame.Instance.Idm);
        ComecoFrase.Sentencas[0] = ComecoFrase.Sentencas[0] + " " + NivelProximo.ToString();
        Impossivel.LerOTexto(ManagerGame.Instance.Idm);
        Quebrado.LerOTexto(ManagerGame.Instance.Idm);
    }

    // Update is called once per frame
    void Update()
    {
        if(pode && Input.GetButtonDown("Fire1")&&!usou)
        {
            usou = true;
            AumentarCartao();
        }
    }
    void AumentarCartao()
    {
        Diretor.DesativarMenuPlayer();
        if (StoryEvents.NivelCartao == NivelNecessario && StoryEvents.DesafiosCamp[6].Itemdesafio) 
        { 
            StoryEvents.NivelCartao = NivelProximo;
            CaixaDeDialogo.ReceberDialogo(ComecoFrase);
            UICardLevel.Instance.Atualiza();
        }
        else
        {
            CaixaDeDialogo.ReceberDialogo(Impossivel);
        }
        animator.SetTrigger("Cartao");
    }
    public void Colocar()
    {
        Source.PlayOneShot(Colocando);
    }
    public void SomAumentar()
    {
        Source.Stop();
        Source.PlayOneShot(Cartao);
        Fim();
    }
    public void Fim()
    {
        Player.LiberarAndar();
        UICardLevel.Instance.Atualiza();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Player = collision.GetComponent<Walk>();
            pode = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player = null;
            pode = false;
            usou = false;
        }
    }
    public void AumetarCutscene()
    {
        if (StoryEvents.NivelCartao == NivelNecessario && StoryEvents.DesafiosCamp[6].Itemdesafio)
        {
            StoryEvents.NivelCartao = NivelProximo;
        }
    }
}
