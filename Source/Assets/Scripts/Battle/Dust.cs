﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    // Start is called before the first frame update
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
