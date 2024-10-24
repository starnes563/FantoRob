using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArremessaOvo : MonoBehaviour
{
    public GameObject OvoDireita;
    public GameObject OvoEsquerda;
    public GameObject OvoFrente;
    public GameObject OvoCostas;

    public enum IniciaAtira
    {
        FRENTE,
        COSTAS,
        DIREITA,
        ESQUERDA
    }
    public IniciaAtira PosicaoAtira;
    public bool AtiraAutomatico;
    float contador = 0;
    float proximo = 0;
    private void Start()
    {
        if(AtiraAutomatico)
        {
            proximo = Random.Range(0, 3);
        }
    }
    private void Update()
    {
        if(AtiraAutomatico)
        {
            contador += Time.deltaTime;
            if(contador>=proximo)
            {
                contador = 0;
                proximo = Random.Range(1, 3);
                switch (PosicaoAtira)
                {
                    case IniciaAtira.FRENTE:
                        Frente();
                        break;
                    case IniciaAtira.COSTAS:
                        Costas();
                        break;
                    case IniciaAtira.ESQUERDA:
                        Esquerda();
                        break;
                    case IniciaAtira.DIREITA:
                        Direita();
                        break;
                }
            }
        }
    }
    public void Direita()
    {
        Vector3 posicao = new Vector3(Random.Range(this.transform.position.x - 0.4f, this.transform.position.x + 0.5f), Random.Range(this.transform.position.y - 0.4f, this.transform.position.y + 0.5f), this.transform.position.z);
        Instantiate(OvoDireita, posicao, OvoDireita.transform.rotation);
    }

    public void Esquerda()
    {
        Vector3 posicao = new Vector3(Random.Range(this.transform.position.x - 0.4f, this.transform.position.x + 0.5f), Random.Range(this.transform.position.y - 0.4f, this.transform.position.y + 0.5f), this.transform.position.z);
        Instantiate(OvoEsquerda, posicao, OvoEsquerda.transform.rotation);
    }
    public void Frente()
    {
        Vector3 posicao = new Vector3(Random.Range(this.transform.position.x - 0.4f, this.transform.position.x + 0.5f), Random.Range(this.transform.position.y - 0.4f, this.transform.position.y + 0.5f), this.transform.position.z);
        Instantiate(OvoFrente, posicao, OvoFrente.transform.rotation);
    }
    public void Costas()
    {
        Vector3 posicao = new Vector3(Random.Range(this.transform.position.x - 0.4f, this.transform.position.x + 0.5f), Random.Range(this.transform.position.y - 0.4f, this.transform.position.y + 0.5f), this.transform.position.z);
        Instantiate(OvoCostas, posicao, OvoCostas.transform.rotation);
    }
}
