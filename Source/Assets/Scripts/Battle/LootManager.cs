using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour
{
    //[HideInInspector]
    public List<Loot> LootBatalha = new List<Loot>();   
    public TelaFimBatalha TelaFimBatalha;
    public IEnumerator GerarLoot()
    {
        //trending
        int trend = Mathf.RoundToInt(PlayerStatus.Trending / 2);
        LootBatalha.Clear();
        int maxloot = Mathf.RoundToInt(3 + PlayerStatus.Trending / 10);
        if (maxloot > 6) { maxloot = 6; }
        int numeroloot = Random.Range(1, maxloot);       
        while (LootBatalha.Count<numeroloot)
        {          
            Loot proximo = ManagerGame.Instance.Regiao.PossibleLoot[Random.Range(0, ManagerGame.Instance.Regiao.PossibleLoot.Count)];           
            if (!LootBatalha.Contains(proximo))
            {                
                if (Random.Range(0, 101) < proximo.ChanceDeDrop+ trend) { LootBatalha.Add(proximo); }
                yield return null;
            }
            if(proximo.MeuTipo == Loot.TipodeLoot.PARTEROBO)
            {
                proximo.GeraPart();
            }
        }        
    }    
}
