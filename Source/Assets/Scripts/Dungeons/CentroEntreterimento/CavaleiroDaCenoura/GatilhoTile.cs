using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoTile : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.tag == "TileCoelho")
        {            
            collision.GetComponent<GroundTile>().Avisar();
            Destroy(collision.gameObject);
        }        
    }
}
