using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomInBaixo : MonoBehaviour
{
    public ActiveRobotManager Manager;
    public AudioSource Audio;   
    public bool posso = false;
    // Update is called once per frame
    public void Awake()
    {
        if(Manager == null)
        {
            Manager = GameObject.FindWithTag("Gerenciador").GetComponent<ActiveRobotManager>();
        }
    }
    void Update()
    {
        if (posso && Manager.ActivePlayerRobot!=null && Manager.ActivePlayerRobot.gameObject.activeSelf)
        {            
            if (Manager.ActivePlayerRobot.GetComponent<RobotManager>().integridadeAtual <
                Manager.ActivePlayerRobot.GetComponent<Status>().Integridade * 0.3f && !Audio.isPlaying || Manager.ActivePlayerRobot.GetComponent<RobotManager>().bateriaAtual <
                Manager.ActivePlayerRobot.GetComponent<Status>().Bateria * 0.3f && !Audio.isPlaying)
            {               
                Audio.Play();                           
            }
            if (Manager.ActivePlayerRobot.GetComponent<RobotManager>().integridadeAtual <
                Manager.ActivePlayerRobot.GetComponent<Status>().Integridade * 0.1f && Audio.isPlaying || Manager.ActivePlayerRobot.GetComponent<RobotManager>().bateriaAtual <
                Manager.ActivePlayerRobot.GetComponent<Status>().Bateria * 0.1f && Audio.isPlaying)
            {                
                Audio.pitch = 2f;
            }
            if (Manager.ActivePlayerRobot.GetComponent<RobotManager>().integridadeAtual >
                Manager.ActivePlayerRobot.GetComponent<Status>().Integridade * 0.3f && Audio.isPlaying && Manager.ActivePlayerRobot.GetComponent<RobotManager>().bateriaAtual >
                Manager.ActivePlayerRobot.GetComponent<Status>().Bateria * 0.3f && Audio.isPlaying || Manager.ActivePlayerRobot.GetComponent<RobotManager>().KO)
            {                
                Audio.Stop();                           
            }                
           
        }
    }
}
