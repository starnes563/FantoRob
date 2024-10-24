using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvisoDias : MonoBehaviour
{
    public Text Texto;
    float contador;
    public List<string> Complemento = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerStatus.CartaEndosso)
        {
            Texto.text = PlayerStatus.DaysLeft.ToString() + " " + Complemento[ManagerGame.Instance.Idm] ;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        contador = 0f;
    }
    private void Update()
    {
        if (this.gameObject.activeSelf)
        {
            contador += Time.deltaTime;
            if (contador >= 3f)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
