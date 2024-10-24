using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerarVinganca : MonoBehaviour
{
    public GerarRivalAletorio GerarRival;
    [HideInInspector]
    public List<NPCBattle> nPCBattles;
    public void GerarV()
    {
        nPCBattles.Clear();
        //decidir quantas pessoas atacaram
        int ataques = Random.Range(0, 11);
        //criar as pessoas
        if(ataques>0)
        {
            for(int i = 0; i<ataques;i++)
            {
                // gerar
               // GerarRival.GerarBatalha(nPCBattles[i]);
                //decidir se venceu
                if (Random.Range(0,101) < 70) { nPCBattles[i].GanhouAtaque = true; retirarInventario(); }
                //adicionar
                ManagerGame.Instance.NPCAtacou.Add(nPCBattles[i]);
            }
        }        
    }
    void retirarInventario()
    {
        if(PlayerObjects.Fantodin>0)
        {
            PlayerObjects.Fantodin--;
        }        
        if(Random.Range(0,101)<60)
        {
            //decide qual item vai perder
            int qualperder = Random.Range(0, 6);
           switch(qualperder)
            {
                case 0:
                    if (PlayerObjects.PentesCheios.Count > 0)
                    {
                        PlayerObjects.PentesCheios.RemoveAt(Random.Range(0, PlayerObjects.PentesCheios.Count + 1));
                    }
                    break;
                case 1:
                    if (PlayerObjects.PentesVazios.Count > 0)
                    {
                        PlayerObjects.PentesVazios.RemoveAt(Random.Range(0, PlayerObjects.PentesVazios.Count + 1));
                    }
                    break;
                case 2:
                    int index = Random.Range(0, 16);
                    if (PlayerObjects.Circuits[index] > 0)
                    {
                        PlayerObjects.Circuits[index]--;
                    }
                    break;
                case 3:
                    if (PlayerObjects.Silicon > 0)
                    {
                        PlayerObjects.Silicon--;
                    }
                    break;
                case 4:
                    if (PlayerObjects.RobotParts.Count > 0)
                    {
                        PlayerObjects.RobotParts.RemoveAt(Random.Range(0, PlayerObjects.RobotParts.Count + 1));
                    }
                    break;
                case 5:
                    if (PlayerObjects.ItensConstruir.Count > 0)
                    {
                        PlayerObjects.ItensConstruir.RemoveAt(Random.Range(0, PlayerObjects.ItensConstruir.Count + 1));
                    }
                    break;
            }
        }
    }
}
