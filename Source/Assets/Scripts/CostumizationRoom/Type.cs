using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Tipo", menuName = "Loja/Tipo")]
[System.Serializable]
public class Type : ScriptableObject
{
    //0silicio
    //1circuito
    //2pente
    //3peca
    //4placa
    //5plug
    public int Tipo;
    public Sprite Sprite;
    public string Nome;
    public int Quantidade;
    [HideInInspector]
    public Button MyButton;

    public void Diminuir(int qtd)
    {
        Quantidade -= qtd;
    } 
}
