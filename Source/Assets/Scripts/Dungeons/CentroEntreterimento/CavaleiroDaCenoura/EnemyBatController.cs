using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBatController : MonoBehaviour
{    
    public float flightSpeed = 5f;    
    public PlayerController playerController;
    public float attackRate = 1f;
    private float attackTimer;
    public float attackRange = 2f;
    public int damage = 1;    
    public Animator Anim;
    public AudioSource Source;
    public AudioClip SomVoar;    
    public AudioClip Dano;
    public AudioClip Fumaça;
    bool morto;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.MeuEstado != PlayerController.Estado.MORTO)
        {
            if (!morto)
            {
                // Atualizar o contador de tempo do ataque
                attackTimer += Time.deltaTime;

                // Verificar se é hora de atacar
                if (attackTimer >= 1f / attackRate)
                {
                    Attack();
                }
                Vector3 direction = (playerController.transform.position - transform.position).normalized;
                Vector3 movement = direction * flightSpeed * Time.deltaTime;
                if (movement.x > 0)
                {
                    Anim.SetBool("Direita", true);
                    Anim.SetBool("Esquerda", false);
                }
                if (movement.x < 0)
                {
                    Anim.SetBool("Direita", false);
                    Anim.SetBool("Esquerda", true);
                }

                transform.Translate(movement);
                if (!Source.isPlaying)
                {
                    tocarLoop(SomVoar);
                }
            }
        }
        else
        {
            if (Source.isPlaying) { Source.Stop(); Vector3 movement = Vector3.zero; transform.Translate(movement); }
        }
    }
    private void Attack()
    {
        // Verificar se o jogador está dentro da distância de ataque
        if (Vector3.Distance(transform.position, playerController.transform.position) < attackRange)
        {
            // Causar dano ao jogador (implemente o método de dano no PlayerController)
            playerController.TakeDamage(damage);
            attackTimer = 0f;
        }
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
