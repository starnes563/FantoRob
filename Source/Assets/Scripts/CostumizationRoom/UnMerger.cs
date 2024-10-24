using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UnMerger : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] Circuits;
    [HideInInspector]
    public GameObject[] Silicon;
    [HideInInspector]
    public GameObject Comb;
    [HideInInspector]
    public bool animando = false;
    public Image[] Spacers = new Image[3];
    public Button BotaodeEscolha;
    public Button BotaoDeResultado;
    public Button BotaoResultadoCircuito;
    [HideInInspector]
    public Pente pente;
    public AudioSource source;
    public AudioClip confirma;
    public AudioClip desite;
    List<GameObject> BotoesPenteCheio = new List<GameObject>();
    List<GameObject> BotoesVazio = new List<GameObject>();
    public void OnEnable()
    {
        Criar();
    }
    public void Criar()
    {
        if (BotoesPenteCheio.Count>0)
        {
            foreach(GameObject g in BotoesPenteCheio)
            {
                Destroy(g);
            }
            BotoesPenteCheio.Clear();
        }

      if (PlayerObjects.PentesCheios.Count>0)
        {
           
            foreach (Pente thing in PlayerObjects.PentesCheios)
            {
                    Button botao = Instantiate(BotaodeEscolha) as Button;
                BotoesPenteCheio.Add(botao.gameObject);
                    botao.transform.SetParent(Spacers[0].transform, false);
                    botao.GetComponent<CombButton>().MyThing = thing;
                botao.GetComponent<CombButton>().unmerger = this;
                botao.GetComponent<CombButton>().PassarValores(thing);                   
                    thing.MyButton = botao;               
            }
        }
        pente = null;
    }
    public void Separar()
    {
        //limpando os spacers
        if (BotoesVazio.Count > 0)
        {
            foreach (GameObject g in BotoesVazio)
            {
                Destroy(g);
            }
            BotoesVazio.Clear();
        }
        //botao circuito
        BotaoCircuito(pente.Slot1);
        BotaoCircuito(pente.Slot2);
        BotaoCircuito(pente.Slot3);
        BotaoCircuito(pente.Slot4);

        //pega os circuitos
        adicionacircaoinvent(pente.Slot1);
        adicionacircaoinvent(pente.Slot2);
        adicionacircaoinvent(pente.Slot3);
        adicionacircaoinvent(pente.Slot4);

        //pega pente
        pente.Unforge();
        PlayerObjects.PentesVazios.Add(Instantiate(pente));
        PlayerObjects.PentesCheios.Remove(pente);

        //botão pente
        Button botao = Instantiate(BotaoDeResultado) as Button;
        botao.transform.SetParent(Spacers[1].transform, false);               
        botao.transform.GetChild(1).GetComponent<Text>().text = pente.Gasto1.ToString() + "/" + pente.Gasto2.ToString();
        if (pente.Move.Aleatório)
        {
            botao.transform.GetChild(2).GetComponent<Text>().text = pente.Move.Nome;
        }
        else
        {
            botao.transform.GetChild(2).GetComponent<Text>().text = pente.Move.NamesLang[ManagerGame.Instance.Idm];
        }
        //botao.transform.GetChild(2).GetComponent<Text>().text = pente.Move.Nome;
        BotoesVazio.Add(botao.gameObject);
        

        //destroi botao do pente atual
        Destroy(pente.MyButton.gameObject);

        //retira pente
        Destroy(pente);
        pente = null;
    }
    public void Fechar()
    {
        if(!animando)
        {
            if (BotoesVazio.Count > 0)
            {
                foreach (GameObject g in BotoesVazio)
                {
                    Destroy(g);
                }
                BotoesVazio.Clear();
            }
            pente = null;
            TocarSomDesiste();
            this.gameObject.SetActive(false);
        }        
    }
    public void escolherPente(Pente com)
    {
        if (!animando)
        {
            if (pente != null)
            {
                pente.MyButton.GetComponent<CombButton>().Confima.SetActive(false);
            }
            pente = com;
            pente.MyButton.GetComponent<CombButton>().Confima.SetActive(true);
            TocarSomConfirma();
        }        
    }
    void adicionacircaoinvent(Circuit circuito)
    {       
        PlayerObjects.Circuits[circuito.Arrayindex]++;
    }
    void BotaoCircuito(Circuit circuito)
    {        
        Button botao2 = Instantiate(BotaoResultadoCircuito) as Button;
        botao2.transform.SetParent(Spacers[2].transform, false);       
        botao2.transform.GetChild(0).GetComponent<Image>().sprite = Constructor.RetornarSprite(5,0,circuito.Arrayindex,0,0);
        botao2.transform.GetChild(1).GetComponent<Text>().text = circuito.value.ToString();
        BotoesVazio.Add(botao2.gameObject);
    }
    public void TocarSomConfirma()
    {
        source.PlayOneShot(confirma);
    }
    public void TocarSomDesiste()
    {
        source.PlayOneShot(desite);
    }
}
