using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlvoAirSoft : MonoBehaviour
{
    public GerenciadorAirSoft Gerenciador;
    // 0 - vermelho 1 - azul
    public int MeuTipo;
    public Animator Anim;
    public AudioSource AudioSource;
    public AudioClip SomAcertou;
    public List<GameObject> GatilhosAcerto;
    bool acertado;
    public Rigidbody2D Rb;
    [HideInInspector]
    public int X;
    [HideInInspector]
    public float Speed = 5;
    Vector2 movement;
    // Start is called before the first frame update
    void Start()
    {
        movement.x = X;
    }    
    void FixedUpdate()
    {
        Rb.MovePosition(Rb.position + movement * Speed * Time.fixedDeltaTime);
    }
    public void Acertou(int ponto)
    {
        if(!acertado)
        {
            acertado = true;
            Gerenciador.Pontuar(MeuTipo, ponto);
            Anim.SetTrigger("Acertou");
            AudioSource.PlayOneShot(SomAcertou);
            foreach (GameObject g in GatilhosAcerto) { g.SetActive(false); }
        }       
    }
}
