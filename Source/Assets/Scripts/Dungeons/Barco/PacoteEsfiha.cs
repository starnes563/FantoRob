using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacoteEsfiha : MonoBehaviour
{
    float contador;
    // Start is called before the first frame update
    void OnEnable()
    {
        contador = 0;
    }
    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(contador> 2.5f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
