using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterarIdioma : MonoBehaviour
{
    public List<Texto> Textos;
    public void AltIdm(int idm)
    {       
        ManagerGame.Instance.Idm = idm;
        SaveLanguange(idm);
        alterarMenuInicial();
    }
    public void alterarMenuInicial()
    {
        foreach(Texto tx in Textos)
        {
            tx.AlterarTexto();
        }
    }
   public void SaveLanguange(int idioma)
    {
        //0 = não escolhido, 1 = escolhido
        PlayerPrefs.SetInt("Escolhido", 1);
        PlayerPrefs.SetInt("Idm", idioma);
    }
}
