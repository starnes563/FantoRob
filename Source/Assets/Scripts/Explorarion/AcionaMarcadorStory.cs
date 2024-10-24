using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcionaMarcadorStory : MonoBehaviour
{
    public int ID;
    public void Acionar()
    {
        StoryEvents.MarcadoresDesafio[ID] = true;
    }
}
