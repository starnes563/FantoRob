using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelaFimBatalha : MonoBehaviour
{
    public List<GameObject> PainelLoot;
    public List<Image> SpritesLoot;
    public List<Text> NomesLoot;
    public Text Dinheiro;
    public Text Trend;
    private LootManager Lootm;
    public AudioSource AudioSource;
    public AudioClip Fanfarra1;
    public AudioClip Fanfarra2;
    public AudioClip MusicaPerdeu;
    bool ganhou = false;
    bool podefechar = false;
    public List<string> Vitoria;
    public List<string> Derrota;
    public Text TextoDeCima;
    public Text AvisoPerdeuUmDia;
    public List<string> AvisoUmDia;
    public List<string> AvisoDesclassificado;
  public void CriarTelaFim(LootManager lootm)
    {
        Lootm = lootm;
        if (lootm.LootBatalha.Count>0)
        {            
            for (int i = 0; i < lootm.LootBatalha.Count; i++)
            {
                switch (lootm.LootBatalha[i].MeuTipo)
                {
                    case Loot.TipodeLoot.ITEMCONSTRUIR:
                        SpritesLoot[i].sprite = Constructor.RetornarSprite(6, 0, 0, lootm.LootBatalha[i].Propriedade,0);
                        NomesLoot[i].text = Constructor.RetornarNome(6, 0, 0, 0, lootm.LootBatalha[i].Propriedade,0);
                        break;
                    case Loot.TipodeLoot.PENTEVAZIO:
                        SpritesLoot[i].sprite = Constructor.RetornarSprite(4, 0, 0, lootm.LootBatalha[i].Propriedade,0);
                        NomesLoot[i].text = Constructor.RetornarNome(1, 0, 0, 0, 0,0);
                        break; ;
                    case Loot.TipodeLoot.PENTECHEIO:
                        SpritesLoot[i].sprite = Constructor.RetornarSprite(1, 0, 0, lootm.LootBatalha[i].Propriedade,0);
                        NomesLoot[i].text = Constructor.RetornarNome(1, 0, 0, 0, 0,0);
                        break;
                    case Loot.TipodeLoot.SILICIO:
                        SpritesLoot[i].sprite = Constructor.RetornarSprite(0, 0, 0, 0,0);
                        NomesLoot[i].text = Constructor.RetornarNome(0, 0, 0, 0, 0,0);
                        break;
                    case Loot.TipodeLoot.PARTEROBO:
                        SpritesLoot[i].sprite = Constructor.RetornarSprite(7, 0, 0, 0, lootm.LootBatalha[i].partid);
                        NomesLoot[i].text = Constructor.RetornarNome(7, 0, 0, 0, 0, lootm.LootBatalha[i].partid);
                        break;
                    case Loot.TipodeLoot.CIRCUITO:
                        SpritesLoot[i].sprite = Constructor.RetornarSprite(5,0,lootm.LootBatalha[i].Propriedade,0,0);
                        NomesLoot[i].text = Constructor.RetornarNome(5,0,0, lootm.LootBatalha[i].Propriedade,0,0);
                        break;
                }
            }
        }        
    }
    public void MostrarTelaGanhou()
    {       
        this.gameObject.SetActive(true);
        TextoDeCima.text = Vitoria[ManagerGame.Instance.Idm];
        Dinheiro.text = ManagerGame.Instance.Money.ToString();
        Trend.text = ManagerGame.Instance.Trend.ToString();
        if (Lootm != null)
        {
            if (Lootm.LootBatalha.Count > 0)
            {
                for (int i = 0; i < Lootm.LootBatalha.Count; i++)
                {
                    PainelLoot[i].SetActive(true);
                }
            }
        }
        AudioSource.clip = Fanfarra1;        
        AudioSource.Play();
        AudioSource.loop = false;
        ganhou = true;
    }
    public void MostrarTelaPerdeu()
    {
        TextoDeCima.text = Derrota[ManagerGame.Instance.Idm];
        this.gameObject.SetActive(true);
        int money;
        if (PlayerObjects.Fantodin >= ManagerGame.Instance.Money / 2)
        {
          money = ManagerGame.Instance.Money / 2 * -1;
        }
        else
        {
          money = PlayerObjects.Fantodin;
        }
        int trend;
         if(PlayerStatus.Trending >= ManagerGame.Instance.Trend / 2)
        {
          trend = ManagerGame.Instance.Trend / 2 * -1;
        }
        else
        {
            trend = PlayerStatus.Trending;
        }
        Dinheiro.text = money.ToString();
        Trend.text = trend.ToString();
        AudioSource.clip = MusicaPerdeu;        
        AudioSource.loop = true;
        podefechar = true;
        AudioSource.Play();
        if (PlayerStatus.CartaEndosso && ManagerGame.Instance.Regiao.Desafio || ManagerGame.Instance.Regiao.Cutscene)
        {
            AvisoPerdeuUmDia.gameObject.SetActive(true);
            if(PlayerStatus.DaysLeft>0)
            {
                AvisoPerdeuUmDia.text = AvisoUmDia[ManagerGame.Instance.Idm];
            }
            else
            {
                AvisoPerdeuUmDia.text = AvisoDesclassificado[ManagerGame.Instance.Idm];
            }
            
        }
        else
        {
            AvisoPerdeuUmDia.gameObject.SetActive(false);
        }

    }
    private void Update()
    {       
       if(ganhou)
       {           
            if(!AudioSource.isPlaying)
            {
                AudioSource.clip = Fanfarra2;
                AudioSource.Play();
                AudioSource.loop = true;
                ganhou = false;
                podefechar = true;
            }
        }
        if (podefechar && Input.GetMouseButtonDown(0) || podefechar && Input.GetButtonDown("Fire1"))
        {
            podefechar = false;
            ManagerGame.Instance.AnimacaoFimDaBatalha();
            StartCoroutine(AbaixaVolume());
        }
    }
    public IEnumerator AbaixaVolume()
    {
        while (AudioSource.volume > 0)
        {
            AudioSource.volume-= Time.deltaTime;
            yield return null;
        }
    }
}
