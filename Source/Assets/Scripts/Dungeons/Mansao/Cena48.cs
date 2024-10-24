using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cena48 : MonoBehaviour
{
    public void EsqueletoEspecial()
    {
        PlayerObjects.EsqueletoEspecial++;
    }
    public void LiberarUltimaTecla()
    {
        StoryEvents.UltimaTecla = true;
    }
}
