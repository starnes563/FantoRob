using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AleatorizaMesa : MonoBehaviour
{
    public List<Sprite> Mesas = new List<Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Mesas[Random.Range(0, Mesas.Count)];
    }
}
