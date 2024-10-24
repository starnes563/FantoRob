using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pino : MonoBehaviour
{
    public GerenciadorBolhice MeuGerenciador;
    bool caido;
    bool ativado;
    Rigidbody2D rb;
    public Vector2 PosicaoInicial;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(ativado && !caido)
        {            
            if (rb.velocity.sqrMagnitude > 0.3f && !caido)
            {
                MeuGerenciador.PinoDerrubado();
                caido = true;
                MeuGerenciador.SomPino();

            }
        }
    }
    // Start is called before the first frame update    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ativado = true;       
    }
    public void Reiniciar()
    {
        this.gameObject.SetActive(false);        
        this.transform.localPosition = PosicaoInicial;
        this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        caido = false;
        ativado = false;
        this.gameObject.SetActive(true);
    }
}
