using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoMapaGoianopolis : MonoBehaviour
{
    // Start is called before the first frame update
    public MapaGoianopolis MeuMapa;
    public List<string> MeuNome;  

    private void OnMouseEnter()
    {
        MeuMapa.ExibirBotao(MeuNome[ManagerGame.Instance.Idm]);
    }
    private void OnMouseExit()
    {
        MeuMapa.NaoExibir();
    }
    public void Clicar()
    {
        MeuMapa.ExibirBotao(MeuNome[ManagerGame.Instance.Idm]);
    }

}
