﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionPlacer : MonoBehaviour
{
    public float DistanciaDeCriancao;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DistanciaDeCriancao);
    }
}

