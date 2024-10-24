using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimluadorDeBatalha : MonoBehaviour
{
    public List<FantoRob> FantoRobs;
    public List<Weapon> NFs;
    public List<float> PercentualDeVitoria;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IniciarTeste()
    {
        //gera variaveis de teste
        foreach (FantoRob r in FantoRobs)
        {
            r.BatalhaTravada = 0;
            r.BatalhaVencida = 0;
        }
            //gerar o de ataque;
            foreach (FantoRob fantoat in FantoRobs)
        {
            foreach (Weapon wp in NFs)
            {
                ativarArma(fantoat, wp);
                fantoat.Fisico = wp;
                //gerar os de defesa
                foreach (FantoRob fantodef in FantoRobs)
                {
                    foreach (Weapon fisicodef in NFs)
                    {
                        ativarArma(fantodef, fisicodef);
                        StartCoroutine(Batalha(fantoat, fantodef));
                    }
                }
            }
        }
        for (int i = 0; i<FantoRobs.Count; i++)
        {
            float p = FantoRobs[i].BatalhaVencida / FantoRobs[i].BatalhaTravada;
            PercentualDeVitoria.Add(p);
        }
    }
    void ativarArma(FantoRob rob, Weapon wp)
    {
        foreach(Move mv in rob.MovimentoAmbos)
        {
            wp.MovesAtivos.Add(mv);
        }
        foreach (Move mv in rob.MovimentoJogador)
        {
            wp.MovesAtivos.Add(mv);
        }
        foreach (Move mv in wp.MovimentosAmbos)
        {
            wp.MovesAtivos.Add(mv);
        }
        foreach (Move mv in wp.MovimentosJogador)
        {
            wp.MovesAtivos.Add(mv);
        }
        wp.CarregarAtaques();
    }
    IEnumerator Batalha( FantoRob fantorobAtacar, FantoRob fantorobDefender)
    {
        fantorobAtacar.BatalhaTravada++;
        fantorobDefender.BatalhaVencida++;
        yield return null;
    }
}
