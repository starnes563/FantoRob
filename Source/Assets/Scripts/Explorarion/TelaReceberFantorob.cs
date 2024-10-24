using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TelaReceberFantorob : MonoBehaviour
{
    public AudioClip Fanfarra;
    private AudioSource source;
    private AudioSource caixaDeSom;
    private SequenciaCena Director;
    public PlayableAsset Cena2;
    // Start is called before the first frame update
    void Start()
    {
        caixaDeSom = GameObject.Find("CaixaDeSom").GetComponent<AudioSource>();
        source = GetComponent<AudioSource>();
        caixaDeSom.Pause();
        source.PlayOneShot(Fanfarra);
        Director = GameObject.FindWithTag("Cutscene1").GetComponent<SequenciaCena>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!source.isPlaying)
        {
            caixaDeSom.UnPause();
        }
        if (gameObject.activeSelf && Input.GetButtonDown("Fire1"))
        {
            Destruir();
        }
    }
    public void Destruir()
    {
        if(!source.isPlaying)
        {
            Time.timeScale = 1f;
            //ManagerGame.Instance.IniciaCustcene();
            Director.Começar(Cena2);
            Destroy(this.gameObject);
        }       
    }
}
