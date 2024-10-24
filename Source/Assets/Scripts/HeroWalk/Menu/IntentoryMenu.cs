using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntentoryMenu : MonoBehaviour
{
    public StuffBillboard StuffBillboard;
    public Button BotaoInventario;
    public Image Spacer;
    public PlayerMenu PlMenu;    
    public bool Pentes;
    public bool Circuitos;
    public bool Silicio;
    public bool Partes;
    public bool Bateria;
    public bool Fisico;   
    public bool ItensConst;
    public float VelocidadeMenu;
    List<GameObject> botoes = new List<GameObject>();
    public void OnEnable()
    {
        Criar();
        this.transform.position = new Vector3(this.transform.position.x, -6.15f,30f);
        LeanTween.moveLocalY(this.gameObject, 0.36f, VelocidadeMenu);
    }
    public void Criar()
    {
        if(botoes.Count>0)
        {
            foreach(GameObject b in botoes)
            {
                Destroy(b);
            }
            botoes.Clear();
        }
        if(Pentes)
        {
            if(PlayerObjects.PentesCheios.Count>0)
            {
                foreach (Pente p in PlayerObjects.PentesCheios)
                {
                    Button botao = Instantiate(BotaoInventario, Spacer.transform) as Button;
                    botoes.Add(botao.gameObject);
                    botao.GetComponent<InventorySelectButton>().Criar(new ItemInventario(ItemInventario.TipoDeInventario.PENTECHEIO, null, p, 0, 0, null, 0, null, 0), StuffBillboard);
                }
            }
           if(PlayerObjects.PentesVazios.Count>0)
            {
                foreach (Pente p in PlayerObjects.PentesVazios)
                {
                    Button botao = Instantiate(BotaoInventario, Spacer.transform) as Button;
                    botoes.Add(botao.gameObject);
                    botao.GetComponent<InventorySelectButton>().Criar(new ItemInventario(ItemInventario.TipoDeInventario.PENTEVAZIO, p, null, 0, 0, null, 0, null, 0), StuffBillboard);
                }
            }           
        }
        if(Circuitos)
        {
           for(int i =0; i<15;i++)
            {
                if(PlayerObjects.Circuits[i]>0)
                {
                    Button botao = Instantiate(BotaoInventario, Spacer.transform) as Button;
                    botoes.Add(botao.gameObject);
                    botao.GetComponent<InventorySelectButton>().Criar(new ItemInventario(ItemInventario.TipoDeInventario.CIRCUITO, 
                        null, null, i, 0, null, 0, null, 0), StuffBillboard);
                }               
            }
        }      
        if(Silicio && PlayerObjects.Silicon>0)
        {
            Button botao = Instantiate(BotaoInventario, Spacer.transform) as Button;
            botoes.Add(botao.gameObject);
            botao.GetComponent<InventorySelectButton>().Criar(new ItemInventario(ItemInventario.TipoDeInventario.SILICIO, 
                null, null, PlayerObjects.Silicon, 0, null, 0, null, 0), StuffBillboard);
        }
        if(Partes && PlayerObjects.RobotParts.Count>0)
        {
            foreach( RobotPart p in PlayerObjects.RobotParts)
            {
                Button botao = Instantiate(BotaoInventario, Spacer.transform) as Button;
                botoes.Add(botao.gameObject);
                botao.GetComponent<InventorySelectButton>().Criar(new ItemInventario(ItemInventario.TipoDeInventario.PARTE,
                    null, null, 0, 0, p, 0, null, 0), StuffBillboard);
            }           
        }       
        if(Bateria && PlayerObjects.PlayerObjectsStatic.Batteries.Count>0)
        {           
            for (int b = 0; b< PlayerObjects.PlayerObjectsStatic.Batteries.Count;b++)
            {                
                if (PlayerObjects.PlayerObjectsStatic.Batteries[b]>0)
                {
                    Button botao = Instantiate(BotaoInventario, Spacer.transform) as Button;
                    botoes.Add(botao.gameObject);
                    botao.GetComponent<InventorySelectButton>().Criar(new ItemInventario(ItemInventario.TipoDeInventario.BATERIA,
                        null, null, 0, 0, null, b, null, 0), StuffBillboard);
                }
            }
        }
        if(Fisico && PlayerObjects.NucleosFisicos.Count>0)
        {
            foreach( Weapon w in PlayerObjects.NucleosFisicos)
            {
                Button botao = Instantiate(BotaoInventario, Spacer.transform) as Button;
                botoes.Add(botao.gameObject);
                botao.GetComponent<InventorySelectButton>().Criar(new ItemInventario(ItemInventario.TipoDeInventario.NFISICO,
                    null, null, 0, 0, null, 0, w, 0), StuffBillboard);
            }
        }
        if(ItensConst)
        {
            for(int i =0; i< PlayerObjects.ItensConstruir.Count; i++)
            {
                if(PlayerObjects.ItensConstruir[i]>0)
                {
                    Button botao = Instantiate(BotaoInventario, Spacer.transform) as Button;
                    botoes.Add(botao.gameObject);
                    botao.GetComponent<InventorySelectButton>().Criar(new ItemInventario(ItemInventario.TipoDeInventario.ITEMCONSTRUIR,
                        null, null, 0, 0, null, 0, null, i), StuffBillboard);
                }
            }
        }
    }    
   
}
