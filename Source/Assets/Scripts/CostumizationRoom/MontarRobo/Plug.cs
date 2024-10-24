using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plug : MonoBehaviour
{//0 - attack
 //1 - spattack
 //2 - resistance
 //3 - speed
 //4 - integrity
 //5 - buffer;
 //9 - vazio
    [HideInInspector]
    public int Id = 9;
    [HideInInspector]
    public int[] Value = new int[6];
    [HideInInspector]
    public Pente Comb; 
    private bool buffed = false;    
    public GameObject Pente;
    public CombSelectionMenu MenuPentes;    
    public List<Plug> Conectados;
    public Board Placa;
    public ChooseRobotMenu chooseRobotMenu;
    public int NumeroNaPlaca;
    public CombSelectionButton BotaoDeAmostra;
    bool mostrando = false;
      public enum Encaixe
    {
        ACOMPLADO,
        DESACOPLADO,
    }
    public Encaixe Exc = Encaixe.DESACOPLADO;
    public void Clicar()
    {
        switch (Exc)
        {
            case Encaixe.ACOMPLADO:
                if (!mostrando)
                {
                    mostrando = true;
                    BotaoDeAmostra.gameObject.SetActive(true);
                    BotaoDeAmostra.Mostrar(Comb);
                }
                else
                {
                    mostrando = false;
                    Desacoplar();
                    BotaoDeAmostra.gameObject.SetActive(false);
                    BotaoDeAmostra.Esconder();
                }                
                break;
            case Encaixe.DESACOPLADO:
                MenuPentes.Criar(this);
                break;
        }        
    }
    public void Acoplar(Pente comb,bool alteramove)
    {
        //por o pente
        Comb = Instantiate(comb);
        Pente.SetActive(true);
        //troca o estado
        Exc = Encaixe.ACOMPLADO;
        //recebe os valores do pente
        Id = Comb.Id;
        Value = new int[6];
        for (int i=0; i<6;i++)
        {
            Value[i] = Comb.Valor[i];
        }
        
        //Verifica se é buffer e buffa os conectados
        if (Id ==5)
        {
            BuffarPentesAcoplados();
        }
        //verfica se ja tem algum buffer relacioando ao pente atual
        else
        {
            foreach (Plug pl in Conectados)
            {
                if(pl.Id ==5)
                {
                    pl.BuffarPentesAcoplados();
                }
            }
        }
        //tirar o pente do inventario
        PlayerObjects.PentesCheios.Remove(comb);      
        //adiciona pente a parte
        Placa.Parte.Pente[NumeroNaPlaca] = Instantiate(comb);
        //adiciona o move ao fantorob
        if(alteramove)
        {
            chooseRobotMenu.MeuFantorob.Fisico.ReceberMove(Placa.Parte.Pente[NumeroNaPlaca].Move);
            bool pronto = false;
            int idprev = 0;
            while(!pronto)
            {
                idprev = Random.Range(0, 99999);
                if(chooseRobotMenu.MeuFantorob.Fisico.MovesDePentes.Count>0)
                {
                    bool igual = true;
                    foreach(Move mv in chooseRobotMenu.MeuFantorob.Fisico.MovesDePentes)
                    {
                        if(mv.meuIdNoPente == idprev){ igual = false; }
                    }
                    pronto = igual;
                }
                else
                {
                    pronto = true;
                }                
            }
            Placa.Parte.Pente[NumeroNaPlaca].Move.meuIdNoPente = idprev;
        }     
        //tocarsomcofirma
        chooseRobotMenu.TocarSomConfirma();
        //aciona o compilador
        Placa.Compilador();
    }
    public void Desacoplar()
    {
        chooseRobotMenu.TocarSomDesiste();
        //retirarpentedaplaca
        Placa.Parte.Pente[NumeroNaPlaca] = null;
        //removermove do nucle ofisico
        chooseRobotMenu.MeuFantorob.Fisico.RemoverMovePente(Comb.Move.meuIdNoPente);
        //adicionar pente no inventario
        PlayerObjects.PentesCheios.Add(Instantiate(Comb));
        //verificia se é buffer se for debuffa os conectados
        if (Id == 5)
        {
            foreach (Plug pl in Conectados)
            {
                pl.DeBuffer();
            }
        }
        //Zera os valores
        Id = 9;
        for (int i = 0; i < 6; i++)
        {
            Value[i] = 0;
        }
        //apaga o pente
        Destroy(Comb);
        Pente.SetActive(false);
        //troca o estado
        Exc = Encaixe.DESACOPLADO;
        //Compila
        Placa.Compilador();
    }
    public void Buffer(int percentual)
    {
        if(Id !=5)
        {
            if (Comb != null)
            {
                Value = new int[6];
                for (int i = 0; i < 6; i++)
                {
                    Value[i] = Comb.Valor[i];
                }
                for (int i = 0; i < 5; i++)
                {
                    if (Value[i] > 0)
                    {
                        float buff = Value[i] * percentual / 100;
                        Value[i] += Mathf.RoundToInt(buff);
                    }
                }
                buffed = true;
            }
        }
    }
    public void DeBuffer()
    {
        if (buffed)
        {
            if (Comb != null)
            {
                Debug.Log(this.gameObject.name);
                Value = new int[6];
                for (int i = 0; i < 6; i++)
                {
                    Value[i] = Comb.Valor[i];
                }
                buffed = false;
            }
        }
    }
    public int RetornarMeuValor(int i)
    {
        int valor = 0;
        if(Exc == Encaixe.ACOMPLADO && Id !=5)
        {
            valor = Value[i];            
            if(i == Placa.Parte.Compilador&& Value[i]>0)
            {
                valor += Placa.Parte.Nivel;
            }
        }
        return valor;
    }
    public int RetornarMeuGasto()
    {
        int valor = 0;
        if (Exc == Encaixe.ACOMPLADO )
        {
            valor = Comb.GastoAtual;            
        }
        return valor;
    }
    void OnMouseEnter()
    {
        if (Exc == Encaixe.ACOMPLADO)
        {
            BotaoDeAmostra.gameObject.SetActive(true);
            BotaoDeAmostra.Mostrar(Comb);
        }            
    } 
    void OnMouseExit()
    {
        if (Exc == Encaixe.DESACOPLADO)
        {
            BotaoDeAmostra.gameObject.SetActive(false);
            BotaoDeAmostra.Esconder();
        }
    }
    public void LimparPlug()
    {
        Exc = Encaixe.DESACOPLADO;
        Comb = null;
        Pente.SetActive(false);
    }
    public void BuffarPentesAcoplados()
    {
        if(Id ==5)
        {
            foreach (Plug pl in Conectados)
            {
                pl.Buffer(Value[5]);
            }
        }        
    }

}
