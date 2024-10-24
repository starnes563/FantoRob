using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lapis : MonoBehaviour
{
    public List<GameObject> Pencil = new List<GameObject>();
    public static bool Vermelho = true;
    public static bool Verde = true;
    public static bool Amarelo = true;
    public static bool Azul = true;
    public static bool Rosa = true;
    public static bool Laranja = true;
    // Start is called before the first frame update
    void Start()
    {
        if (Vermelho) { Pencil[0].gameObject.SetActive(true); }
        if (Verde) { Pencil[1].gameObject.SetActive(true); }
        if (Amarelo) { Pencil[2].gameObject.SetActive(true); }
        if (Azul) { Pencil[3].gameObject.SetActive(true); }
        if (Rosa) { Pencil[4].gameObject.SetActive(true); }
        if (Laranja) { Pencil[5].gameObject.SetActive(true); }
    }
    public static void RetirarLapis(string lapis)
    {
        switch(lapis)
        {
            case "vermelho":
                Vermelho = false;
                break;
            case "verde":
                Verde = false;
                break;
            case "amarelo":
                Amarelo = false;
                break;
            case "azul":
                Azul = false;
                break;
            case "rosa":
                Rosa = false;
                break;
            case "laranja":
                Laranja = false;
                break;
        }
    }

    
}
