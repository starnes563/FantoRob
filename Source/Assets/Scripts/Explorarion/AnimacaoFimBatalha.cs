using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoFimBatalha : MonoBehaviour
{
    public void Transitar()
    {
        ManagerGame.Instance.FinalizarBatalha();
    }
    public void Fim()
    {
        ManagerGame.Instance.Transitando = false;
        Destroy(this.gameObject);
    }
}
