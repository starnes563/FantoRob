using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cenoura : MonoBehaviour
{
    public Rigidbody2D Rb2d;
    float contador;
    // Start is called before the first frame update
    void Start()
    {
        Rb2d.AddForce(transform.right* transform.localScale.x * 7f, ForceMode2D.Impulse);
    }
    private void Update()
    {
        contador += Time.deltaTime;
        if (contador >= 5) { Destroy(this.gameObject); }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Slime")
        {
            collision.GetComponent<EnemySlimeController>().TomarDano(true);
            Destroy(this.gameObject);
        }
        if (collision.tag == "Morcego")
        {
            collision.GetComponent<EnemyBatController>().TomarDano(true);
            Destroy(this.gameObject);
        }
       
    }

}
