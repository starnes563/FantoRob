using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustumizationObject : MonoBehaviour
{
    public GameObject Ativar;
    public List<GameObject> Desativar = new List<GameObject>();
    private bool dentro = false;
    private MenuManager menuManager;
    public FantoBerco Berco;
    Walk Player;
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && dentro)
        {
            Diretor.DesativarMenuPlayer();
            if(Berco != null)
            {
                 if(!Berco.quebrado)
                {
                    Berco.Iniciar();
                }                
            }
            else
            {
                Ativar.gameObject.SetActive(true);
                foreach (GameObject g in Desativar)
                {
                    g.SetActive(false);
                }
            }
            Player.CanIWalk = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {        
       if (other.tag == "Player")
       {
            dentro = true;
            Player = other.GetComponent<Walk>();
        }        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        dentro = false;       
    }    
}
