using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarcadorDirecao : MonoBehaviour
{
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(StoryEvents.DesafiosCamp[5].Interagiveis[ID]);
    }   
}
