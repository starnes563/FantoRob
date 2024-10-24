using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;


[CreateAssetMenu(fileName = "Dialogo",menuName = "Textos/Dialogos")]
[System.Serializable]
public class Dialogo : ScriptableObject
{
    public List<string> Keys = new List<string>();
    public List<GameObject> Objetos = new List<GameObject>();
    public Dictionary<string ,GameObject> Sprites = new Dictionary<string ,GameObject>();
    [HideInInspector]
    public List<string> Sentencas;
    [HideInInspector]
    public int ProximaFrase = 0;   
    public List<TextAsset> ArquivosTxt = new List<TextAsset>();    
    public string MinhaProximaFranse()
    {
        string frase = Sentencas[ProximaFrase];
        ProximaFrase++;
        return frase;
    }
    public void LerOTexto(int idioma)
    {
        if (ArquivosTxt != null)
        {
            if (ArquivosTxt.Count > 0)
            {
                Sentencas = ArquivosTxt[ManagerGame.Instance.Idm].text.Split('\n').ToList();
                if (Keys.Count > 0)
                {
                    for (int i = 0; i < Keys.Count; i++)
                    {
                        Sprites.Add(Keys[i], Objetos[i]);
                    }
                }
            }
        }
    }
}
