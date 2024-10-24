using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInicial : MonoBehaviour
{
    [HideInInspector]
    public Sprite SpriteEscolhido;
    [HideInInspector]
    public bool escolheusprite = false;
    public Text TextoNome;
    public AudioSource source;
    public AudioClip SomConfirma;
    public AudioClip SomDesiste;
    public List<GameObject> ListaConfirma;
    public Diretor Diretor;
    public Button BotaoCarregarSave;
    public Image Spacer;
    List<GameObject> botoes = new List<GameObject>();   
    public void PegarSprite(Image sp)
    {
        escolheusprite = true;
        SpriteEscolhido = sp.sprite;
        source.PlayOneShot(SomConfirma);
    }
    public void Escolher()
    {
      //  Debug.Log(escolheusprite);
        //if(escolheusprite && TextoNome.text !="")
       // {
           // Debug.Log("aqui");
           // ManagerGame.Instance.ReceberInfoJogador(SpriteEscolhido, TextoNome.text);
           // source.PlayOneShot(SomConfirma);
            ManagerGame.Instance.SceneToLoad = 125;
            Diretor.TrocarACena();
       // }
      //  else
       // {
          //  source.PlayOneShot(SomDesiste);
       // }
    }
    public void EsconderTdConf()
    {
        foreach(GameObject gb in ListaConfirma)
        {
            gb.SetActive(false);
        }
        //source.PlayOneShot(SomConfirma);
        escolheusprite = false;
    }
    public void GerarMenuCarregar()
    {
        if(botoes.Count>0)
        {
            foreach(GameObject g in botoes)
            {
                Destroy(g);
            }
        }
        if(ManagerGame.Instance.SavePath.Count>0)
        {
            for (int i = 0; i< ManagerGame.Instance.SavePath.Count; i++)
            {
                DadosJogador j = SaveSystem.ListaSalvo(i);
                Button b = Instantiate(BotaoCarregarSave, Spacer.transform) as Button;
                botoes.Add(b.gameObject);               
                //estrelas
                GameObject st = b.transform.GetChild(2).gameObject;
                for(int e = 0; e<j.Estrelas;e++)
                {
                    st.transform.GetChild(e).gameObject.SetActive(true);
                }
                //dinheiro
                b.transform.GetChild(4).GetComponent<Text>().text = j.Fantodin.ToString();
                //botaoconfirma
                b.transform.GetChild(5).GetComponent<ConfirmarCarregar>().MeuSave = i;
            }
        }
        
    }
}
