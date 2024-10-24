using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GerenciadorAirSoft : MonoBehaviour
{
    int tipoDeAlvo;
    int pontuacao;
    public float Velocidade;
    public Text Painel;
    public Text TempoDemonstrador;
    public int Necessario;
    public List<Sprite> Sprites;
    public List<SpriteRenderer> Indicadores;
    public float Tempo;
    float tempoatual;
    int tp;
    public AudioSource Source;
    public AudioClip SomIniciar;
    public AudioClip SomAvisar;
    public AudioClip SomConseguiu;
    public GameObject CameraAirSoft;
    public GameObject CameraCenario;
    Walk Player;
    //[HideInInspector]
    public bool Jogou;
    public List<SpawnAlvoAirSoft> MeusAlvos;
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
        if(Jogou)
        {
            tempoatual -= Time.deltaTime;
            tp = Mathf.RoundToInt(tempoatual);
            TempoDemonstrador.text = tp.ToString();
            if (tempoatual <= 0f) { StartCoroutine(Finalizar()); }
        }      
    }
    public void Pontuar(int alvo, int pontos)
    {
        if(alvo == tipoDeAlvo) { pontuacao += pontos; }
        else { pontuacao -= pontos; }
        Painel.text = pontuacao.ToString() + "/" + Necessario.ToString();
    }
    public IEnumerator Iniciar(Walk p)
    {
        int i = Random.Range(0, 2);
        tipoDeAlvo = i;
        foreach (SpriteRenderer sp in Indicadores) { sp.sprite = Sprites[i]; }
        Source.PlayOneShot(SomIniciar);
        Player = p;
        Player.CanIWalk = false;
        Player.GetComponent<Animator>().Play("IdleCostas");
        CameraCenario.SetActive(false);
        CameraAirSoft.SetActive(true);
        pontuacao = 0;
        tempoatual = Tempo;
        foreach (SpawnAlvoAirSoft sp in MeusAlvos) { sp.Reiniciar(); }
        yield return new WaitForSeconds(1.5f);
        Source.PlayOneShot(SomAvisar);
        yield return new WaitForSeconds(0.2f);
        Jogou = true;
    }
    public IEnumerator Finalizar()
    {
        Jogou = false;
        Source.PlayOneShot(SomAvisar);
        yield return new WaitForSeconds(0.5f);
        if(pontuacao >= Necessario)
        {
            MinhaPorta.AvisoAbrir(IdPorta);
            StoryEvents.DesafiosCamp[6].Interagiveis[IdDesafio] = true;
            SpriteIndicadorPorta.sprite = PortaLiberada;
            Source.PlayOneShot(SomConseguiu);
        }
        Player.LiberarAndar();
        CameraCenario.SetActive(true);
        CameraAirSoft.SetActive(false);
    }
}
