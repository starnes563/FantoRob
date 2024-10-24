using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
       
    private WeaponMethods myWeapon;
    private Status myStatus;
    private Rigidbody2D objeto;
   
    // Start is called before the first frame update
    void OnEnable()
    {        
        myStatus = GetComponent<Status>();
        myWeapon = GetComponent<WeaponMethods>();
        objeto = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    public void AnimacaoOn(string boolName)
    {       
        myWeapon.animacaoexecutando = true;
    }   
    public void LiberaAtaque()
    {
        myWeapon.animacaoexecutando = false;
    }
     public IEnumerator SeMover(Vector2 objetivo, float distancia)
    {
        Vector2 direcao = objetivo - objeto.position;
        direcao.x = objeto.position.x;
        float distanciaAtual = Vector3.Distance(transform.position, objetivo);
        while (distanciaAtual >= distancia)
        {
            objeto.MovePosition(objeto.position + direcao.normalized * myStatus.Velocidade * Time.deltaTime);
            distanciaAtual = Vector3.Distance(transform.position, objetivo);

            yield return null;
        }
      LiberaAtaque();
    } 
    
    
}
