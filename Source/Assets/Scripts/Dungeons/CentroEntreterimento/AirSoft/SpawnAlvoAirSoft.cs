using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAlvoAirSoft : MonoBehaviour
{
    public GerenciadorAirSoft Gerenciador;
    float Contador;
    public enum Instancia
    {
        AZUL,
        VERMELHO,
    }
    public Instancia MinhaInstancia;
    public List<GameObject> Alvos;
    public int X;
    public int Order;
    public bool Alternado;

    // Update is called once per frame
    void Update()
    {
        if(Gerenciador.Jogou)
        {
            Contador += Time.deltaTime;
            if (Contador >= 5 / Gerenciador.Velocidade) { instanciar(); }
        }        
    }
    void instanciar()
    {
        AlvoAirSoft alvo = null;
        switch(MinhaInstancia)
        {
            case Instancia.VERMELHO:
                MinhaInstancia = Instancia.AZUL;
                alvo =  Instantiate(Alvos[0], this.transform).GetComponent<AlvoAirSoft>();
                break;
            case Instancia.AZUL:                
                alvo = Instantiate(Alvos[1], this.transform).GetComponent<AlvoAirSoft>();
                MinhaInstancia = Instancia.VERMELHO;
                break;
        }
        alvo.Gerenciador = Gerenciador;
        alvo.Speed = Gerenciador.Velocidade;
        alvo.X = X;
        Contador = 0;
        alvo.GetComponent<SpriteRenderer>().sortingOrder = Order;
    }
    public void Reiniciar()
    {
        if (!Alternado) { Contador = 10; } else { Contador = 0; }
    }
}
