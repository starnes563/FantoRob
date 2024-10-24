using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SairArena : MonoBehaviour
{
    public int Arena;
    public Vector3 PosicaoArena;
    public Diretor Diretor;
    public void IrParaArena()
    {
        ManagerGame.Instance.SceneToLoad = Arena;
        PlayerStatus.NextHeroPosition = PosicaoArena;
        Diretor.TrocarACena();
    }
}
