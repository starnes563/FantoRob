using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaDeSom : MonoBehaviour
{
    public static CaixaDeSom Instancia = null;    

    private AudioSource source;
    // Start is called before the first frame update
    void Awake()
    {
       if(Instancia != null && Instancia !=this)
        {
            
            if(GetComponent<AudioSource>().clip == Instancia.GetComponent<AudioSource>().clip)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Destroy(Instancia.gameObject);
                Instancia = this;
                source = Instancia.GetComponent<AudioSource>();
                source.volume = 0;
                StartCoroutine(AumentaVolume());
            }
        }
       else
        {

            Instancia = this;
            DontDestroyOnLoad(this.gameObject);
        }        
    }
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = 0;
       StartCoroutine(AumentaVolume());
    }

    public void SobeVolume()
    {        
        source.volume = 0;
        StartCoroutine(AumentaVolume());
    }   
    public IEnumerator AumentaVolume()
    {
        if (Instancia.source != null)
        {
            while (Instancia.source.volume < 3)
            {
                Instancia.source.volume += Time.deltaTime;
                yield return null;
            }
        }
    }   
}
