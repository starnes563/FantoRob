using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnforgeButton : MonoBehaviour
{
    private UnMerger unmerger;

    // Start is called before the first frame update
    void Start()
    {
        unmerger = GameObject.FindWithTag("Gerenciador").GetComponent<UnMerger>();
    }

    // Update is called once per frame

    public void Clicar()
    {
        unmerger.Separar();
    }

}
