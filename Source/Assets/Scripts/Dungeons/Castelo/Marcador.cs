using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marcador : MonoBehaviour
{
    public int ID;
    public Sprite SpriteAzul;
    // Start is called before the first frame update
    void Start()
    {
        if(StoryEvents.CavaeleiroCast[ID])
        {
            GetComponent<SpriteRenderer>().sprite = SpriteAzul;
        }
    }

   
}
