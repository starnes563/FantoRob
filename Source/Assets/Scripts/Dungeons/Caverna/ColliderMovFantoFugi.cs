using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderMovFantoFugi : MonoBehaviour
{
    [Range(-1, 1)]
    public int X;
    [Range(-1, 1)]
    public int Y;
    FantoMascaraFugitivo fantoFugitivo;
    public enum Tipo
    {
        ALTERAR,
        PARAR,
    }
    public Tipo MeuTipo;    
    public Walk Player;
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.tag == "FantoFugitivo")
        {            
            fantoFugitivo = collision.gameObject.GetComponent<FantoMascaraFugitivo>();
            switch (MeuTipo)
            {
                case Tipo.PARAR:                   
                    fantoFugitivo.gameObject.SetActive(false);
                    Player.LiberarAndar();
                    break;
                case Tipo.ALTERAR:                  
                    fantoFugitivo.XInicial = X;
                    fantoFugitivo.YInicial = Y;                    
                    break;
            }
        }
    }
}
