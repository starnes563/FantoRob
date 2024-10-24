using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leticia : MonoBehaviour
{
    public List<Construivel> FantoCmp = new List<Construivel>();
    public List<int> ContagemFantorob = new List<int>(7);
    public List<Construivel> FisicoCmp = new List<Construivel>();
    public List<int> ContagemFisico = new List<int>(7);
    public QuadroConstruir QuadroConstruir;
    public Button BotaoSelecionar;
    public Image SpacerConstruirRobo;
    public Image SpacerConstruirNucleo;
    private List<GameObject> botoes = new List<GameObject>();
    public MenuSelecionarWp MenuSelecaoFisico;

    //para funconar a musica
    public static AudioSource Source;
    public static AudioClip SomConfirmar;
    public static AudioClip SomDesistir;
    public static AudioClip SomNaoPode;

    public AudioClip SomParaNao;
    public AudioClip SomParaCom;
    public AudioClip SomParaDes;
    public List<string> FalaInicial;
    public List<string> FalaMontar;
    public Fala Dialogo;
    public Animator AnimatorLeticia;
    public string PosicaoComprando;
    public string PosicaoOciosa;
    public GameObject Pai;
    // Start is called before the first frame update
    void OnEnable()
    {
        this.transform.position = new Vector3(this.transform.position.x, 8.48f);
        LeanTween.moveLocalY(this.gameObject, 3.82f, 0.4f);
        AnimatorLeticia.Play(PosicaoComprando);        
        Dialogo.DigitarNaTela(FalaMontar[ManagerGame.Instance.Idm], "Letícia");        
            SomConfirmar = SomParaCom;
            SomDesistir = SomParaDes;
            SomNaoPode = SomParaNao;
            Source = GetComponent<AudioSource>();
            FazerMenu();
        //}
    }

    public void FazerMenu()
    {
        if (botoes.Count > 0)
        {
            foreach (GameObject bo in botoes)
            {
                Destroy(bo);
            }
            botoes.Clear();
        }       
        for (int i = 0; i < ContagemFantorob[PlayerStatus.Estrelas]; i++)
        {
            Button bt = Instantiate(BotaoSelecionar, SpacerConstruirRobo.transform) as Button;
            botoes.Add(bt.gameObject);
            bt.GetComponent<BotaoConstruir>().Criar(FantoCmp[i], QuadroConstruir,MenuSelecaoFisico, this);
        }      
        if (ContagemFisico.Count>0)
        {            
            for (int i = 0; i < ContagemFisico[PlayerStatus.Estrelas]; i++)
            {                
                Button bt = Instantiate(BotaoSelecionar, SpacerConstruirNucleo.transform) as Button;
                botoes.Add(bt.gameObject);
                bt.GetComponent<BotaoConstruir>().Criar(FisicoCmp[i], QuadroConstruir, MenuSelecaoFisico, this);
            }
        }
    }
    public void Concluir()
    {
        //GameObject.FindWithTag("Player").GetComponent<Walk>().CanIWalk = true;
        AnimatorLeticia.Play(PosicaoOciosa);
        AudioSource cx = GameObject.FindWithTag("CaixaDeSom").GetComponent<AudioSource>();
        if (!cx.isPlaying) { cx.Play(); }
    }
    public static void TocarSomConfimar()
    {
        Source.PlayOneShot(SomConfirmar);
    }
    public static void TocarSomDesiste()
    {
        Source.PlayOneShot(SomDesistir);
    }
    public static void TocarSomNaoPode()
    {
    Source.PlayOneShot(SomNaoPode);
    }
    public void EsconderSub()
    {
        foreach(GameObject b in botoes)
        {
            b.GetComponent<BotaoConstruir>().EsconderSubBotoes();
        }
    }

   
}
