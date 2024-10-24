using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontoGPS : MonoBehaviour
{
    public int DE = 0;
    public int ATE = 0;
    public float DistanciaDeCriancao;
    public bool PrimeiraMissao;
    // Start is called before the first frame update
    void Start()
    {
        if (!PrimeiraMissao)
        {
            if (PlayerStatus.ControleDeCena >= DE && PlayerStatus.ControleDeCena < ATE)
            {
                this.gameObject.SetActive(true);
                GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().MinhaSetaGPS.Ativar(this.transform);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            if (PlayerObjects.Missões != null && PlayerObjects.Missões.Count > 0)
            {
                if (PlayerObjects.Missões[0].Completo)
                {
                    if (PlayerStatus.ControleDeCena >= DE && PlayerStatus.ControleDeCena < ATE)
                    {
                        this.gameObject.SetActive(true);
                        GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().MinhaSetaGPS.Ativar(this.transform);
                    }
                    else
                    {
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DistanciaDeCriancao);
    }
    public void Ativar()
    {
        this.gameObject.SetActive(true);
        GameObject.FindWithTag("MainCamera").GetComponent<Diretor>().MinhaSetaGPS.Ativar(this.transform);
    }
}
