using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caixa : MonoBehaviour
{
    public AudioClip Fanfarra;
    public AudioSource source;
    private AudioSource caixaDeSom;
    public SpriteRenderer MeuSprite;
    // Start is called before the first frame update
    void Start()
    {
        caixaDeSom = GameObject.Find("CaixaDeSom").GetComponent<AudioSource>();        
        caixaDeSom.Pause();
        source.PlayOneShot(Fanfarra);       
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
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
        if (!source.isPlaying)
        {           
            Time.timeScale = 1f;            
            Destroy(gameObject);
        }
    }
   public void ReceberImagem(Sprite sp)
    {
        MeuSprite.sprite = sp;
    }
}
