using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDesafio : MonoBehaviour
{    
    public GameObject ItemDesafio;
    public static GameObject ItemDesafioStatic;
    public GameObject ChaveGrande;
    public static GameObject ChaveGrandeStatic;
    public List<GameObject> ChavePequena = new List<GameObject>();
    public static List<GameObject> ChavePequenaStatic = new List<GameObject>();
    public int IdDesafio;
    public static int IdDesafioStatic;
    // Start is called before the first frame update
    void Start()
    {
        ItemDesafioStatic = ItemDesafio;
        ChaveGrandeStatic = ChaveGrande;
        ChavePequenaStatic = new List<GameObject>();
        ChavePequenaStatic.Clear();
        foreach(GameObject g in ChavePequena)
        {
            ChavePequenaStatic.Add(g);
        }
        IdDesafioStatic = IdDesafio;
        Atualizar();
    }
    public static void Atualizar()
    {
        if (ItemDesafioStatic != null)
        {
            ItemDesafioStatic.SetActive(false);
            ChaveGrandeStatic.SetActive(false);
            foreach (GameObject g in ChavePequenaStatic)
            {
                g.SetActive(false);
            }
            if (StoryEvents.DesafiosCamp[IdDesafioStatic].Itemdesafio)
            {
                ItemDesafioStatic.SetActive(true);
            }
            if (StoryEvents.DesafiosCamp[IdDesafioStatic].Chavegrande)
            {
                ChaveGrandeStatic.SetActive(true);
            }
            for (int i = 0; i < StoryEvents.DesafiosCamp[IdDesafioStatic].Chavepequena; i++)
            {
                ChavePequenaStatic[i].SetActive(true);
            }
        }
    }
    
}
