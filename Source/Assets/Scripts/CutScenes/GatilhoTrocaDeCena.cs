using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoTrocaDeCena : MonoBehaviour
{
    public int MinhaCena;
    public List<GameObject> ListaDePersonagensCut;
    public Vector3 NextPosition;
    public bool PrecisaDaProximaPosicao = false;
    public void PassarMinhaCena()
    {
        ManagerGame.Instance.SceneToLoad = MinhaCena;
        if (PrecisaDaProximaPosicao) { PlayerStatus.NextHeroPosition = NextPosition; }
    }
    public void TrocarACena()
    {
        GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().TrocarACena();
    }
    public void TrocarOGameObject(string posicao)
    {
        ManagerGame.Instance.FinalizarCutsceneAtual(posicao,ListaDePersonagensCut[0].transform.position);
    }
}
