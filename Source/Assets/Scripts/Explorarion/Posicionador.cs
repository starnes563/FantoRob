using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Posicionador : MonoBehaviour
{
    public List<GameObject> Neftari = new List<GameObject>();
    public GameObject Marcus;
    public GameObject Luiza;
    public int NeftariTemporario;
    private void Start()
    {       
        if (Neftari != null && Neftari.Count>0)
        {
            foreach (GameObject g in Neftari)
            {
                g.SetActive(false);
            }
            Neftari[PlayerStatus.PersonagemAtual].transform.position = PlayerStatus.NextHeroPosition;            
            Neftari[PlayerStatus.PersonagemAtual].gameObject.SetActive(true);            
            Neftari[PlayerStatus.PersonagemAtual].GetComponent<Animator>().Play(PlayerStatus.ProximaAnimacao);
            NeftariTemporario = PlayerStatus.PersonagemAtual;
        }
        switch (PlayerStatus.ProximaAnimacao)
        {
            case "Base Layer.IdleFrente":
                posicionanoX();
                if (Marcus != null && Marcus.activeSelf) { Marcus.GetComponent<Animator>().Play("IdleFrenteMarcus"); }
                if (Luiza != null && Luiza.activeSelf) { Luiza.GetComponent<Animator>().Play("IdleFrenteLuiza"); }
                break;
            case "Base Layer.IdleCostas":
                posicionanoX();
                if (Marcus != null && Marcus.activeSelf) { Marcus.GetComponent<Animator>().Play("IdleCostasMarcus"); }
                if (Luiza != null && Luiza.activeSelf) { Luiza.GetComponent<Animator>().Play("IdleCostasLuiza"); }
                break;
            case "Base Layer.IdleDireita":
                posicionanoY();
                if (Marcus != null && Marcus.activeSelf) { Marcus.GetComponent<Animator>().Play("IdleDireita"); }
                if (Luiza != null && Luiza.activeSelf) { Luiza.GetComponent<Animator>().Play("IdleDireitaLuiza"); }
                break;
            case "Base Layer.IdleEsquerda":
                posicionanoY();
                if (Marcus != null && Marcus.activeSelf) { Marcus.GetComponent<Animator>().Play("IdleEsquerda"); }
                if (Luiza != null && Luiza.activeSelf) { Luiza.GetComponent<Animator>().Play("IdleEsquerdaLuiza"); }
                break;
            case "IdleFrente":
                posicionanoX();
                if (Marcus != null && Marcus.activeSelf) { Marcus.GetComponent<Animator>().Play("IdleFrenteMarcus"); }
                if (Luiza != null && Luiza.activeSelf) { Luiza.GetComponent<Animator>().Play("IdleFrenteLuiza"); }
                break;
            case "IdleCostas":
                posicionanoX();
                if (Marcus != null && Marcus.activeSelf) { Marcus.GetComponent<Animator>().Play("IdleCostasMarcus"); }
                if (Luiza != null && Luiza.activeSelf) { Luiza.GetComponent<Animator>().Play("IdleCostasLuiza"); }
                break;
            case "IdleDireita":
                posicionanoY();
                if (Marcus != null && Marcus.activeSelf) { Marcus.GetComponent<Animator>().Play("IdleDireita"); }
                if (Luiza != null && Luiza.activeSelf) { Luiza.GetComponent<Animator>().Play("IdleDireitaLuiza"); }
                break;
            case "IdleEsquerda":
                posicionanoY();
                if (Marcus != null && Marcus.activeSelf) { Marcus.GetComponent<Animator>().Play("IdleEsquerda"); }
                if (Luiza != null && Luiza.activeSelf) { Luiza.GetComponent<Animator>().Play("IdleEsquerdaLuiza"); }
                break;
        }
    }
        void posicionanoX()
        {
        if (PlayerStatus.MarcusAtivo && ManagerGame.Instance.Regiao.PodeAmigo)
            {
                Marcus.transform.position = new Vector3(Neftari[NeftariTemporario].transform.position.x - 2,
                    Neftari[NeftariTemporario].transform.position.y, Neftari[NeftariTemporario].transform.position.z);
                Marcus.SetActive(true);
            }
            if (PlayerStatus.LuizaAtiva && ManagerGame.Instance.Regiao.PodeAmigo)
            {
            Luiza.transform.position = new Vector3(Neftari[NeftariTemporario].transform.position.x + 2,
                    Neftari[NeftariTemporario].transform.position.y, Neftari[NeftariTemporario].transform.position.z);
                Luiza.SetActive(true);
            }
        }
        void posicionanoY()
        {
        if (PlayerStatus.MarcusAtivo && ManagerGame.Instance.Regiao.PodeAmigo)
            {
                Marcus.transform.position = new Vector3(Neftari[NeftariTemporario].transform.position.x,
                     Neftari[NeftariTemporario].transform.position.y + 2, Neftari[NeftariTemporario].transform.position.z);
                Marcus.SetActive(true);
            }
            if (PlayerStatus.LuizaAtiva && ManagerGame.Instance.Regiao.PodeAmigo)
            {
            Debug.Log("1");
                Luiza.transform.position = new Vector3(Neftari[NeftariTemporario].transform.position.x,
                   PlayerStatus.NextHeroPosition.y -2, Neftari[NeftariTemporario].transform.position.z);
                Luiza.SetActive(true);
            }
        }       
    
}
