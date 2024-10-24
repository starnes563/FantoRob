using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Move", menuName = "Fantorob/Move")]
[System.Serializable]
public class Move : ScriptableObject
{
    public string Nome;
    public List<string> NamesLang = new List<string>();
    //0 - Vermelho
    //1 - Azul
    //2 - Amarelo
    //3 - Verde
    //4 - Laranja
    //5 - Roxo
    //9 - ND
    public int Elemento;
    public float Forca;
    public int Precisao;
    public float GastoEnergiaPercentual;
    public int UsoDeAcoes;
    // efeito tem haver com o tipo de virus que um ataque pode ter
    // 0 - nenhum
    // 1 - spy
    // 2 - keylogger
    // 3 - trojan
    // 4 - ranson
    // 5 - worm
    // 6 - virus
    public int Efeito;
    public float AumentoEfeito = 0;
    public bool Diferenciado;
    public bool InstanciaInvertido;
    public float FatorArmadilha = 0;
    public bool elemental = false;    
    public bool escolhido = false;
    public int Id;
    public GameObject AnimacaoDeAtaque;
    public bool Only;
    public List<string> Descrição = new List<string>(); //0-português
    [HideInInspector]
    public bool Aleatório;
    public int Array;
    [HideInInspector]
    public int Animacao;
    [HideInInspector]
    public int meuIdNoPente;
    [HideInInspector]
    public int meuIdnoAtaque;
    [HideInInspector]
    public int meuIdnoAtivos;
    [HideInInspector]
    public bool emUso = false;
    public void CarregarSalvo(DadoMove dados)
    {
        Aleatório = dados.Aleatório;
        if (dados.Aleatório)
        {
            Nome = dados.Nome;
            if(dados.NamesLang.Count>0)
            {
                foreach (string n in dados.NamesLang)
                {                    
                    NamesLang.Add(n);
                }
            }           
            Forca = dados.Forca;
            Precisao = dados.Precisao;
            GastoEnergiaPercentual = dados.GastoEnergiaPercentual;
            UsoDeAcoes = dados.UsoDeAcoes;
            Efeito = dados.Efeito;
            AumentoEfeito = dados.AumentoEfeito;
            Diferenciado = dados.Diferenciado;
            FatorArmadilha = dados.FatorArmadilha;
            elemental = dados.elemental;
            escolhido = dados.escolhido;
            Id = dados.Id;
            Only = dados.Only;
            foreach (string des in dados.Descrição)
            {
                Descrição.Add(des);
            }
            Animacao = dados.Animacao;
            AnimacaoDeAtaque = Constructor.Instance.Animacoes[Animacao];
            //if (elemental) { AnimacaoDeAtaque.GetComponent<AttackAnimation>().MeuTipo = AttackAnimation.Tipo.ELEMENTAL; }
        }
        else
        {
            Array = dados.Array;
        }
        meuIdNoPente = dados.meuIdNoPente;
        meuIdnoAtaque = dados.meuIdnoAtaque;
        meuIdnoAtivos = dados.meuIdnoAtivos;
        emUso = dados.emUso;
    }   
}
