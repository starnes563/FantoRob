using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcionarSaveSystem : MonoBehaviour
{
    public CaixaDeSalvamento CaixaDeSalvamento;
  public void Salvar()
    {
        SaveSystem.Save();
    }
    public void Carregar()
    {
        //StartCoroutine(ManagerGame.Instance.Load());
        ManagerGame.Instance.SceneToLoad = 0;
        GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();
    }
    private void OnEnable()
    {
        CaixaDeSalvamento.AtivarInstancia();
    }
}
