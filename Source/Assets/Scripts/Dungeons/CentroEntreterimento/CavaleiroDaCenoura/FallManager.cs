using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Slime")
        {
            collision.GetComponent<EnemySlimeController>().TomarDano(false);          
        }
        if (collision.tag == "Morcego")
        {
            collision.GetComponent<EnemyBatController>().TomarDano(false);           
        }
        if (collision.tag == "CoelhoCav")
        {
            collision.GetComponent<PlayerController>().TakeDamage(1);            
        }

    }
}
