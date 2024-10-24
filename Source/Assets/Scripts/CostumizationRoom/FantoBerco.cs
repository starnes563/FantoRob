using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantoBerco : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip Colocando;
    public AudioClip Curou;
    public AudioClip SomImpossivel;
    public AudioClip SomQuebrar;
    int numero = 0;
    public bool Quebravel = false;
    float contador;
    int vezes = 0;
    bool contando;
    //variaveis para controle de desafios
    //0-escritorios
    //1-fazendadagua
    //2-barco
    //3-castelo
    //4-minas
    //5-mansao
    //6-centroentreterimento
    public int ID;
    public SpriteRenderer Renderer;
    public Sprite SpQuebrado;
    public GameObject Poeira;
    [HideInInspector]
    public bool quebrado;
    private void Start()
    {
        if (Quebravel && StoryEvents.TrapacaDesafio[ID])
        {
            Renderer.sprite = SpQuebrado;
            quebrado = true;
        }
    }
    public void Iniciar()
    {        
        if(Quebravel && StoryEvents.TrapacaDesafio[ID])
        {
            Source.PlayOneShot(SomImpossivel);
        }
        else
        {
            foreach (FantoRob fanto in PlayerObjects.RobotsInUse)
            {
                if (fanto != null)
                {
                    fanto.Curartudo();
                    numero++;
                }
            }
            switch (numero)
            {
                case 0:
                    //faz nada
                    break;
                case 1:
                    this.GetComponent<Animator>().SetTrigger("Iniciar1");
                    break;
                case 2:
                    this.GetComponent<Animator>().SetTrigger("Iniciar2");
                    break;
                case 3:
                    this.GetComponent<Animator>().SetTrigger("Iniciar3");
                    break;
            }
            if (Quebravel)
            {
                if (!contando) { contando = true; }
            }
        }        
    }
    private void Update()
    {
        if (contando && Quebravel)
        {
            contador += Time.deltaTime;
            if (vezes >= 10 && !quebrado) { Quebrar(); }
            if(contador>=20f)
            {
                vezes = 0;
                contando = false;
                contador = 0;
            }
        }
    }
    void Quebrar()
    {
        quebrado = true;
        Renderer.sprite = SpQuebrado;
        Source.PlayOneShot(SomQuebrar);
        StoryEvents.TrapacaDesafio[ID] = true;        
        Instantiate(Poeira, this.transform);
    }
    public void Colocar()
    {
        Source.PlayOneShot(Colocando);
    }
    public void SomCurar()
    {
        Source.PlayOneShot(Curou);
        Fim();
    }
    public void Fim()
    {
        ManagerGame.Instance.Regiao.GetComponent<Posicionador>().Neftari[PlayerStatus.PersonagemAtual].GetComponent<Walk>().LiberarAndar();
        numero = 0;
        vezes++;        
    }
}
