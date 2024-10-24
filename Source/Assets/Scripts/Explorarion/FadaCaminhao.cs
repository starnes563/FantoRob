using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadaCaminhao : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerStatus.DaysLeft > 3) { this.gameObject.SetActive(false); }
    }
}
