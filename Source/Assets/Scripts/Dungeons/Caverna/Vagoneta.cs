using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vagoneta : MonoBehaviour
{   
    //this script handles all the movement, and animations that changes de sprite renderer.
    public float FPS = 10;
    public float Speed = 5;
    //0-FRENTE
    //1-DIRETA
    //2-COSTAS
    //3-ESQUERDA
    public LoopSpriteAnimation[] Movimentando = new LoopSpriteAnimation[4];
    public LoopSpriteAnimation[] Idle = new LoopSpriteAnimation[4];
    Vector2 movement;
    Rigidbody2D Rb;
    int actualCicle = 0;
    IEnumerator actualCicleCoroutine;
    [HideInInspector]
    public int X = 0;
    [HideInInspector]
    public int Y = 0;
    [HideInInspector]
    public GameObject Jogador;
    public GameObject[] Nef = new GameObject[4];    
    AudioSource audioSource;
    public AudioClip SomVagoneta;
    enum MoveState
    {
       IDLE,
       MOVENDO          
    }
   MoveState MyState;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spRender = GetComponent<SpriteRenderer>();
        foreach (LoopSpriteAnimation anim in Movimentando)
        {
            anim.SetSpriteRenderer(spRender, FPS);
        }
        foreach (LoopSpriteAnimation anim in Idle)
        {
            anim.SetSpriteRenderer(spRender, FPS);
        }
        Rb = GetComponent<Rigidbody2D>();
        MyState = MoveState.IDLE;
        audioSource = GetComponent<AudioSource>();       
    }

    // Update is called once per frame
    void Update()
    {
        switch (MyState)
        {
            case MoveState.MOVENDO:
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(SomVagoneta);
                }
                moveCalc();
                prenderJogador();
                break;
            case MoveState.IDLE:
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                moveCalc();
                break;           
        }       
    }
    void FixedUpdate()
    {
        Rb.MovePosition(Rb.position + movement * Speed * Time.fixedDeltaTime);
    }
    void prenderJogador()
    {
        if(Jogador!=null)
        {
            Jogador.transform.position = this.transform.position;
        }
    }        
    void moveCalc()
    {
        movement.x = X;
        movement.y = Y;
        movement = movement.normalized;
        if (movement.sqrMagnitude > 0.001f)
        {
            loopBlendTree(Movimentando);
        }
        else if (MyState != MoveState.IDLE)
        {
            if (actualCicleCoroutine != null) { StopCoroutine(actualCicleCoroutine); }
            actualCicleCoroutine = Idle[actualCicle].Animate();
            StartCoroutine(actualCicleCoroutine);
            foreach (GameObject gb in Nef)
            {
                gb.SetActive(false);
            }
            MyState = MoveState.IDLE;
        }

    }
    void loopBlendTree(LoopSpriteAnimation[] cicle)
    {
        int cicleCorroutine = 0;
        if (movement.y < -0.001f) { cicleCorroutine = 0; }
        if (movement.x > 0.001f) { cicleCorroutine = 1; }
        if (movement.y > 0.001f) { cicleCorroutine = 2; }
        if (movement.x < -0.001f) { cicleCorroutine = 3; }
        if (actualCicle != cicleCorroutine && MyState == MoveState.MOVENDO || MyState != MoveState.MOVENDO)
        {
            if (actualCicleCoroutine != null) { StopCoroutine(actualCicleCoroutine); }
            actualCicle = cicleCorroutine;
            actualCicleCoroutine = cicle[actualCicle].Animate();
            StartCoroutine(actualCicleCoroutine);
            MyState = MoveState.MOVENDO;
            foreach(GameObject gb in Nef)
            {
                gb.SetActive(false);
            }
            Nef[actualCicle].SetActive(true);
        }
    }     
    public void Desembarar(Vector3 posic, string anim)
    {
        Jogador.transform.position = posic;
        Jogador.GetComponent<Animator>().Play(anim);
        Jogador.GetComponent<Walk>().CanIWalk = true;
        Jogador.GetComponent<BoxCollider2D>().isTrigger = false;
        Jogador = null;    
    }
}
[System.Serializable]
//Loop sprite animtor is the main class tha handles the animation that doesn't have a point to stop as walk cicles, idle poses, sleep cicles
public class LoopSpriteAnimation
{
    private float secPerFrame;
    public Sprite[] spriteSheet;
    private SpriteRenderer sprite;
    public void SetSpriteRenderer(SpriteRenderer sp, float MyFps)
    {
        sprite = sp;
        secPerFrame = 1f / MyFps;
    }
    public IEnumerator Animate()
    {
        int frame = 0;
        while (true)
        {
            frame += 1;
            if (frame >= spriteSheet.Length)
            {
                frame = 0;
            }
            sprite.sprite = spriteSheet[frame];

            yield return new WaitForSeconds(secPerFrame);
        }
    }
}

