using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Texto : MonoBehaviour
{
    public List<string> Textos;
    // Start is called before the first frame update
    void Start()
    {
        AlterarTexto();
    }
    public void AlterarTexto()
    {
        this.GetComponent<Text>().text = Textos[ManagerGame.Instance.Idm];
    }
 
}
