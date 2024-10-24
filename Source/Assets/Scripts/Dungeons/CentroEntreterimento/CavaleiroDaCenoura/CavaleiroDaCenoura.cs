using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CavaleiroDaCenoura : MonoBehaviour
{
    public static CavaleiroDaCenoura instance;   
    public Text scoreText; 
    [HideInInspector]
    public int score;
    public AudioSource Source;
    public AudioClip Ponto;
    [HideInInspector]
    public bool Iniciou;
    public MenuCavaleiro MenuCavaleiro;
    public GameObject CenaPrefab;
    public GameObject CenaCorrente;
    public PortaPuzzleEntreterimento MinhaPorta;
    public int IdPorta;
    public int IdDesafio;
    public SpriteRenderer SpriteIndicadorPorta;
    public Sprite PortaLiberada;
    private void Start()
    {       
        score = 0;
        UpdateScore();
        instance = this;
        if (StoryEvents.DesafiosCamp[6].Interagiveis[IdDesafio]) { MinhaPorta.AvisoAbrir(IdPorta); SpriteIndicadorPorta.sprite = PortaLiberada; }
    }

    public void IncrementScore()
    {
        score++;
        UpdateScore();       
    }

    public void GameOver()
    {
        StartCoroutine(MenuCavaleiro.GameOver());
        //Time.timeScale = 0f; // Pausar o jogo
        if (score > StoryEvents.RecordeFlip)
        {
            //liberarporta
            MinhaPorta.AvisoAbrir(IdPorta);
            SpriteIndicadorPorta.sprite = PortaLiberada;
            StoryEvents.DesafiosCamp[6].Interagiveis[IdDesafio] = true;
            //atualizascore
            StoryEvents.RecordeFlip = score;  
        }
        if(score>40f)
        {
            StoryEvents.TrapacaDesafio[4] = true;
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; // Retomar o jogo
        score = 0;
        Iniciou = false;
        UpdateScore();
        //desativa tela de gameover
        //ativa tela do menu iniciar
        StartCoroutine(MenuCavaleiro.Reiniciar());
        //destroi cena
        Destroy(CenaCorrente);
        //instancia nova cena
        CenaCorrente = Instantiate(CenaPrefab, this.transform) as GameObject;            
    }

    private void UpdateScore()
    {
        scoreText.text = score.ToString();
        Source.PlayOneShot(Ponto);
    }
}
