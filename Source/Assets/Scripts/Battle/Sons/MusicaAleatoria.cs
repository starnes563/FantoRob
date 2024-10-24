using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicaAleatoria : MonoBehaviour
{
    public List<AudioClip> Musicas;
    public AudioClip MusicaRival;
    public AudioClip MusicaCampeao;
    public AudioClip MusicaFantoMascara;
    public AudioSource AudioSource;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (!ManagerGame.Instance.Campeao && !ManagerGame.Instance.Rival &&!ManagerGame.Instance.FantoMascara)
        {           
            int i = Random.Range(0, Musicas.Count);            
            AudioSource.clip = Musicas[i];
            AudioSource.loop = true;
            AudioSource.Play();
        }
        else if(!ManagerGame.Instance.Campeao && ManagerGame.Instance.Rival&&!ManagerGame.Instance.FantoMascara)
        {           
            AudioSource.clip = MusicaRival;
            AudioSource.loop = true;
            AudioSource.Play();
        }
        else if(ManagerGame.Instance.Campeao && !ManagerGame.Instance.Rival && !ManagerGame.Instance.FantoMascara)
        {            
            AudioSource.clip = MusicaCampeao;
            AudioSource.loop = true;
            AudioSource.Play();
        }
        else if (!ManagerGame.Instance.Campeao && !ManagerGame.Instance.Rival && ManagerGame.Instance.FantoMascara)
        {
            AudioSource.clip = MusicaFantoMascara;
            AudioSource.loop = true;
            AudioSource.Play();
        }
    }

    
}
