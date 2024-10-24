using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaixaDeSalvamento : MonoBehaviour
{
    public Text Texto;
    enum estado
    {
        SALVANDO,
        SALVO,
    }
    float contador;
    estado MeuEstado;
    public List<string> TextoSalvando;
    public List<string> TextoSalvo;
    public static CaixaDeSalvamento Instancia;
    // Start is called before the first frame update
        void OnEnable()
    {
        contador = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf && MeuEstado == estado.SALVO)
        {
            contador += Time.deltaTime;
            if (contador >= 2f)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
    public void Salvando()
    {
        this.gameObject.SetActive(true);
        MeuEstado = estado.SALVANDO;
        Texto.text = TextoSalvando[ManagerGame.Instance.Idm];
    }
    public void Salvo()
    {
        MeuEstado = estado.SALVO;
        Texto.text = TextoSalvo[ManagerGame.Instance.Idm];
    }
    public void AtivarInstancia()
    {
        Instancia = this;
    }
}
