using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CordaElevadorCast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!StoryEvents.ElevadorCast) { this.gameObject.SetActive(false); }
    }

}
