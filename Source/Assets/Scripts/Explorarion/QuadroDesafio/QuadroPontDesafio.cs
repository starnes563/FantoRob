using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class QuadroPontDesafio : MonoBehaviour
{
    public List<ParticipanteDEsafio> Participantes = new List<ParticipanteDEsafio>(27);
    //0-escritorios
    //1-fazendadagua
    //2-barco
    //3-castelo
    //4-minas
    //5-mansao
    //6-centroentreterimento
    //7-final
    int desafioAtual = 0;
    public List<int> PontosPorDias;
    public List<PontuacaoDesafio> PontuacaoDesafios;
    public List<LinhaQuadroDesafio> Linhas;
    List<ParticipanteDEsafio> Ranking = new List<ParticipanteDEsafio>();
    public AudioSource Source;
    public AudioClip SomQuadro;
    public AudioClip SomConfirma;
    public int Saida;
    public Vector3 PosicaoSaida;
    public int FimDesafio;
    public int CenaDesclassificado;
    public Diretor MainCamera;
    bool liberado;
    // Start is called before the first frame update
    void Start()
    {
        desafioAtual = StoryEvents.DesafioAtual;
        for (int i = 0; i < Participantes.Count; i++)
        {
            Linhas[i].MostrarParticipante(Participantes[i]);           
        }
        Participantes[0].PontuacaoAtual = PlayerStatus.Pontos;        
        for (int i = 1; i < Participantes.Count; i++)
        {
            Participantes[i].PontuacaoAtual = StoryEvents.PontuacoesParticipantes[i];
            Participantes[i].PosicaoAtual = StoryEvents.PosicaoParticipantes[i];
            
        }       
        DiaCompletaAleatorio();
        PassarPontuacao();
        Organizar();       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")&& liberado)
        {
            liberado = false;
            Finalizar();
        }
    }
    void DiaCompletaAleatorio()
    {
        int max = 0;      

        switch (desafioAtual)
        {
            case 0:
                max = 5;
                break;
            case 1:
                max = 4;
                break;
            case 2:
                max = 3;
                break;
            case 3:
                max = 4;
                break;
            case 4:
                max = 3;
                break;
            case 5:
                max = 4;
                break;
            case 6:
                max = 3;
                break;
        }
        for (int i = 1; i < Participantes.Count; i++)
        {
            if(Participantes[i].Aleatorio)
            {
                PontuacaoDesafios[i].DiaQueCompleta[desafioAtual] = Random.Range(0, max+1);
            }
            
        }
    }
    public void PassarPontuacao()
    {
        //jogador de acordo com dias
        Participantes[0].PontuacaoAtual += PontosPorDias[PlayerStatus.DaysLeft];        
        for (int i = 1; i<Participantes.Count;i++)
        {           
            if (!StoryEvents.TrapacaDesafio[desafioAtual])
            {
                Participantes[i].PontuacaoAtual += PontosPorDias[PontuacaoDesafios[i].DiaQueCompleta[desafioAtual]];
            }
            else
            {               
                if (PontuacaoDesafios[i].DiaQueCompleta[desafioAtual] <= PlayerStatus.DaysLeft)
                {
                    if (PontuacaoDesafios[i].DiaQueCompleta[desafioAtual] - 1 >=0)
                    {
                        Debug.Log(i);

                        Participantes[i].PontuacaoAtual += PontosPorDias[PontuacaoDesafios[i].DiaQueCompleta[desafioAtual] - 1];
                    }
                    else
                    {
                        Participantes[i].PontuacaoAtual += PontosPorDias[PontuacaoDesafios[i].DiaQueCompleta[desafioAtual]];
                    }
                   
                }
                else
                {                    
                    Participantes[i].PontuacaoAtual += PontosPorDias[PontuacaoDesafios[i].DiaQueCompleta[desafioAtual]];
                }                
            }
           
        }        
    }
    public void Organizar()
    {
        Ranking.Clear();
        foreach (ParticipanteDEsafio p in Participantes)
        {
            Ranking.Add(p);
        }
        Ranking = Ranking.OrderByDescending(x => x.PontuacaoAtual).ToList();
        foreach (ParticipanteDEsafio p in Participantes)
        {
            p.PosicaoAtual = Ranking.IndexOf(p)+1;
        }
    }
    public void MostrarDia()
    {
        Linhas[0].MostrarDia(PlayerStatus.DaysLeft, PontosPorDias[PlayerStatus.DaysLeft]);
        for (int i = 1; i < Participantes.Count; i++)
        {
            if (!StoryEvents.TrapacaDesafio[desafioAtual])
            {
                Linhas[i].MostrarDia(PontuacaoDesafios[i].DiaQueCompleta[desafioAtual], PontosPorDias[PontuacaoDesafios[i].DiaQueCompleta[desafioAtual]]);
            }
            else
            {
                if (PontuacaoDesafios[i].DiaQueCompleta[desafioAtual] <= PlayerStatus.DaysLeft)
                {
                    if (PontuacaoDesafios[i].DiaQueCompleta[desafioAtual] - 1 >= 0)
                    {
                        Linhas[i].MostrarDia(PontuacaoDesafios[i].DiaQueCompleta[desafioAtual]-1, PontosPorDias[PontuacaoDesafios[i].DiaQueCompleta[desafioAtual]-1]);
                    }
                    else
                    {
                        Linhas[i].MostrarDia(PontuacaoDesafios[i].DiaQueCompleta[desafioAtual], PontosPorDias[PontuacaoDesafios[i].DiaQueCompleta[desafioAtual]]);
                    }

                }
                else
                {
                    Linhas[i].MostrarDia(PontuacaoDesafios[i].DiaQueCompleta[desafioAtual], PontosPorDias[PontuacaoDesafios[i].DiaQueCompleta[desafioAtual]]);
                }
            }
        }
        Source.PlayOneShot(SomQuadro);
    }
    public void MostrarPosicao()
    {
        for (int i = 0; i < Ranking.Count; i++)
        {
            Linhas[i].MostrarPosicao(Ranking[i]);
        }
        Source.PlayOneShot(SomQuadro);
    }
    public void LiberarFinalizar()
    {
        liberado = true;
    }
    public void Finalizar()
    {
        Source.PlayOneShot(SomConfirma);
        if (PlayerStatus.Estrelas > StoryEvents.DesafioAtual) { StoryEvents.DesafioAtual++; }
        PlayerStatus.Pontos = Participantes[0].PontuacaoAtual;
        PlayerStatus.Posicao = Participantes[0].PosicaoAtual;
        PlayerStatus.DaysLeft = 7;
        for (int i = 0; i < Participantes.Count; i++)
        {
            StoryEvents.PontuacoesParticipantes[i] = Participantes[i].PontuacaoAtual;
            StoryEvents.PosicaoParticipantes[i] = Participantes[i].PosicaoAtual;
        }       
        //verificar qual cena direcionar
        if(StoryEvents.DesafioAtual<7)
        {
            ManagerGame.Instance.SceneToLoad = Saida;
            PlayerStatus.NextHeroPosition = PosicaoSaida;
        }
        else
        {
            if (PlayerStatus.Posicao <= 8)
            {
                ManagerGame.Instance.SceneToLoad = FimDesafio;
            }
            else
            {
                ManagerGame.Instance.SceneToLoad = CenaDesclassificado;
            }
        }
        foreach (FantoRob fanto in PlayerObjects.RobotsInUse)
        {
            if (fanto != null)
            {
                fanto.Curartudo();
            }
        }
        StoryEvents.RegeraEstoque = true;
        MainCamera.TrocarACena();
        this.gameObject.SetActive(false);
    }

}


[System.Serializable]
public class PontuacaoDesafio
{
    public List<int> DiaQueCompleta;
}
