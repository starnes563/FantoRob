using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DadosMissao
{
    public List<string> Nome = new List<string>();
    public List<string> Descriçao = new List<string>();
    public bool Completo { get; set; }
    public int Atual = 0;
    public int Requerido;
    public int Id;
    public List<DadosRecompensa> Recompensas = new List<DadosRecompensa>();
    public int MeuTipo;
    public DadosMissao(Quest q)
    {
        foreach(string n in q.Nome)
        {
            Nome.Add(n);
        }
        foreach(string d in q.Descriçao)
        {
            Descriçao.Add(d);
        }
        Completo = q.Completo;
        Atual = q.Atual;
        Requerido = q.Requerido;
        Id = q.Id;
        foreach(Recompensa r in q.Recompensas)
        {
            Recompensas.Add(new DadosRecompensa(r));
        }
        switch(q.MeuTipo)
        {
            case Quest.TipoDeQuest.BATALHA:
                MeuTipo = 0;
                break;
            case Quest.TipoDeQuest.VENCERFANTOROB:
                MeuTipo = 1;
                break;
            case Quest.TipoDeQuest.VENCEELEMENTO:
                MeuTipo = 2;
                break;
            case Quest.TipoDeQuest.VENCEFISICO:
                MeuTipo = 3;
                break;
            case Quest.TipoDeQuest.USAFANTOROB:
                MeuTipo = 4;
                break;
            case Quest.TipoDeQuest.USAELEMENTO:
                MeuTipo = 5;
                break;
            case Quest.TipoDeQuest.USAFISICO:
                MeuTipo = 6;
                break;
            case Quest.TipoDeQuest.SUPEREFETIVO:
                MeuTipo = 7;
                break;
            case Quest.TipoDeQuest.COMBO:
                MeuTipo = 8;
                break;
            case Quest.TipoDeQuest.HABILIDADE:
                MeuTipo = 9;
                break;
        }
    }
}
[System.Serializable]
public class DadosRecompensa
{    
    public int tipodere;
    public int Propriedade;
    public int quantidade;
    public DadosRecompensa(Recompensa r)
    {
        switch (r.MeuTipo)
        {
            case Recompensa.TipodeRecompensa.ITEMCONSTRUIR:
                tipodere = 0;
                break;
            case Recompensa.TipodeRecompensa.PENTEVAZIO:
                tipodere = 1;
                break;
            case Recompensa.TipodeRecompensa.PENTECHEIO:
                tipodere = 2;
                break;
            case Recompensa.TipodeRecompensa.CIRCUITO:
                tipodere = 3;
                break;
            case Recompensa.TipodeRecompensa.SILICIO:
                tipodere = 4;
                break;
            case Recompensa.TipodeRecompensa.PARTEROBO:
                tipodere = 5;
                break;
        }
        Propriedade = r.Propriedade;
        quantidade = r.quantidade;
    }
}
