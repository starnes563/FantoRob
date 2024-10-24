using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoCenas : MonoBehaviour
{
    public Diretor Director;

    public void Clicar(int cena)
    {
        ManagerGame.Instance.SceneToLoad = cena;
        Director.TrocarACena();
    }
}
