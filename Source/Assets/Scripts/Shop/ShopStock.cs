using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemLoja", menuName = "Loja/ItemLoja")]
[System.Serializable]

public class ShopStock : ScriptableObject
{
	public Sprite Sprite;
	public int Preco;
	public int Credito;
	public FantoRob Fantorob;
	public Weapon Weapon;
	public int Item;
	public int Batterie;	
	public Pente PenteVazio;	
	public int Circuito;	
	public int ItemConstruir;
	public RobotPart ParteRobo;
	Move mv;
	//0-fantorob
	//1-nucleo fisico
	//2-Item
	//3-bateria
	//4-pentesvazios
	//5-circuitos
	//6-itemconstruir
	//7-ParteRobo;
	//8-solda
	//9-expasao inventario
	public int Tipo;
	public int Estrela;
	public bool[] Bag = new bool [2];
	public bool Compravel = true;
	public void Comprar(int quantidade, bool darmissao)
	{
		switch (Tipo)
		{
			case 0:
				//ativar o objeto
				Fantorob.IniciarRobo(false);
				//adicionar ao inventario
				if(!darmissao)
                {
					PlayerObjects.RobotsNotInUse.Add(Instantiate(Fantorob));
				}
				else
                {
					PlayerObjects.RobotsInUse.Add(Instantiate(Fantorob));
				}
				
				break;
			case 1:
				//ativar o objeto
				Compravel = false;
				for(int i = 0; i<quantidade; i++)
                {
					Weapon.IniciarArma(false);
					PlayerObjects.NucleosFisicos.Add(Instantiate(Weapon));
				}				
				break;
				case 2:
				PlayerObjects.PlayerObjectsStatic.Itens[Item].GetComponent<Item>().Quantidade += quantidade;
				break;
			case 3:
				PlayerObjects.PlayerObjectsStatic.Batteries[Batterie] += quantidade;
				break;
			case 4:
				Compravel = false;
				for (int i = 0; i < quantidade; i++)
				{
					PenteVazio.CriarPente(PenteVazio.Level, PenteVazio.MeuTipo, PenteVazio.Move);
					PlayerObjects.PentesVazios.Add(Instantiate(PenteVazio));
				}
				break;
			case 5:
				PlayerObjects.Circuits[Circuito] += quantidade;
				break;
			case 6:
				PlayerObjects.ItensConstruir[ItemConstruir] += quantidade;
				break;
			case 7:
				Compravel = false;
				PlayerObjects.RobotParts.Add(ParteRobo);
				break;
			case 8:
				PlayerObjects.Silicon += quantidade;
				break;
			case 9:
				Compravel = false;
				PlayerObjects.InventarioMax += 5;
				for (int i = 0; i < 2; i++)
				{
					StoryEvents.ExpasoesInventario[i] = Bag[i];
				}
				break;
		}
		//poe objetos do inventario
		//if(PlayerObjects.Fantodin<= Preco * quantidade && Preco>0)
        //{
			PlayerObjects.Fantodin -= Preco * quantidade;
		//}
		//else if(PlayerObjects.Creditos<= Credito* quantidade && Credito>0)
		//{
		//PlayerObjects.Creditos -= Credito * quantidade;
		//		
		ManagerShop.Instance.AtualizarInvent();
	}
	public Sprite RertonarSprite()
	{
		int id = 0;
		if(ParteRobo != null)
        {
			id = ParteRobo.Id;
        }		
		Sprite = Constructor.RetornarSprite(Tipo,Item,Circuito,ItemConstruir,id);
		switch (Tipo)
		{
			case 0:
				Sprite = Fantorob.MenuIconeFantorob;
				break;
			case 1:
				Sprite = Weapon.MySprite;
				break;
		}
		if (Tipo == 8) { Sprite = Constructor.RetornarSprite(0, Item, Circuito, ItemConstruir, id);
		}
		return Sprite;
	}
	public string RetornarNome()
	{
		int id = 0;
		if (ParteRobo != null)
		{
			id = ParteRobo.Id;
		}
		string nm = Constructor.RetornarNome(Tipo, Item, Batterie, Circuito,ItemConstruir, id);
		switch (Tipo)
		{
			case 0:
				nm = Fantorob.Nome;
				break;
			case 1:
				nm = Weapon.Nome[ManagerGame.Instance.Idm];
				break;
			case 4:
				switch (ManagerGame.Instance.Idm)
				{
					case 0:
						nm = "Pente Vazio";
						break;
					case 1:
						nm = "Empty";
						break;
				}
				break;
		}
		if (Tipo == 8)
		{			
			nm = Constructor.RetornarNome(0, Item, Batterie, Circuito, ItemConstruir, id);
		}
		return nm;
	}

	public void AdicionarMove()
    {
		int nivel = 1;
		switch(PlayerStatus.Estrelas)
        {
			case 2:
				nivel = 2;
				break;
			case 4:
				nivel = 3;
				break;
			case 6:
				nivel = 4;
				break;
        }
		mv = Constructor.MoveConstructor(nivel);
		PenteVazio.Move = Instantiate(mv);
    }
	public bool PossoComprar(int quantidade)
	{
		//verifica se o jogador tem dinheiro
		bool posso = false;		
		//if (PlayerObjects.Creditos >= Credito * quantidade)
        //{
			//posso = true;
       // }
		if(PlayerObjects.Fantodin >= Preco * quantidade)
        {
			posso = true;
        }
		if(Tipo == 2)
        {
			int invent = 0;
			foreach (GameObject item in PlayerObjects.PlayerObjectsStatic.Itens)
			{
				invent += item.GetComponent<Item>().Quantidade;
			}
			if (invent >= PlayerObjects.InventarioMax) { posso = false; }
		}		
		return posso;
	}
}
