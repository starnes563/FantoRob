using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerarRivalAletorio : MonoBehaviour
{
    //escondi as variaveis usadas anteriormente
    [Range(10,50)]
    public int Dificuldade;
    [HideInInspector]
    public List<Sprite> Sprite;
    public List<GameObject> PossiveisPontosDeInst = new List<GameObject>();
    public List<GameObject> RivaisRegulares = new List<GameObject>();
    public List<GameObject> RivaisDaDungeon = new List<GameObject>();
    public List<FantoRob> FantoRobs;
    public List<Weapon> Fisicos;
    [HideInInspector]
    public List<string> Consoantes;
    [HideInInspector]
    public List<string> Vogais;
    public List<int> Dinheiro;
    public List<TextosRivaisDesafio> Textos = new List<TextosRivaisDesafio>();
    public int QuantidadeRivaisMaxima;
    public bool DestravaBau;
    public BauDesafio MeuBau;
    public bool DestravaPorta;
    public BloqueioDesafio MinhaPorta;
    private void Start()
    {
        if (DestravaBau)
        {
            MeuBau.InimigosADerrotar = new List<GameObject>();
        }
        if (DestravaPorta)
        {
            MinhaPorta.InimigosADerrotar = new List<GameObject>();
        }
        StartCoroutine(InstanciaRivais());        
    }
    IEnumerator InstanciaRivais()
    {
        //decidir quantos rivais vai instanciar
        int qtd = Random.Range(1, QuantidadeRivaisMaxima+1);
        //gerar os rivais
        for(int i = 0; i<qtd; i++)
        {
            //decide se o rival vai ser regular ou especifico da dungeon
            GameObject rival;
            if (Random.Range(1, 101) > 60 && !DestravaBau && !DestravaPorta)
            {
                rival = RivaisRegulares[Random.Range(0, RivaisRegulares.Count)];
                //geradialogo  
                string text = rival.GetComponent<NPCBattle>().Nome[ManagerGame.Instance.Idm]
                    + ":"+ Textos[ManagerGame.Instance.Idm].FalaRivaisRegulares[Random.Range(0, Textos[ManagerGame.Instance.Idm].FalaRivaisRegulares.Count)]; 
                Dialogo dil = ScriptableObject.CreateInstance<Dialogo>();
                dil.Sentencas = new List<string>();
                dil.Sentencas.Add(text);
                rival.GetComponent<NPCBattle>().Dialogos.Add(dil);
                rival.GetComponent<NPCBattle>().InEscolhaDialogo.Add(0);
            }
            else
            {
                rival = RivaisDaDungeon[Random.Range(0, RivaisDaDungeon.Count)];
                //geradialogo  
                string text = "";
                switch (ManagerGame.Instance.Idm)
                {
                    case 0:
                        text = rival.GetComponent<NPCBattle>().Nome[ManagerGame.Instance.Idm] + " " + Textos[ManagerGame.Instance.Idm].AdjetivosRivaisLocais[Random.Range(0, Textos[ManagerGame.Instance.Idm].AdjetivosRivaisLocais.Count)]
                    + ":" + Textos[ManagerGame.Instance.Idm].FalaRivaisLocais[Random.Range(0, Textos[ManagerGame.Instance.Idm].FalaRivaisLocais.Count)];                        
                        break;
                    case 1:
                        text = Textos[ManagerGame.Instance.Idm].AdjetivosRivaisLocais[Random.Range(0, Textos[ManagerGame.Instance.Idm].AdjetivosRivaisLocais.Count)] + " " + rival.GetComponent<NPCBattle>().Nome[ManagerGame.Instance.Idm]
                    + ":" + Textos[ManagerGame.Instance.Idm].FalaRivaisLocais[Random.Range(0, Textos[ManagerGame.Instance.Idm].FalaRivaisLocais.Count)];
                        break;
                }
                Dialogo dil = ScriptableObject.CreateInstance<Dialogo>();
                dil.Sentencas = new List<string>();
                dil.Sentencas.Add(text);
                rival.GetComponent<NPCBattle>().Dialogos.Add(dil);
                rival.GetComponent<NPCBattle>().InEscolhaDialogo.Add(0);
            }
            //decide o ponto de instanciamento
            GameObject posic = PossiveisPontosDeInst[Random.Range(0, PossiveisPontosDeInst.Count)];
            //retira para nao repetir
            PossiveisPontosDeInst.Remove(posic);
            //gera o rival
            GerarBatalha(rival.GetComponent<NPCBattle>(), posic);
            
        }

        yield return null;
    }

    public void GerarBatalha(NPCBattle NPC, GameObject Posicao)
    {
       NPC.Robots.Clear();
       NPC.Tipo = 0;       
       NPC.Estrelas = PlayerStatus.Estrelas;
       NPC.Nivel = Nivel(NPC.Estrelas);
       int difimax = Mathf.RoundToInt(Dificuldade / 10);
       if (difimax < 1) { difimax = 1; }
       //para o modo easy
        //NPC.Dificuldade = Random.Range(0, difimax);
        NPC.Dificuldade = 1;
       NPC.Exp = Exp(NPC.Estrelas);
       NPC.Money = GerarDinheiro();        
       NPC.Persegue = true;
        NPC.Velocidade = Random.Range(8, 11);
        //gera time
       int numerofanto = 3;
        NPC.Trend = 3;
        if (Dificuldade<=20)
       {
           numerofanto = 1;
            NPC.Trend = 1;
       }
       else if(Dificuldade > 20 && Dificuldade <= 55)
       {
           numerofanto = Random.Range(1, 3);
            NPC.Trend = Random.Range(1, 4);
        }
       for(int i = 0; i<numerofanto; i++)
       {
           NPC.Robots.Add(GerarFantorob(NPC.Estrelas));
       }
       GameObject npc =  Instantiate(NPC.gameObject, Posicao.transform.position, Quaternion.identity);
       npc.transform.position = new Vector3(npc.transform.position.x, npc.transform.position.y, 0f);
        if (DestravaBau)
        {
            MeuBau.InimigosADerrotar.Add(npc);
        }
        if(DestravaPorta)
        {
            MinhaPorta.InimigosADerrotar.Add(npc);
        }
     }
    string Nome()
    {
        string nm = "";
        int tamanho = Random.Range(4, 9);
        bool vogal = false;
        if (Random.Range(0, 11) > 5) { vogal = true; }
        for (int i = 0; i<tamanho;i++)
        {
            switch(vogal)
            {
                case false:
                    nm = nm + Consoantes[Random.Range(0, Consoantes.Count)];
                    vogal = true;
                    break;
                case true:
                    nm = nm + Vogais[Random.Range(0, Vogais.Count)];
                    vogal = false;
                    break;
            }
        }
        nm = nm + "#" + Random.Range(1000, 9999).ToString();
        return nm;
    }
    int Estrelas()
    {
        int b = PlayerStatus.Estrelas - 1;
        if (b < 0) { b = 0; }
        int est = Random.Range(b, PlayerStatus.Estrelas + 2);
        if (est > 8) { est = 8; }
        return est;
    }
    int Nivel(int estrelas)
    {
        int nivel = 0;
        switch(estrelas)
        {
            case 0:
                nivel = Random.Range(0, 5);
                break;
            case 1:
                nivel = Random.Range(5, 11);
                break;
            case 2:
                nivel = Random.Range(11, 19);
                break;
            case 3:
                nivel = Random.Range(19, 31);
                break;
            case 4:
                nivel = Random.Range(31, 47);
                break;
            case 5:
                nivel = Random.Range(47, 67);
                break;
            case 6:
                nivel = Random.Range(71, 100); ;
                break;
            case 7:
                nivel = Random.Range(100, 152);
                break;
            case 8:
                nivel = Random.Range(152, 10000);
                break;
        }
        return nivel;
    }
    int Exp(int estrelas)
    {
        int e = Random.Range(1, 3);

        if(estrelas>=5 && estrelas<7)
        {
            e = Random.Range(2,4);
        }
        else if(estrelas ==7)
        {
            e = Random.Range(3, 5);
        }
        else if(estrelas >=8)
        {
            e = Random.Range(4, 6);
        }
        return e;
    }
    int GerarDinheiro()
    {
        int din;
        din = Random.Range(Dinheiro[0], Dinheiro[1]);
        int divisor = PlayerStatus.Estrelas + 2;
        din = Mathf.RoundToInt(din / divisor);
        return din;
    }
    FantoRob GerarFantorob(int estrelas)
    {       
        int fantorob = Random.Range(0, FantoRobs.Count);
        int fisico = Random.Range(0, Fisicos.Count);            
        //testar todos os fantorobs na batalha       
        FantoRob rob = Instantiate(FantoRobs[fantorob]);       
        rob.Fisico = Instantiate(Fisicos[fisico]);
        //montar ataques
        PorEmAtivos(rob.MovimentoAmbos, rob.Fisico);
        PorEmAtivos(rob.MovimentoInimigo, rob.Fisico);
        PorEmAtivos(rob.Fisico.MovimentosAmbos, rob.Fisico);
        PorEmAtivos(rob.Fisico.MovimentosInimigo, rob.Fisico);       
        //adiciona moves aleatorios
        for (int i=0; i< Random.Range(1,4);i++)
        {            
                int nivelmv = 1;
                switch(estrelas)
                {
                    case 0:
                        nivelmv = 1;
                        break;
                    case 1:
                        nivelmv = Random.Range(1,2);
                        break;
                    case 2:
                        nivelmv = Random.Range(1, 2);
                        break;
                    case 3:
                        nivelmv = Random.Range(1, 3);
                        break;
                    case 4:
                        nivelmv = Random.Range(1, 3);
                        break;
                    case 5:
                        nivelmv = Random.Range(2, 4);
                        break;
                    case 6:
                        nivelmv = Random.Range(2, 3);
                        break;
                    case 7:
                        nivelmv = 4;
                        break;
                    case 8:
                        nivelmv = 4;
                        break;
                }
                rob.Fisico.MovesAtivos.Add(Instantiate(Constructor.MoveConstructor(nivelmv)));
         }              
        //retira ate ficar igua o attacksmax e nao dar erro na hora de carregar o ataque
        while(rob.Fisico.MovesAtivos.Count > rob.Fisico.AttacksMax)
        {
            rob.Fisico.MovesAtivos.RemoveAt(Random.Range(0, rob.Fisico.MovesAtivos.Count));
        }
        //carrega os ataque
        rob.Fisico.CarregarAtaques();      
        //criar combo
        if(estrelas<3 && rob.Fisico.Model == 5)
        {
            rob.Fisico.MontarArma(rob.Fisico.AttacksMax, Random.Range(3, 5));
        }
        else if(estrelas>=3)
        {
            if(rob.Fisico.Model == 5)
            {
                rob.Fisico.MontarArma(rob.Fisico.AttacksMax, Random.Range(3, 8));
            }
            else
            {
                rob.Fisico.MontarArma(rob.Fisico.AttacksMax, Random.Range(3, 7));
            }
        }
        //montarstatus
        float intg = Random.Range(0, Dificuldade) / 100;
        rob.Integridade = Mathf.RoundToInt(rob.IntegridadeMinima/2+rob.IntegridadeMinima*intg);
       // rob.Integridade = 5;
        rob.Resistencia = calcularStatus(rob.ResistenciaMinima, rob.ResistenciaMaxima);      
        rob.Ataque = calcularStatus(rob.AtaqueMinimo, rob.AtaqueMaximo);        
        rob.AtaqueElemental = calcularStatus(rob.AtaqueElementalMinimo, rob.AtaqueElementalMaximo);       
        rob.Velocidade = calcularStatus(rob.VelocidadeMinima, rob.VelocidadeMaxima);        
        rob.IntegridadeAtual = rob.Integridade;        
        float fator = Random.Range(85, 101);       
        rob.BateriaAtual = (int)(rob.Bateria * fator/100);        
        return rob;        
    }
    int calcularStatus(int minimo, int maximo)
    {
        int st;
        float ale = Random.Range(0, Dificuldade) / 100;
        st =Mathf.RoundToInt( minimo + ((maximo - minimo) * ale));
        return st;
    }
    void PorEmAtivos(List<Move>m, Weapon f)
    {
        if(m != null && m.Count>0)
        {
            if(m.Count>=1)
            {
                f.MovesAtivos.Add(m[m.Count-1]);
            }
            if (m.Count >= 2)
            {
                f.MovesAtivos.Add(m[m.Count - 2]);
            }

        }
    }
}
[System.Serializable]
public class TextosRivaisDesafio
{
    public string Lingua;    
    public List<string> FalaRivaisRegulares;
    public List<string> AdjetivosRivaisLocais;
    public List<string> FalaRivaisLocais;
}

