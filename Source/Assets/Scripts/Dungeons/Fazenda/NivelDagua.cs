using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NivelDagua : MonoBehaviour
{
    public bool NivelAguaBaixo;
    // Start is called before the first frame update
    void Start()
    {
        if(NivelAguaBaixo!=StoryEvents.NivelAguaBaixo)
        {
            this.gameObject.SetActive(false);
        }
    }


}
