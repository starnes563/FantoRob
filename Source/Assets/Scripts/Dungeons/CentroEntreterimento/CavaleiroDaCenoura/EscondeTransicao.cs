using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscondeTransicao : MonoBehaviour
{
    public float TempoTransic;
    float contador;
    public Walk Player;
    bool liberarAndar = false;
    public GameObject UI;
    // Start is called before the first frame update
    void OnEnable()
    {
        contador = 0;   
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador>=TempoTransic)
        {
            UI.SetActive(false);
            if(liberarAndar) { Player.LiberarAndar();liberarAndar = false; UI.SetActive(true); }
            this.gameObject.SetActive(false);
        }
    }
    public void LiberarPlayer()
    {
        liberarAndar = true;
    }
}
