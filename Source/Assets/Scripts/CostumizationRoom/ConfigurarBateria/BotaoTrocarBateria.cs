using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotaoTrocarBateria : MonoBehaviour
{
    public Text Valor;
    private MenuTrocarBateria menu;
    private int MeuValor;
    public GameObject MostrarConfirma;
    enum estado
    {
        SEMSELECIONAR,
        SELECIONADO,
    }
    //private estado std;
    public void Criar(MenuTrocarBateria m, int valor)
    {
        menu = m;
        MeuValor = valor;
        Valor.text = MeuValor.ToString();
    }
    public void Clicar()
    {
        menu.TrocarBateria(MeuValor);
       // switch (std)
        //{
          //  case estado.SEMSELECIONAR:                
            //    menu.DescelecionarTodos();
              //  MostrarConfirma.SetActive(true);
                //std = estado.SELECIONADO;
               // break;
            //case estado.SELECIONADO:               
              //  MostrarConfirma.SetActive(false);
              //  menu.TrocarBateria(MeuValor);
               // std = estado.SEMSELECIONAR;
               // break;
       // }       
    }
    public void Descelecionar()
    {
       // std = estado.SEMSELECIONAR;
        MostrarConfirma.SetActive(false);
    }
}
