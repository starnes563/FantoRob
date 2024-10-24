using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardLevel : MonoBehaviour
{
    public Text Nivel;
    public static UICardLevel Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Atualiza();
    }
    public void Atualiza()
    {
        Nivel.text = StoryEvents.NivelCartao.ToString();
    }
   

}
