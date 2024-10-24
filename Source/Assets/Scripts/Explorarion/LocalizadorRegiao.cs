using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizadorRegiao : MonoBehaviour
{
    public Text Texto;
    float contador;
    // Start is called before the first frame update
    void Start()
    {
        if(ManagerGame.Instance.Regiao.RegionName[ManagerGame.Instance.Idm] !=null && ManagerGame.Instance.Regiao.RegionName[ManagerGame.Instance.Idm] !="")
        {
            Texto.text = ManagerGame.Instance.Regiao.RegionName[ManagerGame.Instance.Idm];
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
        if(this.gameObject.activeSelf)
        {
            contador += Time.deltaTime;
            if(contador>=3f)
            {
                this.gameObject.SetActive(false);
            }
        }
    }


}
