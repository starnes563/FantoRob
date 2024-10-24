using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvisoAtaque : MonoBehaviour
{
    public Animator Anim;
    public AudioSource Source;
    public AudioClip Clip;
    public AudioSource SourceAcertou;
    public AudioClip ClipAcertou;
    public GameObject Ancora;
    private Vector3 distancia;
    private bool podeatualizar = false;
    bool calculadist = true;
    private void Start()
    {
       // Source.PlayOneShot(Clip);
    }
    public void Ativar()
    {
        
        
    }
    public void Desativar()
    {
        Destroy(this.gameObject); 
    }
    public void SomAcertou()
    {
        SourceAcertou.PlayOneShot(ClipAcertou);
    }
    private void Update()
    {
        if (Ancora!= null && calculadist)
        {            
         this.transform.position = new Vector3(Ancora.transform.position.x, Ancora.transform.position.y,0f);

          distancia = transform.position - Ancora.transform.position;
          podeatualizar = true;
            calculadist = false;
         }
          if(podeatualizar)
          {
              transform.position = Ancora.transform.position + distancia;
          }     
    }
}
