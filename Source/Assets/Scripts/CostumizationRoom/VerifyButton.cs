using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyButton : MonoBehaviour
{
    [HideInInspector]
    public Type MyThing;
    private Merger merger;
    // Start is called before the first frame update
    void Start()
    {
        merger = GameObject.FindWithTag("Gerenciador").GetComponent<Merger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Devolver()
    {
       // merger.Desistir(MyThing.MyButton.GetComponent<ChooseButton>());
        Destroy(gameObject);
    }
    public void Destruir()
    {
        if(MyThing.Quantidade>0)
        {
            Destroy(gameObject);
        }
        else
        {
           // MyThing.Destruir();
            Destroy(gameObject);
        }
        {

        }
    }
}
