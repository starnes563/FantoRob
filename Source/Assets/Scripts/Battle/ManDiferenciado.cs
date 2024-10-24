using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManDiferenciado : MonoBehaviour
{
    private BattleManager battleManager;
    private ActiveRobotManager activeRobotManager;
    private WeaponMethods weapMeth;
    //variaveis do fio armadilha
    private bool fiopl = false;
    private bool fioriv = false;
    bool desligarfiopl = false;
    bool desligarfioriv = false;
    float danofiopl = 0;
    float danofioriv = 0;
    //variaveis circulos elementais
    private bool circelepl = false;
    private bool circeleriv = false;
    bool desligarcircpl = false;
    bool desligarcircriv = false;
    float danocirpl = 0;
    float danocicriv = 0;
    int elempl;
    int elemriv;
    //barreiras
    int barreirafisipl;
    int barreiraelempl;
    int barreirafisiriv;
    int barreiraelemriv;
    //contadorpentatonica
    int contpentpl;
    int contpentriv;
    bool pentpl;
    bool pentriv;
    //contadorpoli
    int contpolipl;
    int contpoliriv;
    bool polipl;
    bool poliriv;
    // Start is called before the first frame update
    void Start()
    {
        battleManager = GetComponent<BattleManager>();
        activeRobotManager = GetComponent<ActiveRobotManager>();
    }
    public void KoFantoRob(bool player)
    {
        if(player)
        {
            contpentpl = 0;
            if(pentpl)
            {
                pentpl = false;
                battleManager.JogadorFimTurno -= ContarPentPl;
            }          
            contpolipl = 0;
            if(polipl)
            {
                polipl = false;
                battleManager.JogadorFimTurno -= ContarPoliPl;
            }           
        }
        else
        {
            contpentriv = 0;
            if(pentriv)
            {
                pentriv = false;
                battleManager.RivalFimTurno -= ContarPentRiv;
            }            
            contpoliriv = 0;
            if(poliriv)
            {
                poliriv = false;
                battleManager.RivalFimTurno -= ContarPoliRiv;
            }
           
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (fiopl)
        {
            if (battleManager.BattleState == BattleManager.BattleStateMachine.PLAYERANIMATION)
            {
                danofiopl += (float)activeRobotManager.ActivePlayerRobot.GetComponent<Status>().Integridade * (float)0.1 * Time.deltaTime;
                desligarfiopl = true;
            }
            if (battleManager.BattleState == BattleManager.BattleStateMachine.START && desligarfiopl)
            {
                danofiopl *= 0.05f;
                activeRobotManager.ActivePlayerRobot.GetComponent<RobotManager>().TomarDano(danofiopl, 0, 2);
                desligarfiopl = false;
                fiopl = false;
            }
        }
        if (fioriv)
        {
            if (battleManager.BattleState == BattleManager.BattleStateMachine.ENEMYANIMATION)
            {
                danofioriv += (float)activeRobotManager.ActiveEnemyRobot.GetComponent<Status>().Integridade * (float)0.1 * Time.deltaTime;
                desligarfioriv = true;
            }
            if (battleManager.BattleState == BattleManager.BattleStateMachine.IDLE && desligarfioriv)
            {
                activeRobotManager.ActiveEnemyRobot.GetComponent<RobotManager>().TomarDano(danofioriv, 0, 2);
                desligarfioriv = false;
                fioriv = false;
            }
        }
        if (circelepl)
        {
            if (battleManager.BattleState == BattleManager.BattleStateMachine.PLAYERANIMATION)
            {
                danocirpl += (float)activeRobotManager.ActivePlayerRobot.GetComponent<Status>().Integridade * (float)0.2 * Time.deltaTime;
                danocirpl *= battleManager.fatorElemental(elempl, battleManager.enemyManager.MyStatus.NucleoElemental);
                desligarcircpl = true;
            }
            if (battleManager.BattleState == BattleManager.BattleStateMachine.PLAYERTURN && desligarcircpl)
            {
                int tipodn = 2;
                if (battleManager.fatorElemental(elempl, battleManager.enemyManager.MyStatus.NucleoElemental) > 1) { tipodn = 3; }
                activeRobotManager.ActivePlayerRobot.GetComponent<RobotManager>().TomarDano(danocirpl, 0, tipodn);
                desligarcircpl = false;
                circelepl = false;
            }
        }
        if (circeleriv)
        {
            if (battleManager.BattleState == BattleManager.BattleStateMachine.ENEMYANIMATION)
            {
                danocicriv += (float)activeRobotManager.ActiveEnemyRobot.GetComponent<Status>().Integridade * (float)0.2 * Time.deltaTime;
                danocicriv *= battleManager.fatorElemental(elemriv, battleManager.playerManager.MyStatus.NucleoElemental);
                desligarcircriv = true;
            }
            if (battleManager.BattleState == BattleManager.BattleStateMachine.PLAYERANIMATION && desligarcircriv)
            {
                int tipodn = 2;
                if (battleManager.fatorElemental(elempl, battleManager.enemyManager.MyStatus.NucleoElemental) > 1) { tipodn = 3; }
                activeRobotManager.ActiveEnemyRobot.GetComponent<RobotManager>().TomarDano(danocicriv, 0, tipodn);
                desligarcircriv = false;
                circeleriv = false;
            }
        }
    }
    public void AtacarDiferenciado(int id, WeaponMethods wp)
    {
        weapMeth = wp;
        switch (id)
        {
            case 18:
                protecao(5);
                break;
            case 21:
                fioarmadinha();
                break;
            case 66:
                circuloElemental(0);
                break;
            case 106:
                circuloElemental(2);
                break;
            case 118:
                ligarBarreiraFisica(3);
                break;
            case 38:
                ligarbarreiraElem(4);
                break;
            case 39:
                ligarBarreiraFisica(4);
                break;
            case 160:
                protecao(12);
                break;
            case 73:
                pentatonica();
                break;
            case 64:
                standby();
                break;
            case 79:
                roubarBateria();
                break;
            case 119:
                autoPolimento();
                break;
        }
    }
    void protecao(int vl)
    {       
        weapMeth.myRobot.ResistenciaAtual += vl;
        weapMeth.myRobot.atualizaBarraResistencia();
    }    
    void fioarmadinha()
    {
        if (weapMeth.myRobot.PlayerRobot)
        {
            fioriv = true;
        }
        else
        {
            fiopl = true;
        }
    }
    void circuloElemental(int el)
    {
        if (weapMeth.myRobot.PlayerRobot)
        {
            circelepl = true;
            elempl = el;
        }
        else
        {
            circeleriv = true;
            elemriv = el;
        }
    }
    void ligarbarreiraElem(int turnos)
    {
        if (weapMeth.myRobot.PlayerRobot)
        {
            if (turnos - barreiraelempl > 0) { barreiraelempl += turnos - barreiraelempl; }
            if (!battleManager.barreiraelempl)
            {
                battleManager.barreiraelempl = true;
                battleManager.JogadorFimTurno += contarBarreiraElemPlay;
            }
        }
        else
        {
            if (turnos - barreiraelemriv > 0) { barreiraelemriv += turnos - barreiraelemriv; }
            if (!battleManager.barreiraelemriv)
            {
                battleManager.barreiraelemriv = true;
                battleManager.RivalFimTurno += contarBarreiraElemRiv;
            }
        }
    }
    public void contarBarreiraElemPlay()
    {
        barreiraelempl--;
        if (barreiraelempl <= 0)
        {
            battleManager.barreiraelempl = false;
            battleManager.JogadorFimTurno -= contarBarreiraElemPlay;
            barreiraelempl = 0;
        }
    }
    public void contarBarreiraElemRiv()
    {
        barreiraelemriv--;
        if (barreiraelemriv<=0)
        {
            battleManager.barreiraelemriv = false;
            battleManager.RivalFimTurno -= contarBarreiraElemRiv;
            barreiraelemriv = 0;
        }
    }
    void ligarBarreiraFisica(int turnos)
    {
        if (weapMeth.myRobot.PlayerRobot)
        {
            if (turnos - barreirafisipl > 0) { barreirafisipl += turnos - barreirafisipl; }
            if (!battleManager.barreirafisicapl)
            {
                battleManager.barreirafisicapl = true;
                battleManager.JogadorFimTurno += contarBarreiraFisPlay;
            }
        }
        else
        {
            if (turnos - barreirafisiriv > 0) { barreirafisiriv += turnos - barreirafisiriv; }
            if (!battleManager.barreiraelemriv)
            {
                battleManager.barreirafisicariv = true;
                battleManager.RivalFimTurno += contarBarreiraFisRiv;
            }
        }
    }
    public void contarBarreiraFisPlay()
    {
        barreirafisipl--;
        if(barreirafisipl<=0)
        {
            battleManager.barreirafisicapl = false;
            battleManager.JogadorFimTurno -= contarBarreiraFisPlay;
            barreirafisipl = 0;
        }
    }
    public void contarBarreiraFisRiv()
    {
        barreirafisiriv--;
        if(barreirafisiriv<=0)
        {
            battleManager.barreirafisicariv = false;
            battleManager.RivalFimTurno -= contarBarreiraFisRiv;
            barreirafisiriv = 0;
        }

    }
    void pentatonica()
    {       
        if (weapMeth.myRobot.PlayerRobot)
        {
            contpentpl += 3 - contpentpl;
            if (!pentpl)
            {
                weapMeth.myRobot.AtaqueAtual *= 1.15f;
                weapMeth.myRobot.AtaqueEnergeticoAtual *= 1.15f;
                battleManager.JogadorFimTurno += ContarPentPl;
            }            
        }
        else
        {
            contpentriv += 3 - contpentriv;
            if (!pentriv)
            {
                weapMeth.myRobot.AtaqueAtual *= 1.15f;
                weapMeth.myRobot.AtaqueEnergeticoAtual *= 1.15f;
                battleManager.RivalFimTurno += ContarPentRiv;
            }
        }
    }
    public void ContarPentPl()
    {
        contpentpl--;
        if(contpentpl<=0)
        {
            weapMeth.myRobot.AtaqueAtual *= weapMeth.myRobot.MyStatus.Ataque;
            weapMeth.myRobot.AtaqueEnergeticoAtual *= weapMeth.myRobot.MyStatus.AtaqueEnergetico;
            pentpl = false;
            contpentpl = 0;
            battleManager.JogadorFimTurno -= ContarPentPl;
        }
    }
    public void ContarPentRiv()
    {
        contpentriv--;
        if(contpentriv<=0)
        {
            weapMeth.myRobot.AtaqueAtual *= weapMeth.myRobot.MyStatus.Ataque;
            weapMeth.myRobot.AtaqueEnergeticoAtual *= weapMeth.myRobot.MyStatus.AtaqueEnergetico;
            pentriv = false;
            contpentriv = 0;
            battleManager.RivalFimTurno -= ContarPentRiv;
        }
    }
    void standby()
    {
        float inte = weapMeth.myRobot.integridadeAtual;
        weapMeth.myRobot.integridadeAtual += weapMeth.myRobot.integridadeAtual * 0.35f;
        if (weapMeth.myRobot.integridadeAtual > weapMeth.myRobot.MyStatus.Integridade)
        {
            weapMeth.myRobot.integridadeAtual = weapMeth.myRobot.MyStatus.Integridade;
        }
        StartCoroutine(weapMeth.myRobot.atualizaBarraIntegridade(inte, weapMeth.myRobot.MyStatus.Integridade / 2));
        weapMeth.myRobot.ResistenciaAtual += weapMeth.myRobot.ResistenciaAtual * 0.35f;
        if(weapMeth.myRobot.ResistenciaAtual>weapMeth.myRobot.MyStatus.Resistencia)
        {
            weapMeth.myRobot.ResistenciaAtual = weapMeth.myRobot.MyStatus.Resistencia;
        }
        weapMeth.myRobot.atualizaBarraResistencia();
    }
    void roubarBateria()
    {
        if (weapMeth.myRobot.PlayerRobot)
        {
            battleManager.enemyManager.GastoBateria(100);
        }
        else
        {
            battleManager.playerManager.GastoBateria(100);           
        }
    }
    void autoPolimento()
    {
        if (weapMeth.myRobot.PlayerRobot)
        {
            contpolipl += 3 - contpolipl;
            if(!polipl)
            {
                weapMeth.myRobot.VelocidadeAtual *= 1.2f;
                battleManager.JogadorFimTurno += ContarPoliPl;
            }
        }
        else
        {
            contpoliriv += 3 - contpoliriv;
            if(!poliriv)
            {
                weapMeth.myRobot.VelocidadeAtual *= 1.2f;
                battleManager.RivalFimTurno += ContarPoliRiv;
            }
        }
    }
    public void ContarPoliPl()
    {
        contpolipl--;
        if (contpolipl <= 0)
        {
            contpolipl = 0;
            polipl = false;
            weapMeth.myRobot.VelocidadeAtual = weapMeth.myRobot.MyStatus.Velocidade;
            battleManager.JogadorFimTurno -= ContarPoliPl;
        }
    }
    public void ContarPoliRiv()
    {
        contpoliriv--;
        if (contpoliriv <= 0)
        {
            contpoliriv = 0;
            poliriv = false;
            weapMeth.myRobot.VelocidadeAtual = weapMeth.myRobot.MyStatus.Velocidade;
            battleManager.RivalFimTurno -= ContarPoliRiv;
        }
    }
}


