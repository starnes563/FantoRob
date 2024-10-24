using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    [HideInInspector]
    public BattleManager BattleManager;
    [HideInInspector]
    public WeaponMethods WeaponMethod;
    public AudioClip MeuSom;
    public GameObject AvisoParentObject;
    AvisoAtaque MeuAviso;
    public enum Tipo
    {
        FISICO,
        ELEMENTAL,
    }
    public Tipo MeuTipo;
    // 1 = 1 centesimo de segundo
    float janela;
    float contador;
    bool ativado = false;
    bool acertou = false;
    bool pode = true;
    // Start is called before the first frame update
    public void FinalizarAnimacao()
    {       
        if (BattleManager.BattleState == BattleManager.BattleStateMachine.ENEMYTURN ||
            BattleManager.BattleState == BattleManager.BattleStateMachine.ENEMYANIMATION ||
            BattleManager.BattleState == BattleManager.BattleStateMachine.ENEMYTRADE)
        {            
            if (acertou)
            {                
                switch (MeuTipo)
                {
                    case Tipo.FISICO:
                        BattleManager.ExecutarDano(0.2f, 0);
                        break;
                    case Tipo.ELEMENTAL:
                        BattleManager.ExecutarDano(1f, 40);
                        break;
                }
            }
            else
            {
                BattleManager.ExecutarDano(1f, 0);
            }
        }
        else
        {
            BattleManager.ExecutarDano(1f, 0);
        }              
        Destroy(gameObject);
    }
    public void TocarSom()
    {
        if (GetComponent<AudioSource>().isPlaying) { GetComponent<AudioSource>().Stop(); }
        GetComponent<AudioSource>().PlayOneShot(MeuSom);
        GetComponent<AudioSource>().loop = false;
    }
    public void TocarSomLoop()
    {
        if (GetComponent<AudioSource>().isPlaying) { GetComponent<AudioSource>().Stop(); }
        GetComponent<AudioSource>().clip = MeuSom;
        GetComponent<AudioSource>().loop = true;
    }
    public void AtivaJanela(float jan)
    {
        if(BattleManager.BattleState == BattleManager.BattleStateMachine.ENEMYTURN ||
            BattleManager.BattleState == BattleManager.BattleStateMachine.ENEMYANIMATION ||
            BattleManager.BattleState == BattleManager.BattleStateMachine.ENEMYTRADE )
           
        {
            //Ativar o sinal
            switch (MeuTipo)
            {
                case Tipo.FISICO:
                    MeuAviso = Instantiate(BattleManager.AvisoFisico, AvisoParentObject.transform.position,Quaternion.identity).GetComponent<AvisoAtaque>();
                    break;
                case Tipo.ELEMENTAL:
                    MeuAviso = Instantiate(BattleManager.AvisoElemental, AvisoParentObject.transform.position, Quaternion.identity).GetComponent<AvisoAtaque>();
                    break;
            }
            MeuAviso.Ancora = AvisoParentObject;
            MeuAviso.SourceAcertou = BattleManager.SourceAcertou;
            janela = jan / 100;
            contador = 0;
            acertou = false;
            ativado = true;
        }       
    }
    private void Update()
    {
        if (ativado) { contador += Time.deltaTime; }
        if (contador > janela&&ativado)
        {
            pode = false;
            ativado = false;
            acertou = false;
        }
        switch (MeuTipo)
        {
            case Tipo.FISICO:
                if(Input.GetMouseButtonDown(1))
                {
                    if(ativado)
                    {
                        if(pode)
                        {
                            if(contador<=janela)
                            {
                                acertou = true;
                                MeuAviso.SomAcertou();                                
                                ativado = false;
                            }
                            else
                            {
                                acertou = false;
                                pode = false;
                            }
                        }
                    }
                    else
                    {
                        pode = false;
                        ativado = false;
                        acertou = false;
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    pode = false;
                    ativado = false;
                    acertou = false;
                }
                break;
            case Tipo.ELEMENTAL:
                if (Input.GetMouseButtonDown(0))
                {
                    if (ativado)
                    {
                        if (pode)
                        {
                            if (contador <= janela)
                            {
                                acertou = true;
                                MeuAviso.SomAcertou();                               
                                ativado = false;
                            }
                            else
                            {
                                acertou = false;
                                pode = false;
                            }
                        }
                    }
                    else
                    {
                        pode = false;
                        ativado = false;
                        acertou = false;
                    }
                }
                if (Input.GetMouseButtonDown(1))
                {
                    pode = false;
                    ativado = false;
                    acertou = false;
                }
                break;
        }
    }

}
