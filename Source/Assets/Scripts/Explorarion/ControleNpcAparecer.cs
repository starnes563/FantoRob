using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleNpcAparecer : MonoBehaviour
{
    public int DE = 0;
    public int ATE = 0;
    public enum Condicao
    {
        SEM,
        CLASSIFICADO,
        DESCLASSIFICADO,
        DIA,
        MARCADORTRUE,
        MARCADORFALSE,
        SIDEQUEST,
    }
    public Condicao MinhaCondicao = Condicao.SEM;
    public List<int> MeusDias = new List<int>();
    public int MeuMarcador;
    // Start is called before the first frame update
    void Awake()
    {
        if(PlayerStatus.ControleDeCena >=DE && PlayerStatus.ControleDeCena<ATE)
        {
            if (posso())
            {
                this.gameObject.SetActive(true);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    bool posso()
    {
        bool pd = false;
        switch (MinhaCondicao)
        {
            case Condicao.SEM:
                if (PlayerStatus.ControleDeCena >= 24 && PlayerStatus.ControleDeCena < 43)
                {
                    pd = false;
                }
                else
                {
                    pd = true;
                }
                   
                break;
            case Condicao.CLASSIFICADO:
                if (PlayerStatus.Posicao <= 8) { pd = true; }
                break;
            case Condicao.DESCLASSIFICADO:
                if (PlayerStatus.Posicao > 8) { pd = true; }
                break;
            case Condicao.DIA:
                foreach (int d in MeusDias)
                {
                    if (d == PlayerStatus.DaysLeft) { pd = true; break; }
                }
                break;
            case Condicao.MARCADORTRUE:
                if (StoryEvents.MarcadoresDesafio[MeuMarcador]) { pd = true; }
                break;
            case Condicao.MARCADORFALSE:
                if (!StoryEvents.MarcadoresDesafio[MeuMarcador]) { pd = true; }
                break;
            case Condicao.SIDEQUEST:
                pd = true;
                break;
        }
        return pd;
    }
}
