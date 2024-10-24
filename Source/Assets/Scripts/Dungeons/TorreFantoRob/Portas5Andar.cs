using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portas5Andar : MonoBehaviour
{
    public List<Animator> Um = new List<Animator>();
    public List<Animator> Dois = new List<Animator>();
    public List<Animator> Tres = new List<Animator>();
    // Start is called before the first frame update
    public void Apertarbotao(int num)
    {
        switch(num)
        {
            case 1:
                abrirUm();
                fecharTres();
                break;
            case 2:
                abrirDois();
                fecharUm();
                break;
            case 3:
                abrirTres();
                fecharDois();
                break;
        }
    }
    void abrirUm()
    {
        foreach(Animator anim in Um)
        {
            anim.ResetTrigger("Fechar");
            anim.SetTrigger("Abrir");
        }
    }
    void fecharUm()
    {
        foreach (Animator anim in Um)
        {
            anim.ResetTrigger("Abrir");
            anim.SetTrigger("Fechar");
        }
    }
    void abrirDois()
    {
        foreach (Animator anim in Dois)
        {
            anim.ResetTrigger("Fechar");
            anim.SetTrigger("Abrir");
        }
    }
    void fecharDois()
    {
        foreach (Animator anim in Dois)
        {
            anim.ResetTrigger("Abrir");
            anim.SetTrigger("Fechar");
        }
    }
    void abrirTres()
    {
        foreach (Animator anim in Tres)
        {
            anim.ResetTrigger("Fechar");
            anim.SetTrigger("Abrir");
        }
    }
    void fecharTres()
    {
        foreach (Animator anim in Tres)
        {
            anim.ResetTrigger("Abrir");
            anim.SetTrigger("Fechar");
        }
    }
}
