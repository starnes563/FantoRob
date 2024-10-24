using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombSelectionMenu : MonoBehaviour
{
    [HideInInspector]
    public Plug MyPlug;
    public Button BotaodePente;
    public Image Spacer;
    [HideInInspector]
    public GameObject MeuPente;
    private List<GameObject> Botoes = new List<GameObject>();
    
    public void Criar(Plug plug)
    {
        this.transform.position = new Vector3(0.636f, this.transform.position.y);
        LeanTween.moveLocalX(this.gameObject, 0.473f, 0.3f);
        if (Botoes.Count>0)
        {
            foreach (GameObject br in Botoes)
            {
                Destroy(br);
            }
            Botoes = new List<GameObject>();
        }        
        MyPlug = plug;
      foreach (Pente pente in PlayerObjects.PentesCheios)
        {
          Button botao = Instantiate(BotaodePente, Spacer.transform) as Button;
            Botoes.Add(botao.gameObject);
            botao.GetComponent<CombSelectionButton>().Criar(pente, plug, this);                       
        }

        this.gameObject.SetActive(true);
    }
   public void Finalizar()
    {
        this.gameObject.SetActive(false);
    }
}
