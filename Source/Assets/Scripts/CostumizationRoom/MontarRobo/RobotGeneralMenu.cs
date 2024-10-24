using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotGeneralMenu : MonoBehaviour
{
    public Button [] BotaoDaParte = new Button [6];
    [HideInInspector]
    public FantoRob MeuRobo;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Montar(FantoRob robo)
    {
        MeuRobo = robo;
        for(int i = 0; i<5; i++)
        {
            if(robo.RobotPart[i] != null)
            {
                BotaoDaParte[i].GetComponent<PartButton>().MyPart = robo.RobotPart[i];
                BotaoDaParte[i].GetComponent<PartButton>().Menu = this;
                BotaoDaParte[i].GetComponent<PartButton>().MyId = i;
            }
            
        }        
        
    }
    public void ReceberParte(RobotPart parte, int id)
    {
        BotaoDaParte[id].GetComponent<PartButton>().MyPart = parte;
        MeuRobo.ReceberPart(parte, id);
    }
    public void ReceberNucleo(GameObject nucleo, Weapon arma)
    {
       // BotaoDaParte[5].GetComponent<PartButton>().MyPart = nucleo;
        MeuRobo.ReceberNucleo(nucleo,arma);
    }
    public void Concluir()
    {
        this.gameObject.SetActive(false);
        BotaoDaParte = new Button[6];
        MeuRobo = null;
    }
}
