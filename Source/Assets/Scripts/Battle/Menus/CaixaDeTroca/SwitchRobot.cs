using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchRobot : MonoBehaviour
{
    public GameObject MyRobot;
    public GameObject Gerenciador;
    private TransitionManager screemManager;
    // Start is called before the first frame update
    void Start()
    {
        screemManager = Gerenciador.GetComponent<TransitionManager>();
        MyRobot.GetComponent<RobotManager>().MeuBotao = this;
     
    }
    // Update is called once per frame
    
    public void TrocarRobo()
    {        
        if (screemManager.possoexecutar && !MyRobot.GetComponent<RobotManager>().KO && !MyRobot.GetComponent<RobotManager>().Ativo)
        {
            SonsDoMenu.Confirmar();
            Gerenciador.GetComponent<ActiveRobotManager>().IniciarTrocaPlayer(MyRobot, false);
        }
        else
        {
            SomFantoRob.SomNegado();
        }
        
    }
    public void Atualizar()
    {

        //integridade
        transform.GetChild(2).transform.GetChild(2).GetComponent<Text>().text
                = MyRobot.GetComponent<RobotManager>().integridadeAtual.ToString();
        transform.GetChild(2).transform.GetChild(3).GetComponent<Slider>().value
               = MyRobot.GetComponent<RobotManager>().integridadeAtual;
        //resistencia
        transform.GetChild(3).transform.GetChild(2).GetComponent<Text>().text
                = MyRobot.GetComponent<RobotManager>().ResistenciaAtual.ToString();
        transform.GetChild(3).transform.GetChild(3).GetComponent<Slider>().value
                = MyRobot.GetComponent<RobotManager>().ResistenciaAtual;
        //bateria
        transform.GetChild(4).transform.GetChild(0).GetComponent<Slider>().value
                = MyRobot.GetComponent<RobotManager>().bateriaAtual;
    }
}
