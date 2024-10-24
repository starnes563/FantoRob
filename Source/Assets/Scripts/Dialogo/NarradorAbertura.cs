using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarradorAbertura : MonoBehaviour
{
    private GerenciadorDialogo dialogo;
    private TextoDeBatalha texto;
    public TransitionManager manager;
    public RobotManager PlayerRobot;
    public RobotManager EnemyRobot;
    // Start is called before the first frame update
    void Awake()
    {
        dialogo = GetComponent<GerenciadorDialogo>();
        texto = GetComponent<TextoDeBatalha>();
        texto.TextoPadrao = true;
        
    }
    void Start()
    {
        StartCoroutine(comeco());
    }

    // Update is called once per frame
    void Update()
    {
        terminar(); 
    }
    IEnumerator comeco()
    {
        manager.EsconderCaixaDeAcao();
        dialogo.DisplayNextSetence();
        int i = 0;
        while (i<2)
        {
            if(!dialogo.DialogoDigitando)
            {
                manager.EsconderCaixaDeAcao();
                dialogo.DisplayNextSetence();
                i++;
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        manager.MostrarCaixaDeAcao();        
        texto.TextoPadrao = false;
    }
    void terminar()
    {
        if(PlayerRobot.KO || EnemyRobot.KO)
        {
            dialogo.DisplayNextSetence();
        }
    }
}
