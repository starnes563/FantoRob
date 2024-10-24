using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCenaBatalha : MonoBehaviour
{
    public GatilhoCutscene[] Batalha1 = new GatilhoCutscene[2];
    public GatilhoCutscene[] Batalha2 = new GatilhoCutscene[2];
    public GatilhoCutscene[] Batalha3 = new GatilhoCutscene[2];
    enum Estado
    {
        GANHOU,
        PERDEU,
    }
    Estado meuEstado;
    // Start is called before the first frame update
    void Start()
    {
        ManagerGame.Instance.GanhouBatalha += Ganhar;
        ManagerGame.Instance.PerdeuBatalha += Perder;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Perder()
    {
        meuEstado = Estado.PERDEU;
    }
    void Ganhar()
    {
        meuEstado = Estado.GANHOU;
    }
    public void Executar()
    {
        ManagerGame.Instance.GanhouBatalha -= Ganhar;
        ManagerGame.Instance.PerdeuBatalha -= Perder;       
        switch(PlayerStatus.ControleDeCena)
        {
            case 13:
                if (meuEstado == Estado.PERDEU)
                {
                    Batalha1[1].Iniciar();
                }
                else
                {
                    Batalha1[0].Iniciar();
                }
                break;
            case 62:
                if(meuEstado == Estado.PERDEU)
                {
                    Batalha1[1].Iniciar();
                }
                else
                {
                    Batalha1[0].Iniciar();
                }
                break;
            case 64:
                if (meuEstado == Estado.PERDEU)
                {
                    Batalha2[1].Iniciar();
                }
                else
                {
                    Batalha2[0].Iniciar();
                }
                break;
            case 67:
                if (meuEstado == Estado.PERDEU)
                {
                    Batalha3[1].Iniciar();
                }
                else
                {
                    Batalha3[0].Iniciar();
                }
                break;

        }
    }
    public void LoadSave()
    {
        ManagerGame.Instance.SceneToLoad = 0;
        ManagerGame.Instance.LoadNextScene();
    }
}
