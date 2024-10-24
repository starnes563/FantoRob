using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarBau : MonoBehaviour
{
    public static int[] NumeroBauAberto = new int[3];
    public int[] Teclas = new int[2];    
    public int NumeroBaus;
    public List<Bau> MeusBaus = new List<Bau>();
    // Start is called before the first frame update
    void Start()
    {
        
    }
    IEnumerator montarbau()
    {
     
        yield return null;
    }
    void pornobau(int tecla)
    {
        bool feito = false;
        while (!feito)
        {
            int bau = Random.Range(0, MeusBaus.Count);
           // if (!MeusBaus[bau].recebeu)
           // {
             //   MeusBaus[bau].recebeu = true;
               // MeusBaus[bau].Tecla = tecla;
                //feito = true;
            //}
            
        }
    }

}
