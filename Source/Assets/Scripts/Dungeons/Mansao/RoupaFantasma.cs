using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoupaFantasma : MonoBehaviour
{
    public float FPS = 10;
    public float Speed = 5;
    //0-FRENTE
    //1-DIRETA
    //2-COSTAS
    //3-ESQUERDA
    public LoopSpriteAnimation[] Movimentando = new LoopSpriteAnimation[4];
    public LoopSpriteAnimation[] Idle = new LoopSpriteAnimation[4];
    Vector2 movement;   
    [HideInInspector]
    public int actualCicle = 0;
    IEnumerator actualCicleCoroutine;
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
        MyState = MoveState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        switch (MyState)
        {
            case MoveState.MOVENDO:

                moveCalc();
                break;
            case MoveState.IDLE:

                moveCalc();
                break;
        }
    }    
    void moveCalc()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
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

        }    
    }
}
