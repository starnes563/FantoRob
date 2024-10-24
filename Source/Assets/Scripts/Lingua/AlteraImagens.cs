using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlteraImagens : MonoBehaviour
{
    public List<Sprite> Sprites;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().sprite = Sprites[ManagerGame.Instance.Idm];   
    }
}
