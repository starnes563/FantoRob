using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public GameObject Jogador;
  //  private Vector3 distancia;
    //private bool podeatualizar = false;
    // Start is called before the first frame update   
    private void Start()
    {
        if (Jogador == null)
        {
            Jogador = GameObject.FindWithTag("Player");
        }
        GetComponent<CinemachineVirtualCamera>().Follow = Jogador.transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {        
       // if (Jogador!= null)
        //{            
           // this.transform.position = new Vector3(Jogador.transform.position.x, Jogador.transform.position.y, -10f);

          //  distancia = transform.position - Jogador.transform.position;
          //  podeatualizar = true;
       // }
      //  if(podeatualizar)
      //  {
      //      transform.position = Jogador.transform.position + distancia;
      //  }        
    }
}
