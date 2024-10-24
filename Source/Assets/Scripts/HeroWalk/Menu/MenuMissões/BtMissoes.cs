using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtMissoes : MonoBehaviour
{
    public Quest MyQuest;
    public Text MeuNome;
    private QuestBoard Quadro;
    private AudioSource Audio;
    public AudioClip SomClicar;

    public void Criar(Quest m, QuestBoard q, AudioSource ad)
    {
        MeuNome.text = m.Nome[ManagerGame.Instance.Idm];       
        MyQuest = m;
        Quadro = q;
        Audio = ad;
    }
    public void Clicar()
    {
        Audio.PlayOneShot(SomClicar);
        Quadro.MontarQuadro(MyQuest);
        Quadro.gameObject.SetActive(true);
    }
}
