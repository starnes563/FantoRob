using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMansao : MonoBehaviour
{
    public List<GameObject> Teclas = new List<GameObject>(4);
    public static List<GameObject> TeclasStatic = new List<GameObject>(4);
    // Start is called before the first frame update
    void Start()
    {
        TeclasStatic.Clear();
        foreach (GameObject g in Teclas)
        {
            TeclasStatic.Add(g);
        }
        MostrarTeclas();
    }   
    public static void MostrarTeclas()
    {
        for (int i = 0; i < 4; i++)
        {
            TeclasStatic[i].gameObject.SetActive(StoryEvents.TeclasOrgao[i]);
        }
    }
}
