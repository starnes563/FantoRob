using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class SequenciaCena : MonoBehaviour
{
    public List<int> ControlesDialogo;
    Queue<int> controledial;
    [HideInInspector]
    public int ProximoPausaDial;
    public PlayableDirector Director;
    public CutsceneDialogo CaixaDeDialogo;
    public Diretor Camera;
    private Walk Player;
    bool pausadoTexto = false;
    bool pausadoEvento = false;
    bool pausadoBatalha = false;
    [HideInInspector]
    public bool LerProxima = false;
   
    public Dialogo DialogoCutscene;
    public bool TutorialInicial = false;   
    // Start is called before the first frame update
    public void Começar(PlayableAsset pb)
    {
        if (!TutorialInicial)
        {
            Player = GameObject.FindWithTag("Player").GetComponent<Walk>();
            Player.PararDeAndar();
        }
        controledial = new Queue<int>();
        controledial.Clear();
        foreach(int c in ControlesDialogo)
        {            
            controledial.Enqueue(c);
        }
        ProximaPausa();
        this.gameObject.SetActive(true);
        Director.Play(pb);
        if(DialogoCutscene !=null)
        {
            CaixaDeDialogo.DialogoCut(DialogoCutscene, this);
        }
    }
    // Update is called once per frame
    void Update()
    {
       if(pausadoTexto && CaixaDeDialogo.PodeContinuar && !ManagerGame.Instance.EmBatalha || TutorialInicial&& pausadoTexto && CaixaDeDialogo.PodeContinuar)
        {            
            ProximaPausa();
            Resumir();
            CaixaDeDialogo.PodeContinuar = false;
        }    
       if(pausadoBatalha && CaixaDeDialogo.PodeContinuar && !ManagerGame.Instance.EmBatalha && !ManagerGame.Instance.Transitando)
        {           
            Resumir();
        }
        if (pausadoEvento && CaixaDeDialogo.PodeContinuar && !ManagerGame.Instance.EmBatalha && !ManagerGame.Instance.Transitando)
        {
            Resumir();
        }
    }
    public void Pausar()
    {
        Director.playableGraph.GetRootPlayable(0).SetSpeed(0);       
    }
    public void Resumir()
    {       
        Director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        pausadoBatalha = false;
        pausadoEvento = false;
        pausadoTexto = false;        
    }   
    public void Finalizar()
    {
        Player.CanIWalk = true;
        
        this.gameObject.SetActive(false);
    }
    public void ProximaPausa()
    {
        
        LerProxima = false;
        if(controledial.Count>0)
        {
            ProximoPausaDial = controledial.Dequeue();
        }
           
    }
    public void PausarComProximaParteTexto()
    {
        Pausar();
        pausadoTexto = true;        
    }
    public void PausarComProximoBatalha()
    {
        Pausar();
        pausadoBatalha = true;
    }
    public void PausarComProximoEvento()
    {
        Pausar();
        pausadoEvento = true;
    }
}
