using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetasTrocar : MonoBehaviour
{
    public int ID;
    public TelaVingaca TelaVinganca;
    public bool SetaDireita;
    public bool SetaEsquerda;
    void Start()
    {        
        if(ID == 0) { ID = ManagerGame.Instance.NPCAtacou.Count - 1; }
    }

   public void Clicou()
    {
        if(ManagerGame.Instance.NPCAtacou[ID] !=null)
        {
            TelaVinganca.Criar(ID);
            if(SetaDireita)
            {
                ID++;
                if(ID> ManagerGame.Instance.NPCAtacou.Count-1)
                {
                    ID = 0;
                }
            }
            else if(SetaEsquerda)
            {
                ID--;
                if(ID<0)
                {
                    ID = ManagerGame.Instance.NPCAtacou.Count - 1;
                }
            }
        }
    }
}
