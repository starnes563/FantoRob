using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuTrocarAtaque : MonoBehaviour
{
    private BtVerAtaque id;
    public Image Spacer;
    public Button BotaoMove;
    public ChooseRobotMenu RobotMenu;
    private List<GameObject> bt = new List<GameObject>();
    public QuadroAtaque Quadro;
    public ConfigurarAtaques ConfigAta;
    public void Criar(BtVerAtaque i)
    {
        this.transform.position = new Vector3(this.transform.position.x, -0.563f);
        LeanTween.moveLocalY(this.gameObject,0f, 0.3f);
        if (bt.Count>0)
        {
            foreach (GameObject b in bt)
            {
                Destroy(b);
            }
            bt.Clear();
        }       
        id = i;
        criarBotao(RobotMenu.MeuFantorob.MovimentoAmbos);
        criarBotao(RobotMenu.MeuFantorob.MovimentoJogador);
        criarBotao(RobotMenu.MeuFantorob.Fisico.MovimentosAmbos);
        criarBotao(RobotMenu.MeuFantorob.Fisico.MovimentosJogador);
        criarBotao(RobotMenu.MeuFantorob.Fisico.MovesDePentes);        
        this.gameObject.SetActive(true);
    }
    void criarBotao(List<Move> Lista)
    {
        if(Lista!=null)
        {
            if(Lista.Count >0)
            {
                foreach (Move mv in Lista)
                {                    
                    Button botao = Instantiate(BotaoMove, Spacer.transform) as Button;
                    bt.Add(botao.gameObject);
                    botao.GetComponent<BotaoTrocarAtaque>().Criar(mv, this, Quadro);
                }
            }
        }        
    }
    public void TrocarAtaque(Move mv)
    {
        RobotMenu.TocarSomConfirma();
        RobotMenu.MeuFantorob.Fisico.RemoverMoveAtivo(id.ID);            
        RobotMenu.MeuFantorob.Fisico.MovesAtivos.Add(mv);
        //criar id 
        bool pronto = false;
        int idprev = 0;
        while (!pronto)
        {
            idprev = Random.Range(0, 99999);
            if (RobotMenu.MeuFantorob.Fisico.MovesAtivos.Count > 0)
            {
                bool igual = true;
                foreach (Move mov in RobotMenu.MeuFantorob.Fisico.MovesAtivos)
                {
                    if (mov.meuIdNoPente == idprev) { igual = false; }
                }
                pronto = igual;
            }
            else
            {
                pronto = true;
            }
        }
        mv.meuIdnoAtivos = idprev;
        RobotMenu.MeuFantorob.Fisico.ReceberAtaque(mv, id.ID);
        mv.meuIdnoAtaque = id.ID;
        id.Trocar(mv);       
        this.gameObject.SetActive(false);
        ConfigAta.IniciarMenu();
        id = null;
    }
}
