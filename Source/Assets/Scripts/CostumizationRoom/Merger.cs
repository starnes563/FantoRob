using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merger : MonoBehaviour
{    
    public Image[] Spacers = new Image[2];
    public Button BotaodeEscolha;
    public Button BotaoCircuito;
    public Button BotaodeVerificacao;
    public Button BotaoDeResultado; 
    [HideInInspector]
    public bool escolheuPente = false;
    public Circuit Circuitobase;   
    //textos do menu
    public Text Solda;
    public Text Gasto;   
    public Text[] Valor = new Text[6];
    public Text Nomedomove;
    //pente escolhido
    [HideInInspector]
    public Pente PenteNaMesa;
    int indexpentenamesa =9999999;
    //circuitos colocados;
    public int[] Slots= new int [4];
    public GameObject GOPente;
    int slotTemporario=99;
    public AudioSource source;
    public AudioClip Confirma;
    public AudioClip Desiste;
    public AudioClip NPode;
    [HideInInspector]
    public bool animando = false;
    public List<SlotButton> BotoesDeSlot;
    List<GameObject> BotoesCircuitos = new List<GameObject>();
    List<GameObject> BotoesPentes= new List<GameObject>();
    // Update is called once per frame
    public void OnEnable()
    {
        Criar();
    }
    public void Criar()
    {     
        for(int i = 0; i<4; i++)
        {
            Slots[i] = 99;
        }
        //botões dos pentes
        if(BotoesPentes.Count>0 && BotoesPentes != null)
        {
            foreach( GameObject g in BotoesPentes)
            {
                Destroy(g);
            }
            BotoesPentes.Clear();
        }
        if(PlayerObjects.PentesVazios.Count>0)
        {
            foreach (Pente pente in PlayerObjects.PentesVazios)
            {               

                    Button botao = Instantiate(BotaodeEscolha) as Button;
                BotoesPentes.Add(botao.gameObject);
                    botao.transform.SetParent(Spacers[0].transform, false);
                    botao.transform.GetChild(1).GetComponent<Text>().text = pente.Gasto1.ToString() + "/" + pente.Gasto2.ToString();
                if (pente.Move.Aleatório)
                {
                    botao.transform.GetChild(2).GetComponent<Text>().text = pente.Move.Nome;
                }
                else
                {
                    botao.transform.GetChild(2).GetComponent<Text>().text = pente.Move.NamesLang[ManagerGame.Instance.Idm];
                }
               // botao.transform.GetChild(2).GetComponent<Text>().text = pente.Move.Nome;
                //criar o botao
                ChooseButton bt = botao.GetComponent<ChooseButton>();
                bt.merger = this;
                bt.MyComb = pente;
                bt.meutipo = 0;
                bt.Index = PlayerObjects.PentesVazios.IndexOf(pente);
            }
        }        
        //botões dos circuito
        if(BotoesCircuitos.Count>0 && BotoesCircuitos != null)
        {
            foreach(GameObject g in BotoesCircuitos)
            {
                Destroy(g);
            }
            BotoesCircuitos.Clear();
        }
        for (int i = 0; i < 15; i++)
        {
            if (PlayerObjects.Circuits[i] > 0)
            {
                Button botao = Instantiate(BotaoCircuito) as Button;
                BotoesCircuitos.Add(botao.gameObject);
                botao.transform.SetParent(Spacers[1].transform, false);               
                botao.transform.GetChild(0).GetComponent<Image>().sprite = Constructor.RetornarSprite(5, 0, i, 0, 0);
                botao.transform.GetChild(1).GetComponent<Text>().text = Constructor.RetornarNome(5, 0, 0, i, 0, 0);
                botao.transform.GetChild(2).GetComponent<Text>().text = PlayerObjects.Circuits[i].ToString();
                //criarbotao
                ChooseButton bt = botao.GetComponent<ChooseButton>();
                bt.merger = this;
                bt.MyCr = PlayerObjects.Circuits[i];
                bt.meutipo = 1;
                bt.Index = i;
            }
        }
       
        Solda.text = PlayerObjects.Silicon.ToString();
    }
    public void EscolherPente(Pente pente, int index)
    {
        if(!animando)
        {
            PenteNaMesa = pente;
            escolheuPente = true;
            if (pente.Move.Aleatório)
            {
                Nomedomove.text = pente.Move.Nome;
            }
            else
            {
                Nomedomove.text = pente.Move.NamesLang[ManagerGame.Instance.Idm];
            }
            //Nomedomove.text = pente.Move.Nome;
            indexpentenamesa = index;
            GOPente.SetActive(true);
        }       
    }
    public void RetirarPente()
    {
        if(!animando)
        {
            for (int i = 0; i < 4; i++)
            {
                if (BotoesDeSlot[i].St == SlotButton.estado.COMCHIP)
                {
                    RetirarSlot(i, BotoesDeSlot[i]);
                }
            }
            if (!escolheuPente)
            {
                TocarSomDesiste();
            }
            else
            {
                PenteNaMesa = null;
                indexpentenamesa = 99999;
                escolheuPente = false;
                for(int i = 0; i<4; i++)
                {
                   Slots[i] = 99;
                }
                for (int i = 0; i < 6; i++)
                {
                    Valor[i].text = "";
                }
                Gasto.text = "";
                Nomedomove.text = "";
                for (int i = 0; i < 6; i++)
                {
                    Valor[i].text = "";
                }
                GOPente.SetActive(false);
                TocarSomDesiste();
            }
        }
    }
    void calcvlpt()
    {
        int[] vl = new int[6];
        //vl[0]=ak
        //vl[1]=as
        //vl[2]=re
        //vl[3]=vl
        //vl[4]=it
        //vl[5]=bf
        if (Slots[0] != 99) 
        {
            int p = Slots[0];
            for (int i = 0; i<4;i++)
            {
                if(Slots[i] !=99 && Slots[i]<15)
                {
                    vl[Circuitobase.RetornarId(Slots[i])] += Circuitobase.RetornarValueint(Slots[i]);             
                }                
            }
            for( int i = 0; i<6;i++)
            {
                Valor[i].text = vl[i].ToString();
            }
            
        }
        if(vl[0]==5)
        {
            Gasto.text = PenteNaMesa.Gasto2.ToString();
        }
        else
        {
            Gasto.text = PenteNaMesa.Gasto1.ToString();
        }
        
       
    }
    public void SlotEscolhido( int index)
    {
        if(!animando)
        {
            slotTemporario = index;
        }
        
    }
    public void PorSlot(int slot, SlotButton sl)
    {
        if (!animando)
        {
            if (slotTemporario != 99)
            {
                if (escolheuPente && PlayerObjects.Circuits[slotTemporario] > 0 && slotTemporario != 99)
                {                   
                    Slots[slot] = slotTemporario;
                    PlayerObjects.Circuits[slotTemporario]--;
                    calcvlpt();
                    slotTemporario = 99;
                    TocarSomConfirma();
                    sl.St = SlotButton.estado.COMCHIP;
                    sl.Ativarimagem();
                }
                else
                {
                    TocarSomDesiste();
                }
            }
        }
       
    }
    public void RetirarSlot(int slot, SlotButton sl)
    {
        if(!animando)
        {
            if (slot == 0)
            {
                PlayerObjects.Circuits[Slots[0]]++;
                sl.DesativarImagem();
                Slots[0] = 99;
                Gasto.text = "";
                for (int i = 0; i < 6; i++)
                {
                    Valor[i].text = "";
                }
            }
            else
            {                
                PlayerObjects.Circuits[Slots[slot]]++;
                sl.DesativarImagem();
                Slots[slot] = 99;
            }
            sl.St = SlotButton.estado.SEMCHIP;
            calcvlpt();
        }
        
    }        
    public void SoldarPente()
    {
        if (PlayerObjects.Silicon > 0)
        {
            animando = false;
            //gastar solda
            PlayerObjects.Silicon--;
            //por os circuitos
            PenteNaMesa.Slot1 = Instantiate(Circuitobase);
            PenteNaMesa.Slot1.IniciarCircuito(Slots[0]);
            PenteNaMesa.Slot2 = Instantiate(Circuitobase);
            PenteNaMesa.Slot2.IniciarCircuito(Slots[1]);
            PenteNaMesa.Slot3 = Instantiate(Circuitobase);
            PenteNaMesa.Slot3.IniciarCircuito(Slots[2]);
            PenteNaMesa.Slot4 = Instantiate(Circuitobase);
            PenteNaMesa.Slot4.IniciarCircuito(Slots[3]);
            //forjar
            PenteNaMesa.Forge();
            //adicionar ao inventario
            PlayerObjects.PentesCheios.Add(Instantiate(PenteNaMesa));
            //Retira o pente vazio do inventario
            PlayerObjects.PentesVazios.RemoveAt(indexpentenamesa);
            PlayerObjects.Circuits[Slots[0]]--;
            PlayerObjects.Circuits[Slots[1]]--;
            PlayerObjects.Circuits[Slots[2]]--;
            PlayerObjects.Circuits[Slots[3]]--;
            //Retira o pente da mesa
            RetirarPente();
            //Refazermenu
            Criar();
        }
        else
        {
            source.PlayOneShot(NPode);
        }
    }
    public void Fechar()
    {
        RetirarPente();
        TocarSomDesiste();
        this.gameObject.SetActive(false);

    }
    public void TocarSomConfirma()
    {
        source.PlayOneShot(Confirma);
    }
    public void TocarSomDesiste()
    {
        source.PlayOneShot(Desiste);
    }
    public void LimparCircuito()
    {
        foreach(GameObject b in BotoesCircuitos)
        {
            b.GetComponent<ChooseButton>().Deselecionar();
        }
    }
 }

