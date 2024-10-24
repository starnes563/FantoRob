using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransicaoBatalha : MonoBehaviour
{
    private ActiveRobotManager manager;
    private void Start()
    {
        manager = ManagerGame.Instance.Regiao.GerenteTransicao.GetComponent<ActiveRobotManager>();
    }
    public void Transitar()
    {
        ManagerGame.Instance.TrocarCenaBatalha();
        
    }
    public void Destruir()
    {
        ManagerGame.Instance.Transitando = false;
        manager.CanIStart = true;
        Destroy(this.gameObject);
    }
}
