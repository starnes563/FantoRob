using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotaoDeAtaque : MonoBehaviour
{
    public int Id;
    public WeaponMethods weaponMethods;
    public float Forca;
    public int Prescisao;
    public float Gasto;
    public bool Elemental;
    public int Elem;
    public int Acoes;
    public AttackManager manager;
    public BotaoRetornar Retornar;
    public List<Text> NomeAtaque;
    public List<Text> Relacao;
    public Image Fundo;
    public void Clicou()
    {        
        SonsDoMenu.Confirmar();
        weaponMethods.UsarAtaque(Id);
        Retornar.atacou = true;
    }
    void OnMouseEnter()
    {       
        manager.Mostrar(Forca, Prescisao, Gasto+ weaponMethods.regitrarAumentoEnergia, Acoes, Elemental, Elem);
    }
    void OnMouseExit()
    {
        manager.Apagar();
    }
    public void Mostrar()
    {
        manager.Mostrar(Forca, Prescisao, Gasto + weaponMethods.regitrarAumentoEnergia, Acoes, Elemental, Elem);
    }
    public void Apagar()
    {
        manager.Apagar();
    }
}
