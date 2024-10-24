using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomQuest : MonoBehaviour
{
    [Range(0,10)]
    public int Dificuldade;
    public List<ListaNomes> Nomes = new List<ListaNomes>();
    public List<DescricaoIdm> DescriçõesIdm = new List<DescricaoIdm>();
    public List<int> ElementosBase = new List<int>();
    public List<int> ElementosTopo = new List<int>();
    public List<Recompensa> Recompensas = new List<Recompensa>();
    List<Quest> QuestsAtuais = new List<Quest>();   
    public Button BotaoMissao;
    public Image Spacer;
    List<GameObject> botoes = new List<GameObject>();
    public QuestBoard QuadroMissoes;
    public AudioSource AudioSource;
    private void Start()
    {

        //if(ManagerGame.Tempo - TempoMissao > 15)
       // {
          //  gerarMissao();
           // TempoMissao = ManagerGame.Tempo;
      //  }
    }
    private void OnEnable()
    {

        if(ManagerGame.Instance.Tempo - StoryEvents.UltTempoMissao > 900f)
       {
            gerarMissao();
            StoryEvents.UltTempoMissao = ManagerGame.Instance.Tempo;
       }
        this.transform.position = new Vector3(this.transform.position.x, -6.15f);
        LeanTween.moveLocalY(this.gameObject, 0.71f, 0.4f);
        GerarMenu();
    }
    public void GerarMenu()
    {
       if(botoes!=null)
        {
            if(botoes.Count>0)
            {
                foreach(GameObject g in botoes)
                {
                    Destroy(g);
                }
            }
            botoes.Clear();
        }        
        foreach (Quest q in QuestsAtuais)
        {
            Button b = Instantiate(BotaoMissao, Spacer.transform) as Button;
            botoes.Add(b.gameObject);
            b.GetComponent<BtMissoes>().Criar(q, QuadroMissoes, AudioSource);
        }
    }
    void gerarMissao()
    {
        QuestsAtuais.Clear();
        for (int k = 0; k < 4; k++)
        {           
            //decidetipo
            int tp;
            if (Dificuldade < 4)
            {
                tp = Random.Range(0, 8);
            }
            else
            {
                tp = Random.Range(0, 10);
            }
            //decide objeto
            int obj = Random.Range(ElementosBase[tp], ElementosTopo[tp] + 1);
            //decidequantidade
            int rg;
            if (Dificuldade < 4)
            {
                rg = Random.Range(0, 6);
            }
            else if (Dificuldade < 8)
            {
                rg = Random.Range(0, 14);
            }
            else
            {
                rg = Random.Range(0, 22);
            }
            //criar nome
            List<string> nomes = new List<string>();
            foreach (string l in Nomes[tp].Nomes)
            {
                nomes.Add(l);
            }
            //criar descricao
            List<string> desc = new List<string>();
            for (int i = 0; i < DescriçõesIdm.Count; i++)
            {
                desc.Add(DescriçõesIdm[i].Descrições[tp].Parte1[0] + " " + rg + " " + DescriçõesIdm[i].Descrições[tp].Objetos[obj] + " " + DescriçõesIdm[i].Descrições[tp].Parte2[0]);
            }
            //criar o tipo
            Quest.TipoDeQuest MTipo = Quest.TipoDeQuest.BATALHA;
            switch (tp)
            {
                case 0:
                    MTipo = Quest.TipoDeQuest.BATALHA;
                    break;
                case 1:
                    MTipo = Quest.TipoDeQuest.VENCERFANTOROB;
                    break;
                case 2:
                    MTipo = Quest.TipoDeQuest.VENCEELEMENTO;
                    break;
                case 3:
                    MTipo = Quest.TipoDeQuest.VENCEFISICO;
                    break;
                case 4:
                    MTipo = Quest.TipoDeQuest.USAFANTOROB;
                    break;
                case 5:
                    MTipo = Quest.TipoDeQuest.USAELEMENTO;
                    break;
                case 6:
                    MTipo = Quest.TipoDeQuest.USAFISICO;
                    break;
                case 7:
                    MTipo = Quest.TipoDeQuest.SUPEREFETIVO;
                    break;
                case 8:
                    MTipo = Quest.TipoDeQuest.COMBO;
                    break;
                case 9:
                    MTipo = Quest.TipoDeQuest.HABILIDADE;
                    break;
            }
            //criarRecompensas
            int vezes = Random.Range(1, 3);
            List<Recompensa> r = new List<Recompensa>();
            for (int i = 0; i < vezes; i++)
            {
                r.Add(Recompensas[Random.Range(0, Recompensas.Count)]);
                r[i].quantidade = Random.Range(1, 3);
            }
            //criarquest
            Quest q = new Quest(MTipo, rg, obj, nomes, desc, r);
            QuestsAtuais.Add(q);            
        }    
    }
    public void RetirarQuest(Quest q)
    {
        if(QuestsAtuais.Contains(q))
        {
            QuestsAtuais.Remove(q);
        }
    }    
}
[System.Serializable]
public class ListaNomes
{
    public List<string> Nomes = new List<string>();
}
[System.Serializable]
public class ListaDescricao
{
    public List<string> Parte1 = new List<string>();
    public List<string> Objetos = new List<string>();
    public List<string> Parte2 = new List<string>();
}
[System.Serializable]
public class DescricaoIdm
{
    public List<ListaDescricao> Descrições = new List<ListaDescricao>();
}

