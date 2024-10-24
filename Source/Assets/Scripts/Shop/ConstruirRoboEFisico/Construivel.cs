using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Construivel", menuName = "Loja/Construivel")]
[System.Serializable]
public class Construivel : ScriptableObject
{
	[HideInInspector]
	public Sprite SpriteVazio;
	public int Preco;
	public FantoRob Fantorob;
	public Weapon Weapon;
	//0-fantorob
	//1-nucleo fisico
	public int Tipo;
	public List<int> ObjetosNecessarios = new List<int>();
	public List<int> QuantidadesNecessarias = new List<int>();
	public bool EsqueletoEspecial;
	public void Construir()
	{
			switch (Tipo)
			{
				case 0:
				if(!EsqueletoEspecial || EsqueletoEspecial && PlayerObjects.EsqueletoEspecial> 0)
                {
					//ativar o objeto
					Fantorob.IniciarRobo(false);
					//adicionar ao inventario
					PlayerObjects.RobotsNotInUse.Add(Instantiate(Fantorob));
					if (EsqueletoEspecial) { PlayerObjects.EsqueletoEspecial--; }
				}					
					break;
				case 1:
					//ativar o objeto
					Weapon.IniciarArma(false);
				if(Weapon.CombosMax>0)
                {
					Weapon.MontarArma(Weapon.NumeroDeAtaques, Random.Range(Weapon.CombosMin, Weapon.CombosMax + 1));
				}				    
					PlayerObjects.NucleosFisicos.Add(Instantiate(Weapon));
					break;
			}

		//retira objetos do inventario
		PlayerObjects.Fantodin -= Preco;
		for (int i = 0; i < ObjetosNecessarios.Count; i++)
		{
			PlayerObjects.ItensConstruir[ObjetosNecessarios[i]] -= QuantidadesNecessarias[i];			
		}
	}
	public Sprite RertonarSprite()
    {
		Sprite sp = SpriteVazio;
		switch(Tipo)
        {
			case 0:
				sp = Fantorob.MenuIconeFantorob;
				break;
			case 1:
				sp = Weapon.MySprite;
				break;

        }
		return sp;
    }
	public string RetornarNome()
    {
		string nm = "";
		switch (Tipo)
		{
			case 0:
				nm = Fantorob.Nome;
				break;
			case 1:
				nm = Weapon.Nome[ManagerGame.Instance.Idm];
				break;
		}
		return nm;
	}
	public bool PossoConstruir()
    {
		bool posso = true;
		//verifica se o jogador tem todos os itens necessarios
		if (!EsqueletoEspecial || EsqueletoEspecial && PlayerObjects.EsqueletoEspecial > 0)
		{
			for (int i = 0; i < ObjetosNecessarios.Count; i++)
			{
				if (PlayerObjects.ItensConstruir[ObjetosNecessarios[i]] < QuantidadesNecessarias[i] || PlayerObjects.Fantodin < Preco)
				{
					posso = false;
					break;
				}
			}
		}
		else
        {
			posso = false;
        }		
		return posso;
    }
}
