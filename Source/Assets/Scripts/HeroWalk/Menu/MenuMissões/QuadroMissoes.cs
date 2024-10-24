using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuadroMissoes : MonoBehaviour
{
    public Text Nome;
    public Text Descricao;
    public Text Atual;
    public Text Total;
    Quest MinhaQuest;
    public RandomQuest Menu;
    public List<MostrarRecompensa> MostrarRecompensas;
    public void Mostrar(Quest missao)
    {
        Nome.text = missao.Nome[ManagerGame.Instance.Idm];
        Descricao.text = missao.Descriçao[ManagerGame.Instance.Idm];
        Atual.text = missao.Atual.ToString();
        Total.text = missao.Requerido.ToString();
        for(int i = 0; i< missao.Recompensas.Count;i++)
        {
            MostrarRecompensas[i].Mostrar(missao.Recompensas[i]);
        }
    }
    public void Esconder()
    {
        Nome.gameObject.SetActive(false);
        Descricao.gameObject.SetActive(false);
        Atual.gameObject.SetActive(false);
        Total.gameObject.SetActive(false);
    }
    public void PegarMissao()
    {
        MinhaQuest.AdicionarMissao();
        Menu.RetirarQuest(MinhaQuest);
    }
}
