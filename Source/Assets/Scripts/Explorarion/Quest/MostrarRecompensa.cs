using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostrarRecompensa : MonoBehaviour
{
    public Image Sprite;
    public Text Nome;
    public Text Quantidade;
   public void Mostrar(Recompensa r)
    {
        Quantidade.text = r.quantidade.ToString();
        switch (r.MeuTipo)
        {
            case Recompensa.TipodeRecompensa.ITEMCONSTRUIR:
               Sprite.sprite = Constructor.RetornarSprite(6, 0, 0, r.Propriedade, 0);
               Nome.text = Constructor.RetornarNome(6,0,0,0, r.Propriedade,0);
                break;
            case Recompensa.TipodeRecompensa.PENTEVAZIO:
                Sprite.sprite = Constructor.RetornarSprite(4, 0, 0, 0, 0);
                Nome.text = Constructor.RetornarNome(1, 0, 0, 0, 0, 0);
                break;
            case Recompensa.TipodeRecompensa.PENTECHEIO:
                Sprite.sprite = Constructor.RetornarSprite(1, 0, 0, 0, 0);
                Nome.text = Constructor.RetornarNome(1, 0, 0, 0, 0, 0);
                break;
            case Recompensa.TipodeRecompensa.CIRCUITO:
                Sprite.sprite = Constructor.RetornarSprite(5, 0, r.Propriedade, 0, 0);
                Nome.text = Constructor.RetornarNome(5,0,0,r.Propriedade,0,0);
                break;
            case Recompensa.TipodeRecompensa.SILICIO:
                Sprite.sprite = Constructor.RetornarSprite(0, 0, 0, r.Propriedade, 0);
                Nome.text = Constructor.RetornarNome(0, 0, 0, 0, r.Propriedade, 0);
                break;
            case Recompensa.TipodeRecompensa.PARTEROBO:
                Sprite.sprite = Constructor.RetornarSprite(6, 0, 0, r.Propriedade, 0);
                Nome.text = Constructor.RetornarNome(6, 0, 0, 0, r.Propriedade, 0);
                break;
        }
        this.gameObject.SetActive(true);
    }

}
