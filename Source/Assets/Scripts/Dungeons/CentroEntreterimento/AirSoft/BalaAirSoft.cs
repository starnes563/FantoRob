using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaAirSoft : MonoBehaviour
{
    public Rigidbody2D Rb;
    // Start is called before the first frame update
    void Start()
    {
        Rb.AddForce(transform.up * 40f, ForceMode2D.Impulse);
    }  
}
