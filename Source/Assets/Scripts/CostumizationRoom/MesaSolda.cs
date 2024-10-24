using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MesaSolda : MonoBehaviour
{
    public Merger MenuSoldar;
    public Transform Ponta1;
    public Transform Ponta2;
    public GameObject Particula;
    public AudioClip SomBrac;
    public AudioSource Source;
    public void ClicarNaExclamação()
    {
        if(!MenuSoldar.animando && MenuSoldar.escolheuPente && MenuSoldar.Slots[0] !=99 && MenuSoldar.Slots[1
            ] != 99 && MenuSoldar.Slots[2] != 99 && MenuSoldar.Slots[3] != 99)
        {
        
        this.GetComponent<Animator>().SetTrigger("Bracos");
            MenuSoldar.animando = true;
    }
        else
        {
            MenuSoldar.TocarSomDesiste();
        }
        
    }
    public void Soldar()
    {      
        Instantiate(Particula, Ponta1);
        Instantiate(Particula, Ponta2);
    }
    public void SomBraco()
    {
        Source.PlayOneShot(SomBrac);
    }
    public void Terminar()
    {       
        MenuSoldar.SoldarPente();
        MenuSoldar.animando = false;
    }
}
