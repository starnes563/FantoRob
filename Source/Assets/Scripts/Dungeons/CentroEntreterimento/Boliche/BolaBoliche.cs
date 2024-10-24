using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BolaBoliche : MonoBehaviour
{
    public Animator Anim;
    public Rigidbody2D Rb;
    bool rolling;
    public AudioSource Source;
    enum Estado
    {
        ESPERANDO,
        ROLANDO,
    }
    Estado MeuEstado;
    enum Lado
    {
        DIREITA,
        ESQUERDA,
    }
    Lado meuLado;
    public Transform MeuTransform;
    float dificuldade;
    Vector2 movement;
    public GerenciadorBolhice MeuGerenciador;
    public Vector2 PosicaoInicial;
    // Start is called before the first frame update
    void Start()
    {
        meuLado = Lado.ESQUERDA;
        MeuEstado = Estado.ESPERANDO;
        dificuldade = MeuGerenciador.Dificuldade;
    }

    // Update is called once per frame
    void Update()
    {
        switch (MeuEstado)
        {
            case Estado.ESPERANDO:
                switch (meuLado)
                {
                    case Lado.DIREITA:
                        movement.x = 1;
                        if (MeuTransform.localPosition.x >= 32f) { meuLado = Lado.ESQUERDA; }
                        break;
                    case Lado.ESQUERDA:
                        movement.x = -1;
                        if (MeuTransform.localPosition.x <= 27.60f) { meuLado = Lado.DIREITA; }
                        break;
                }
                Rb.MovePosition(Rb.position + movement * dificuldade * Time.fixedDeltaTime);
                if (Input.GetButtonDown("Fire1"))
                {
                    StartCoroutine(Atirar());
                }
                break;
            case Estado.ROLANDO:
                if (Rb.velocity.sqrMagnitude > 1f && !rolling)
                {
                    Anim.SetBool("Parada", false);
                    Anim.SetBool("Rolando", true);
                    rolling = true;
                    Source.Play();
                }
                if (Rb.velocity.sqrMagnitude < 1f && rolling)
                {
                    Anim.SetBool("Parada", true);
                    Anim.SetBool("Rolando", false);
                    rolling = false;
                    Source.Stop();
                }
                break;
        }
    }
    IEnumerator Atirar()
    {
        MeuEstado = Estado.ROLANDO;
        MeuGerenciador.Cameras[0].gameObject.SetActive(false);
        MeuGerenciador.Cameras[1].gameObject.SetActive(true);
        MeuGerenciador.Cameras[2].gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Rb.AddForce(transform.up * 50f, ForceMode2D.Impulse);
    }
    public void Reiniciar()
    {
        meuLado = Lado.ESQUERDA;
        MeuEstado = Estado.ESPERANDO;
        dificuldade = MeuGerenciador.Dificuldade;
        this.transform.localPosition = PosicaoInicial;
    }
}
