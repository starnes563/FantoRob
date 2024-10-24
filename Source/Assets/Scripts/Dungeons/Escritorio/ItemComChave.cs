using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComChave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
    }

}
