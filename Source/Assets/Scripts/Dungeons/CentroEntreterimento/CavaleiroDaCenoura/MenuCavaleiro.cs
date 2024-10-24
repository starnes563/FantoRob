using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCavaleiro : MonoBehaviour
{
    public GameObject Logo;
    public GameObject TelaGameOver;
    public PlayerController Cavaleiro;
    public AudioSource MusicaFundo;
    public AudioClip SomComeço;
    public AudioClip SomGameOver;
    public AudioClip SomMenuIniciar;
    public GameObject Contador;
    public Text Score;
    public Text HighScore;
    public GameObject TelaPassagem;
    public Text ControleFantoFicha;
    public GameObject AperteE;
    enum Estado
    {
        INICIAR,
        JOGANDO,
        GAMEOVER,
    }
    Estado MeuEstado;
    // Start is called before the first frame update
    void Start()
    {
        MeuEstado = Estado.INICIAR;
        if (!StoryEvents.DesafiosCamp[6].Chavegrande) { ControleFantoFicha.text = "=0"; AperteE.SetActive(false); }
        else { ControleFantoFicha.text = "=1"; AperteE.SetActive(false); }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && StoryEvents.DesafiosCamp[6].Chavegrande)
        {
            switch(MeuEstado)
            {
                case Estado.INICIAR:
                    StartCoroutine(Iniciar());
                    break;
                case Estado.JOGANDO:
                    
                    break;
                case Estado.GAMEOVER:
                    CavaleiroDaCenoura.instance.RestartGame();
                    break;
            }
           
        }
    }
    IEnumerator Iniciar()
    {
        this.GetComponent<AudioSource>().PlayOneShot(SomComeço);
        this.GetComponent<AudioSource>().loop = false;
        this.GetComponent<AudioSource>().Stop();
        Logo.SetActive(false);
        MusicaFundo.Play();
        yield return new WaitForSeconds(0.5f);
        CavaleiroDaCenoura.instance.Iniciou = true;
        //Cavaleiro.ComecarAndar();
        MeuEstado = Estado.JOGANDO;
    }
    public IEnumerator GameOver()
    {
        TelaGameOver.SetActive(true);
        MusicaFundo.Stop();
        this.GetComponent<AudioSource>().PlayOneShot(SomGameOver);
        yield return new WaitForSeconds(2f);
        MeuEstado = Estado.GAMEOVER;
        Score.text = CavaleiroDaCenoura.instance.score.ToString();
        HighScore.text = StoryEvents.RecordeFlip.ToString();
    }
    public IEnumerator Reiniciar()
    {        
        this.GetComponent<AudioSource>().Stop();
        MeuEstado = Estado.JOGANDO;
        TelaPassagem.SetActive(true);
        TelaGameOver.SetActive(false);
        Logo.SetActive(true);
        TelaGameOver.GetComponent<Animator>().Rebind();        
        yield return new WaitForSeconds(1f);
        TelaPassagem.SetActive(false);       
        this.GetComponent<AudioSource>().clip =SomMenuIniciar;
        this.GetComponent<AudioSource>().loop = true;
        this.GetComponent<AudioSource>().Play();
        MeuEstado = Estado.INICIAR;
    }
    public void SomCenario()
    {
        CaixaDeSom.Instancia.GetComponent<AudioSource>().Play();
        CaixaDeSom.Instancia.SobeVolume();
    }
}
