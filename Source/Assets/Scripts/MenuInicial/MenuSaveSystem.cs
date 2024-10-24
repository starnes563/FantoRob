using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSaveSystem : MonoBehaviour
{
    public List<BotaoSaveSystem> Botoes;
    public AcionarSaveSystem AcionarSaveSystem;
    public MenuInicial MenuInicial;
    public GameObject Confirmacao;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (ManagerGame.Instance.SavePath.Count > 0)
        {
            for (int i = 0; i < ManagerGame.Instance.SavePath.Count; i++)
            {
                Botoes[i].ID = i;
                DadosJogador j = SaveSystem.ListaSalvo(i);
                if(j==null)
                {
                    Botoes[i].ArquivoInexistente();
                }
                else
                {
                    Botoes[i].ArquivoExistente(j.Fantodin, j.Tempo, j.Estrelas);
                }               
            }
        }
    }
    public void DesselecionaTudo()
    {
        foreach(BotaoSaveSystem bt in Botoes)
        {
            bt.Desseleciona();
        }
    }
    public void MostrarConfirmacao()
    {
        Confirmacao.SetActive(true);              
    }
}
