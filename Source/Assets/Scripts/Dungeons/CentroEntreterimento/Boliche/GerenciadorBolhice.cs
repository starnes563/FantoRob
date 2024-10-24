using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GerenciadorBolhice : MonoBehaviour
{
    int pinos = 0;
    bool somPino;
    public AudioSource Source;
    public AudioClip Pinos;
    public Text Placar;
    public AudioClip SomStrike;
    IEnumerator mostrandoPlacar;
    public int Dificuldade;
    public List<CinemachineVirtualCamera> Cameras = new List<CinemachineVirtualCamera>(3);
    public List<Pino> MeusPinos = new List<Pino>();
    public List<GameObject> Bolas = new List<GameObject>(2);
    public GameObject CameraCenario;
    int bola;
    Walk Player;
    bool dentro;
    bool jogou;
    public AudioClip SomIniciar;
    public PortaPuzzleEntreterimento MinhaPorta;
    public int IdPorta;
    public int IdDesafio;
    public SpriteRenderer SpriteIndicadorPorta;
    public Sprite PortaLiberada;
    // Start is called before the first frame update
    void Start()
    {
        if (StoryEvents.DesafiosCamp[6].Interagiveis[IdDesafio]) { MinhaPorta.AvisoAbrir(IdPorta); SpriteIndicadorPorta.sprite = PortaLiberada; }
    }

    // Update is called once per frame
    void Update()
    {
        if(dentro && Input.GetButtonDown("Fire1") && !jogou)
        {
            IniciarJogo();
        }
    }
    public void PinoDerrubado()
    {
        pinos++;
        if(mostrandoPlacar == null)
        {
            mostrandoPlacar = MostrarPlacar();
            StartCoroutine(MostrarPlacar());
            Cameras[0].gameObject.SetActive(false);
            Cameras[1].gameObject.SetActive(false);
            Cameras[2].gameObject.SetActive(true);
        }
        
    }
    public void SomPino()
    {
        if(!somPino)
        {
            Source.PlayOneShot(Pinos);
            somPino = true;
        }
    }
    public void Finalizar()
    {
        Player.LiberarAndar();
        foreach(CinemachineVirtualCamera c in Cameras) { c.gameObject.SetActive(false); }
        CameraCenario.SetActive(true);
    }
    IEnumerator MostrarPlacar()
    {
        yield return new WaitForSeconds(1f);
        if(pinos<10)
        {
            Placar.text = pinos.ToString() + "/10";
        }
        else if(pinos ==10)
        {
            Placar.text = "STRIKE!";
            Source.PlayOneShot(SomStrike);
            MinhaPorta.AvisoAbrir(IdPorta);
            StoryEvents.DesafiosCamp[6].Interagiveis[IdDesafio] = true;
            SpriteIndicadorPorta.sprite = PortaLiberada;
        }
        yield return new WaitForSeconds(0.5f);
        Finalizar();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
           Player = collision.GetComponent<Walk>();
            dentro = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dentro = false;
            jogou = false;
            foreach (Pino p in MeusPinos) { p.Reiniciar(); }
            foreach (GameObject b in Bolas) { b.GetComponent<BolaBoliche>().Reiniciar(); }
            somPino = false;
            Player = null;
            mostrandoPlacar = null;
        }
    }
    public void IniciarJogo()
    {        
        Source.PlayOneShot(SomIniciar);
        jogou = true;
        Player.CanIWalk = false;
        Player.GetComponent<Animator>().Play("IdleCostas");
        CameraCenario.SetActive(false);
        Cameras[0].gameObject.SetActive(true);
        //reinicia os pinos;
        foreach(Pino p in MeusPinos) { p.Reiniciar(); }
        foreach(GameObject b in Bolas) { b.GetComponent<BolaBoliche>().Reiniciar(); }
        //reinicia o placar
        pinos = 0;
        Placar.text = "";
        bola = Random.Range(0, 2);
        Bolas[bola].SetActive(true);
        Cameras[1].Follow = Bolas[bola].GetComponent<Transform>();        
    }
}
