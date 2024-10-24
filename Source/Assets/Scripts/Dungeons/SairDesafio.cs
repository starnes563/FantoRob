using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SairDesafio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // this.transform.position = new Vector3(this.transform.position.x, -7.23f);
       // LeanTween.moveLocalY(this.gameObject, 5.61f, 0.4f);
    }
    public void Clicou()
    {
        PlayerStatus.Trending = 0;
        ManagerGame.Instance.Regiao.IrParaEntrada();
    }
}
