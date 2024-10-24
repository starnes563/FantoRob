using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public GameObject Pai;
    public bool ParaAndar = false;
    // Start is called before the first frame update

    void Start()
    {
        if(ParaAndar)
        {
            foreach (GameObject nef in GameObject.FindWithTag("Regiao").GetComponent<Posicionador>().Neftari)
            {
                nef.GetComponent<Walk>().PararDeAndar();
            }
        }       
    }
public void Destroy()
    {
        if(Pai !=null)
        {
            Destroy(Pai);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
