using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlimeController : MonoBehaviour
{   
    public float Velocidade = 5f;
    public PlayerController playerController;
    public float attackRate = 1f;
    private float attackTimer;
    public float attackRange = 2f;
    public int damage = 1;
    private Vector2 movimento;
    public Rigidbody2D Rb;
    public Animator Anim;
    public AudioSource Source;
    public AudioClip SomAndar;
    public AudioClip Atacar;
    public AudioClip Dano;
    public AudioClip Fumaça;
    private enum State
    {
        CAINDO,
        ANDANDO,
        ATACANDO,
    }
    private State myState;
    bool morto;
    // Start is called before the first frame update
    public void Start()
    {
        myState = State.CAINDO;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Andar();
    }
  
    public void Andar()
    {
        myState = State.ANDANDO;
    }
    // Update is called once per frame
    void Update()
    {
        if(playerController.MeuEstado != PlayerController.Estado.MORTO)
        {
            if (myState == State.ANDANDO)
            {
                movimento = playerController.transform.position - this.transform.position;
                if (movimento.x > 0)
                {
                    Anim.SetBool("Direita", true);
                    Anim.SetBool("Esquerda", false);
                }
                if (movimento.x < 0)
                {
                    Anim.SetBool("Direita", false);
                    Anim.SetBool("Esquerda", true);
                }
                // Atualizar o contador de tempo do ataque
                attackTimer += Time.deltaTime;
                // Verificar se é hora de atacar
                if (attackTimer >= 1f / attackRate)
                {
                    Attack();
                }
                if (!Source.isPlaying)
                {
                    tocarLoop(SomAndar);
                }
            }
        }
        else
        {
            if (Source.isPlaying) { Source.Stop();movimento = Vector2.zero; }
        }
    }
    private void FixedUpdate()
    {     
        if(!morto)
        {
            Rb.MovePosition(Rb.position + movimento.normalized * Velocidade * Time.fixedDeltaTime);
        }                
    }
    private void Attack()
    {        
        if (Vector3.Distance(transform.position, playerController.transform.position) < attackRange)
        {
            Anim.SetTrigger("Atacar");
            myState = State.ATACANDO;
            attackTimer = 0f;
            tocarUmaVez(Atacar);
        }
    }
    public void CasarDano()
    {
        playerController.TakeDamage(damage);
    }    
    public void TomarDano(bool cenoura)
    {
        morto = true;
        Anim.SetTrigger("Morrer");
        tocarUmaVez(Dano);
        if (cenoura) { CavaleiroDaCenoura.instance.IncrementScore(); }
    }
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
    void tocarLoop(AudioClip clip)
    {
        Source.Stop();
        Source.clip = clip;
        Source.loop = true;
        Source.Play();
    }
    void tocarUmaVez(AudioClip clip)
    {
        Source.Stop();
        Source.loop = false;
        Source.PlayOneShot(clip);
    }
    public void SomFumaca()
    {
        tocarUmaVez(Fumaça);
    }
}
