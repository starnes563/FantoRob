using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FantorobMenu : MonoBehaviour
{
    public Button RobotButton;
    public Image Spacer;   
    [HideInInspector]
    public FantoRob Robot;
    [HideInInspector]
    public PlayerMenu PlayerMenu;
    public QuadroRobo QuadroRobo;
    List<GameObject> botoes = new List<GameObject>();
    public PartyMenu PartyMenu;
    // Start is called before the first frame update    
    public void Criar(PlayerMenu menu)
    {
        this.transform.position = new Vector3(this.transform.position.x, -10.09f,30f);
        LeanTween.moveLocalY(this.gameObject, 0, 0.4f);        
        {            
            foreach (GameObject g in botoes)
            {                
                Destroy(g);
            }
            botoes.Clear();
        }
        PlayerMenu = menu;
        int inde = 0;
        if (PlayerObjects.RobotsNotInUse.Count>0)
        {
            foreach (FantoRob robo in PlayerObjects.RobotsNotInUse)
            {
                Button botao = Instantiate(RobotButton, Spacer.transform) as Button;
                botoes.Add(botao.gameObject);
                botao.GetComponent<FatorobButton>().MyRobot = robo;
                botao.GetComponent<FatorobButton>().menu = this;
                botao.GetComponent<FatorobButton>().QuadroRobo = QuadroRobo;
                botao.GetComponent<FatorobButton>().Index = inde;               
                inde++;
                botao.transform.GetChild(0).GetComponent<Image>().sprite = robo.MenuIconeFantorob;
                botao.transform.GetChild(1).GetComponent<Text>().text = robo.Nome;
            }
        }       
    }
    public void RefazerMenu()
    {
        if (botoes.Count > 0)
        {
            foreach (GameObject g in botoes)
            {                
                Destroy(g);
            }
            botoes.Clear();
        }
        if (PlayerObjects.RobotsNotInUse.Count > 0)
        {            
            int inde = 0;
            foreach (FantoRob robo in PlayerObjects.RobotsNotInUse)
            {
                Button botao = Instantiate(RobotButton, Spacer.transform) as Button;
                botoes.Add(botao.gameObject);
                botao.GetComponent<FatorobButton>().MyRobot = robo;
                botao.GetComponent<FatorobButton>().menu = this;
                botao.GetComponent<FatorobButton>().QuadroRobo = QuadroRobo;
                botao.GetComponent<FatorobButton>().Index = inde;
                inde++;
                botao.transform.GetChild(0).GetComponent<Image>().sprite = robo.MenuIconeFantorob;
                botao.transform.GetChild(1).GetComponent<Text>().text = robo.Nome;
            }
        }
    }
    public void Mostrar()
    {
        PlayerMenu.AbrirMenuCore(Robot);
    }
    public void AdicionarAParty(int index)
    {       
        if(PlayerObjects.RobotsInUse.Count<3)
        {          
            PlayerObjects.RobotsInUse.Add(Robot);           
            FantoRob rob = PlayerObjects.RobotsNotInUse[index];            
            PlayerObjects.RobotsNotInUse.Remove(rob);           
            SonsMenu.Confimar();
        }
        else
        {
            SonsMenu.Negado();
        }       
        Criar(this.PlayerMenu);
        PartyMenu.RefazerMenu();
    }
    public void Fechar()
    {
        Robot = null;
        this.gameObject.SetActive(false);
    }

    public void EsconderTodos()
    {       
        if (botoes.Count > 0)
        {
            foreach (GameObject g in botoes)
            {
                g.GetComponent<FatorobButton>().MenuSecundario.SetActive(false);
            }            
        }
    }
}
