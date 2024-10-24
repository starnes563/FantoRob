using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float jumpForce = 5f;
    public float moveSpeed = 5f;
    public GameObject CenouraFrente;
    public GameObject CenouraTras;
    public Transform spearSpawnPoint;

    private Rigidbody2D rb;
    public int health;

    private Animator animator;
    public AudioSource Source;
    public AudioClip Passos;
    public AudioClip Pulo;
    public AudioClip Ataque;
    public AudioClip Cenoura;
    public AudioClip Morte;
    public AudioClip Fumaça;    
    public enum Estado
    {
        IDLE,
        ANDANDO,       
        ATACANDO,   
        PULANDO,
        MORTO
    }
    public Estado MeuEstado;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        MeuEstado = Estado.IDLE;       
    }
    public void ComecarAndar()
    {
        animator.SetBool("Andando", true);
        MeuEstado = Estado.ANDANDO;
    }
    private void Update()
    {
        if(CavaleiroDaCenoura.instance.Iniciou)
        {
            if (MeuEstado != Estado.MORTO)
            {
                // Verificar os inputs do jogador e chamar os métodos correspondentes
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.Z) && Input.GetAxis("Horizontal") < 0)
                {
                    AtacarParaTras();
                }
                if (Input.GetKeyDown(KeyCode.Z) && Input.GetAxis("Horizontal") >= 0)
                {
                    Atacar();
                }
            }
            if (animator.GetBool("Andando") && !Source.isPlaying)
            {
                tocarLoop(Passos);
            }
        }        
    }
    private void FixedUpdate()
    {
        if (CavaleiroDaCenoura.instance.Iniciou)
        {
            if (MeuEstado != Estado.MORTO)
            {
                if (MeuEstado != Estado.ATACANDO)
                {
                    if (rb.velocity.y > 0)
                    {
                        animator.SetBool("Andando", false);
                        animator.SetBool("Subindo", true);
                        animator.SetBool("Descendo", false);
                    }
                    if (rb.velocity.y < 0)
                    {
                        animator.SetBool("Andando", false);
                        animator.SetBool("Descendo", true);
                        animator.SetBool("Subindo", false);
                    }
                    if (rb.velocity.y == 0 && !animator.GetBool("Andando"))
                    {
                        animator.SetBool("Subindo", false);
                        animator.SetBool("Descendo", false);
                        ComecarAndar();
                    }
                }
            }
        }
    }
    private void Atacar()
    {
        animator.SetTrigger("Attack");
        MeuEstado = Estado.ATACANDO;
        tocarUmaVez(Ataque);
    }
    void AtacarParaTras()
    {
        animator.SetTrigger("BackAttack");
        MeuEstado = Estado.ATACANDO;
        tocarUmaVez(Ataque);
    }
    private void Jump()
    {
        if (rb.velocity.y == 0)
        {          
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
           // CavaleiroDaCenoura.instance.PlayJumpSound();
            MeuEstado = Estado.PULANDO;
            tocarUmaVez(Pulo);
        }
    }
    public void ThrowSpear()
    {
        Instantiate(CenouraFrente, spearSpawnPoint.position, Quaternion.identity);
        tocarUmaVez(Cenoura);
    }
    public void ThrowBackSpear()
    {
        Instantiate(CenouraTras, spearSpawnPoint.position, Quaternion.identity);
        tocarUmaVez(Cenoura);
    }
   
    public void TakeDamage(int damageAmount)
    {
        // Reduzir a vida do jogador pelo valor de dano
        health -= damageAmount;
        tocarUmaVez(Morte);
        // Verificar se o jogador ficou sem vida
        if (health <= 0)
        {
            // O jogador perdeu, executar as ações necessárias (ex.: mostrar tela de game over, reiniciar o jogo, etc.)
           
            animator.SetTrigger("Morto");
            MeuEstado = Estado.MORTO;            
        }       
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
    public void GameOver()
    {
        CavaleiroDaCenoura.instance.GameOver();
        Source.Stop();
    }
}
