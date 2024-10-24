using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchManager : MonoBehaviour
{
    public List<Button> BotaoDeTroca = new List<Button>(3);
    private ActiveRobotManager robotManager;
    // Start is called before the first frame update
    public void GerarMenuDeTroca(ActiveRobotManager robotman)
    {
        if (!robotman.Abertura)
        {           
            robotManager = robotman;
            for (int i = 0; i < robotManager.PlayerRobots.Count; i++)
            {                
                BotaoDeTroca[i].GetComponent<SwitchRobot>().MyRobot = robotManager.PlayerRobots[i];
                //sprite
                BotaoDeTroca[i].transform.GetChild(0).GetComponent<Image>().sprite = robotManager.PlayerRobots[i].GetComponent<Status>().MeuSprite;
                //nome
                BotaoDeTroca[i].transform.GetChild(1).GetComponent<Text>().text = robotManager.PlayerRobots[i].GetComponent<Status>().Nome;

                //integridade
                //numero maximo
                BotaoDeTroca[i].transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text
                    = robotManager.PlayerRobots[i].GetComponent<Status>().Integridade.ToString();
                //numero atual
                BotaoDeTroca[i].transform.GetChild(2).transform.GetChild(2).GetComponent<Text>().text
                    = robotManager.PlayerRobots[i].GetComponent<RobotManager>().integridadeAtual.ToString();
                //atualiza barra
                BotaoDeTroca[i].transform.GetChild(2).transform.GetChild(3).GetComponent<Slider>().maxValue
                    = robotManager.PlayerRobots[i].GetComponent<Status>().Integridade;
                BotaoDeTroca[i].transform.GetChild(2).transform.GetChild(3).GetComponent<Slider>().value
                    = robotManager.PlayerRobots[i].GetComponent<RobotManager>().integridadeAtual;

                //resistencia
                //numero maximo
                BotaoDeTroca[i].transform.GetChild(3).transform.GetChild(0).GetComponent<Text>().text
                    = robotManager.PlayerRobots[i].GetComponent<Status>().Resistencia.ToString();
                //numero atual
                BotaoDeTroca[i].transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text
                    = robotManager.PlayerRobots[i].GetComponent<RobotManager>().ResistenciaAtual.ToString();
                //atualiza barra
                BotaoDeTroca[i].transform.GetChild(3).transform.GetChild(3).GetComponent<Slider>().maxValue
                    = robotManager.PlayerRobots[i].GetComponent<Status>().Resistencia;
                BotaoDeTroca[i].transform.GetChild(3).transform.GetChild(3).GetComponent<Slider>().value
                    = robotManager.PlayerRobots[i].GetComponent<RobotManager>().ResistenciaAtual;

                //bateria
                BotaoDeTroca[i].transform.GetChild(4).transform.GetChild(0).GetComponent<Slider>().maxValue
                    = robotManager.PlayerRobots[i].GetComponent<Status>().Bateria;
                BotaoDeTroca[i].transform.GetChild(4).transform.GetChild(0).GetComponent<Slider>().value
                    = robotManager.PlayerRobots[i].GetComponent<RobotManager>().bateriaAtual;

                BotaoDeTroca[i].gameObject.SetActive(true);               
            }
        }
    }    
}