using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCombButton : MonoBehaviour
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
    public void Clicar()
    {
        merger.SoldarPente();
    }
}
