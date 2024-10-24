using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackNote : MonoBehaviour
{
    private Attack MyAttack;
    private GameObject MyMove;
    public Button BotaoMove;
    public Image Spacer;
    public Text Id;
    public Button BotaoAtual;

    
    public void Gerar(Attack ataque, int id,FantoRob robo)
    {
        Id.text = id.ToString();
        MyAttack = ataque;
        MyMove = ataque.Move;

        CriarBotao(MyMove.GetComponent<Move>(), true);        
        
    }
    public void Selecionar(GameObject move, Button botao)
    {
        if(!move.GetComponent<Move>().escolhido)
        {
            MyMove = move;
            MyAttack.Move = move;
            move.GetComponent<Move>().escolhido = true;
            alterarTexto(BotaoAtual, 7, "");
            BotaoAtual = botao;
            alterarTexto(BotaoAtual, 7, "Escolhido");
        }
    }
    public void CriarBotao(Move move, bool escolhido)
    {
        Button botao = Instantiate(BotaoMove, Spacer.transform) as Button;
        botao.GetComponent<MoveButton>().move = move;
        botao.GetComponent<MoveButton>().menu = this;
        alterarTexto(botao, 0, move.Nome);
        alterarTexto(botao, 1, move.Forca.ToString()) ;
        alterarTexto(botao, 2, move.Precisao.ToString());
        alterarTexto(botao, 3, move.GastoEnergiaPercentual.ToString());
        if(move.elemental)
        {
            alterarTexto(botao, 4, "Elemental");
        }
        else 
        {
            alterarTexto(botao, 4, "Normal");
        }
        switch(move.Efeito)
        {
            case 0:
                alterarTexto(botao, 5, "None");
                break;
            case 1:
                alterarTexto(botao, 5, "Spy");
                break;
            case 2:
                alterarTexto(botao, 5, "Keylogger");
                break;
            case 3:
                alterarTexto(botao, 5, "Trojan");
                break;
            case 4:
                alterarTexto(botao, 5, "Ranson");
                break;
            case 5:
                alterarTexto(botao, 5, "Worm");
                break;
            case 6:
                alterarTexto(botao, 5, "Virus");
                break;

        }
        
        
        alterarTexto(botao, 6, move.AumentoEfeito.ToString());
        if(escolhido)
        {
            alterarTexto(botao, 7, "Choosen");
            BotaoAtual = botao;
            move.escolhido = true;
        }
        else
        {
            alterarTexto(botao, 7, "");
        }


    }
    private void alterarTexto(Button botao, int child, string movetext)
    {
        botao.transform.GetChild(child).GetComponent<Text>().text = movetext;
    }
    
}
