using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class Quest
{   public List<string> Nome = new List<string>();
    public List<string> Descriçao = new List<string>();
    public bool  Completo { get; set; }
    public int Atual = 0;
    public int Requerido;
    public int Id;
    public List<Recompensa> Recompensas = new List<Recompensa>();
    public enum TipoDeQuest
    {
        BATALHA,
        VENCERFANTOROB,
        VENCEELEMENTO,
        VENCEFISICO,
        USAFANTOROB,
        USAELEMENTO,
        USAFISICO,
        SUPEREFETIVO,
        COMBO,
        HABILIDADE,
    }
    public TipoDeQuest MeuTipo;

    public Quest(TipoDeQuest tipo, int requer, int id, List<string> nm, List<string> des, List<Recompensa> recompensas)
    {
        MeuTipo = tipo;
        Requerido = requer;
        Id = id;       
        foreach(string n in nm) { Nome.Add(n); }        
        foreach (string d in des) { Descriçao.Add(d); }
        if(recompensas != null)
        {
            foreach (Recompensa r in recompensas) { Recompensas.Add(r); }
        }
    }
    public void AdicionarMissao()
    {
        inicializar();
        PlayerObjects.Missões.Add(this);
    }
    public void DarMissao()
    {           
        ManagerGame.Instance.MostrarQuadroMissao(this);
    }
    void inicializar()
    {        
        switch (MeuTipo)
        {           
            case TipoDeQuest.BATALHA:
                ManagerGame.Instance.GanhouBatalha += ContadorBasico;
                break;
            case TipoDeQuest.VENCERFANTOROB:
                ManagerGame.Instance.VenceuFantorob += ContadorId;
                break;
            case TipoDeQuest.VENCEELEMENTO:
                ManagerGame.Instance.VenceuElemento += ContadorId;
                break;
            case TipoDeQuest.VENCEFISICO:
                ManagerGame.Instance.VenceuFisico += ContadorId;
                break;
            case TipoDeQuest.USAFANTOROB:
                ManagerGame.Instance.UsaFantoRob += ContadorId;
                break;
            case TipoDeQuest.USAELEMENTO:
                ManagerGame.Instance.UsaElemento += ContadorId;
                break;
            case TipoDeQuest.USAFISICO:
                ManagerGame.Instance.UsaFisico += ContadorId;
                break;
            case TipoDeQuest.SUPEREFETIVO:
                ManagerGame.Instance.SuperEfeitov += ContadorBasico;
                break;
            case TipoDeQuest.COMBO:
                ManagerGame.Instance.Combo += ContadorId;
                break;
            case TipoDeQuest.HABILIDADE:
                ManagerGame.Instance.UsaArma += ContadorId;
                break;
        }
    }
    public void Evaluate()
    {
        if (Atual >= Requerido) { Complete(); }
    }
    public void Complete()
    {
        switch (MeuTipo)
        {
            case TipoDeQuest.BATALHA:
                ManagerGame.Instance.GanhouBatalha -= ContadorBasico;
                break;
            case TipoDeQuest.VENCERFANTOROB:
                ManagerGame.Instance.VenceuFantorob -= ContadorId;
                break;
            case TipoDeQuest.VENCEELEMENTO:
                ManagerGame.Instance.VenceuElemento -= ContadorId;
                break;
            case TipoDeQuest.VENCEFISICO:
                ManagerGame.Instance.VenceuFisico -= ContadorId;
                break;
            case TipoDeQuest.USAFANTOROB:
                ManagerGame.Instance.UsaFantoRob -= ContadorId;
                break;
            case TipoDeQuest.USAELEMENTO:
                ManagerGame.Instance.UsaElemento -= ContadorId;
                break;
            case TipoDeQuest.USAFISICO:
                ManagerGame.Instance.UsaFisico -= ContadorId;
                break;
            case TipoDeQuest.SUPEREFETIVO:
                ManagerGame.Instance.SuperEfeitov -= ContadorBasico;
                break;
            case TipoDeQuest.COMBO:
                ManagerGame.Instance.Combo -= ContadorId;
                break;
            case TipoDeQuest.HABILIDADE:
                ManagerGame.Instance.UsaArma += ContadorId;
                break;
        }
        Completo = true;
        ManagerGame.Instance.MostrarQuadroMissao(this);
       if(Recompensas != null && Recompensas.Count>0)
        {
            foreach (Recompensa r in Recompensas) { r.DarRecompensa(); }
        }        
    }
    void ContadorBasico()
    {
        if(!Completo)
        {           
            Atual++;
            Evaluate();
        }      
    } 
    void ContadorId(int id)
    {
        if (!Completo)
        {
            if (id == Id)
            {
                Atual++;
                Evaluate();
            }
        }
    }   
    public void CarregarSalvo(DadosMissao q)
    {
        foreach (string n in q.Nome)
        {
            Nome.Add(n);            
        }
        foreach (string d in q.Descriçao)
        {
            Descriçao.Add(d);
        }
        Completo = q.Completo;
        Atual = q.Atual;
        Requerido = q.Requerido;
        Id = q.Id;
        foreach (DadosRecompensa r in q.Recompensas)
        {
            Recompensa re = new Recompensa(Recompensa.TipodeRecompensa.CIRCUITO,0,0);
            re.CarregarSalvo(r);
            Recompensas.Add(re);
        }
        switch (q.MeuTipo)
        {
            case 0:
                MeuTipo = TipoDeQuest.BATALHA;
                break;
            case 1:
                MeuTipo = TipoDeQuest.VENCERFANTOROB;
                break;
            case 2:
                MeuTipo = TipoDeQuest.VENCEELEMENTO;
                break;
            case 3:
                MeuTipo = TipoDeQuest.VENCEFISICO;
                break;
            case 4:
                MeuTipo = TipoDeQuest.USAFANTOROB;
                break;
            case 5:
                MeuTipo = TipoDeQuest.USAELEMENTO;
                break;
            case 6:
                MeuTipo = TipoDeQuest.USAFISICO;
                break;
            case 7:
                MeuTipo = TipoDeQuest.SUPEREFETIVO;
                break;
            case 8:
                MeuTipo = TipoDeQuest.COMBO;
                break;
            case 9:
                MeuTipo = TipoDeQuest.HABILIDADE;
                break;
        }        
    }
}
[System.Serializable]
public class Recompensa
{
    public enum TipodeRecompensa
    {
        ITEMCONSTRUIR,
        PENTEVAZIO,
        PENTECHEIO,
        CIRCUITO,
        SILICIO,
        PARTEROBO,
    }
    public TipodeRecompensa MeuTipo;
    public int Propriedade;
    public int quantidade;
    public Recompensa(TipodeRecompensa tp, int prop, int qd)
    {
        MeuTipo = tp;
        Propriedade = prop;
        quantidade = qd;
    }
    public void DarRecompensa()
    {
        for (int i = 0; i < quantidade; i++)
        {
            switch (MeuTipo)
            {
                case TipodeRecompensa.ITEMCONSTRUIR:
                    PlayerObjects.ItensConstruir[Propriedade]++;
                    break;
                case TipodeRecompensa.PENTEVAZIO:
                    PlayerObjects.PentesVazios.Add(Constructor.Instance.CombConstructor(true, Propriedade));
                    break;
                case TipodeRecompensa.PENTECHEIO:
                    PlayerObjects.PentesCheios.Add(Constructor.Instance.CombConstructor(false, Propriedade));
                    break;
                case TipodeRecompensa.CIRCUITO:
                    PlayerObjects.Circuits[Propriedade]++;
                    break;
                case TipodeRecompensa.SILICIO:
                    PlayerObjects.Silicon++;
                    break;
                case TipodeRecompensa.PARTEROBO:
                    int partid = 1;
                    if (PlayerStatus.Estrelas < 2)
                    {
                        partid = UnityEngine.Random.Range(1, 3);
                    }
                    else if (PlayerStatus.Estrelas < 4)
                    {
                        partid = UnityEngine.Random.Range(1, 5);
                    }
                    else if (PlayerStatus.Estrelas >= 4)
                    {
                        partid = UnityEngine.Random.Range(0, 5);
                    }
                    PlayerObjects.RobotParts.Add(Constructor.Instance.PartConstructor(partid, UnityEngine.Random.Range(0, 5), Propriedade));
                    break;
            }
        }
    }
    public void CarregarSalvo(DadosRecompensa r)
    {
        switch(r.tipodere)
        {
            case 0:
                MeuTipo = TipodeRecompensa.ITEMCONSTRUIR;
                break;
            case 1:
                MeuTipo = TipodeRecompensa.PENTEVAZIO;
                break;
            case 2:
                MeuTipo = TipodeRecompensa.PENTECHEIO;
                break;
            case 3:
                MeuTipo = TipodeRecompensa.CIRCUITO;
                break;
            case 4:
                MeuTipo = TipodeRecompensa.SILICIO;
                break;
            case 5:
                MeuTipo = TipodeRecompensa.PARTEROBO;
                break;
        }
        Propriedade = r.Propriedade;
        quantidade = r.quantidade;
    }
}