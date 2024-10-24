using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuadroParte : MonoBehaviour
{
    public Text Nome;
    public Text Comp;
    public Text Placa;
    public Text Nivel;

    public void Mostrar(RobotPart parte)
    {
        Nome.text = Constructor.RetornarNome(7, 0, 0, 0, 0, parte.Id);
        Comp.text = parte.Nome;
        Placa.text = parte.Placa.ToString();
        Nivel.text = parte.Nivel.ToString();
        this.gameObject.SetActive(true);
    }
    public void Esconder()
    {
        this.gameObject.SetActive(false);
    }
}
