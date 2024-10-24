using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SairTorre : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (StoryEvents.NivelCartao < 5&& StoryEvents.NivelCartao>0) { StoryEvents.NivelCartao = 0; }
    }

}
