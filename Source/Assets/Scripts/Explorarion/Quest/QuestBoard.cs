using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestBoard : MonoBehaviour
{
    public Text Nome;
    public Text Descricao;
    public Text Atual;
    public Text Total;
    public GameObject Completo;
    public AudioSource Source;
    public AudioClip SomReceberMissao;
    public AudioClip SomCompletarMissao;
    Quest MinhaQuest;
    public RandomQuest Menu;
    public List<MostrarRecompensa> MostrarRecompensas;
   
    public void MontarQuadro(Quest q)
    {        
        Nome.text = q.Nome[ManagerGame.Instance.Idm];       
        Descricao.text = q.Descriçao[ManagerGame.Instance.Idm];        
        Atual.text = q.Atual.ToString();      
        Total.text = q.Requerido.ToString();      
        Completo.SetActive(false);       
        if (q.Completo)
        {
            Source.PlayOneShot(SomCompletarMissao);
            Completo.SetActive(true);
        }
        else
        {
            Source.PlayOneShot(SomReceberMissao);
        }
        for (int i = 0; i < q.Recompensas.Count; i++)
        {
            MostrarRecompensas[i].Mostrar(q.Recompensas[i]);
        }
        MinhaQuest = q;       
    }    
    public void Destruir()
    {
        Destroy(this.gameObject);
    }
    public void PegarMissao()
    {
        MinhaQuest.AdicionarMissao();
        Menu.RetirarQuest(MinhaQuest);
        Menu.GerarMenu();
    }
    public void PegarMissaoNPC()
    {
        MinhaQuest.AdicionarMissao();        
    }
    void Update()
    {
        if (gameObject.activeSelf && Input.GetButtonDown("Fire1"))
        {
            LiberarAndar();
            gameObject.SetActive(false);
        }
    }
    public void LiberarAndar()
    {
        GameObject.FindWithTag("Player").GetComponent<Walk>().CanIWalk = true;
    }
}
