using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTrocarBateria : MonoBehaviour
{
    public ChooseRobotMenu RobotMenu;
    public List<int> Valores = new List<int>();
    public Image Spacer;
    public Button BotaoTrocarBateria;
    public List<GameObject> botoes = new List<GameObject>();
    public Text ValorBateria;
    public void Iniciar()
    {
        this.transform.position = new Vector3(this.transform.position.x, 0.446f);
        LeanTween.moveLocalY(this.gameObject, 0.005607297f, 0.3f);
        if (botoes.Count>0)
        {
            foreach (GameObject b in botoes)
            {               
                Destroy(b);
            }
            botoes.Clear();
        }
        for (int i = 0; i < PlayerObjects.PlayerObjectsStatic.Batteries.Count; i++)
        {
            if(PlayerObjects.PlayerObjectsStatic.Batteries[i]>0)
            {
                for(int b = 0; b< PlayerObjects.PlayerObjectsStatic.Batteries[i];b++)
                {
                    Button botao = Instantiate(BotaoTrocarBateria, Spacer.transform) as Button;
                    botao.GetComponent<BotaoTrocarBateria>().Criar(this, Valores[i]);
                    botoes.Add(botao.gameObject);                
                }               
            }
        }
    }
    public void TrocarBateria (int bateria)
    {        
       for (int i =0; i<Valores.Count; i++)
        {
            if (Valores[i] == RobotMenu.MeuFantorob.Bateria)
            {
                PlayerObjects.PlayerObjectsStatic.Batteries[i]++;
                break;
            }
        }
        for (int i = 0; i < Valores.Count; i++)
        {
            if (Valores[i] == bateria)
            {
                PlayerObjects.PlayerObjectsStatic.Batteries[i]--;
                break;
            }
        }
        RobotMenu.MeuFantorob.Bateria = bateria;
        RobotMenu.MeuFantorob.BateriaAtual = bateria;
        ValorBateria.text = bateria.ToString();
        RobotMenu.TocarSomConfirma();
        Iniciar();
    }
    public void DescelecionarTodos()
    {
        if (botoes.Count > 0)
        {
            foreach (GameObject b in botoes)
            {
                b.GetComponent<BotaoTrocarBateria>().Descelecionar();
            }           
        }
    }
}
