using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifVing : MonoBehaviour
{
    public GameObject EtiquetaAtacado;
    public GerarVinganca GerarVinganca;
    public Image Spacer;
    // Start is called before the first frame update
    void Start()
    {
        Criar();
    }
    public void Criar()
    {
        foreach(NPCBattle npc in GerarVinganca.nPCBattles)
        {
            GameObject etiq = Instantiate(EtiquetaAtacado, Spacer.transform);
            etiq.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = npc.MeuSp;
            //etiq.transform.GetChild(1).GetComponent<Text>().text = npc.Nome[ManagerGame.Instance.Idm];
            if (npc.GanhouAtaque) {
                etiq.transform.GetChild(2).gameObject.SetActive(false);
                etiq.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
    }
}
