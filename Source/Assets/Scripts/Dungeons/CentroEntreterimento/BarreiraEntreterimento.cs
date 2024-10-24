using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreiraEntreterimento : MonoBehaviour
{
    public bool Laranja;
    public int FPS = 10;
    //partedebaixo
    public Sprite[] Loop;     
    public Sprite AbaixadoSp;
    public Sprite LevantadoSp;
    public SpriteRenderer Renderer;
    public GameObject GatilhosOrderCollider;    
    private float secPerFrame;   
   
    enum Estado
    {
        LEVANTADO,
        ABAIXADO,        
    }
    Estado MeuEstado;
    // Start is called before the first frame update
    void Start()
    {        
        secPerFrame = 1f / FPS;
        if (Laranja == StoryEvents.DesafiosCamp[6].Interagiveis[0])
        {
            Levantado();
            
        }
        else
        {
            Abaixado();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Laranja == StoryEvents.DesafiosCamp[6].Interagiveis[0] & MeuEstado != Estado.LEVANTADO)
        {
            StopAllCoroutines();
            StartCoroutine(Levantar());
        }
        if (Laranja != StoryEvents.DesafiosCamp[6].Interagiveis[0] & MeuEstado != Estado.ABAIXADO)
        {
            StopAllCoroutines();
            StartCoroutine(Abaixar());
        }
    }
    void Abaixado()
    {
        Renderer.sprite = AbaixadoSp;
        GatilhosOrderCollider.SetActive(false);
        MeuEstado = Estado.ABAIXADO;        
    }
    void Levantado()
    {
        Renderer.sprite = LevantadoSp;
        GatilhosOrderCollider.SetActive(true);
        MeuEstado = Estado.LEVANTADO;       
    }
    public IEnumerator Levantar()
    {
        MeuEstado = Estado.LEVANTADO;
        int frame = 0;
        while(frame<Loop.Length)
        {
            Renderer.sprite = Loop[frame];
            frame++;
            yield return new WaitForSeconds(secPerFrame);
        }
        Levantado();
    }
    public IEnumerator Abaixar()
    {
        MeuEstado = Estado.ABAIXADO;
        int frame = 3;
        while (frame > 0)
        {
            Renderer.sprite = Loop[frame];
            frame--;
            yield return new WaitForSeconds(secPerFrame);
        }
        Abaixado();
    }
    
}
