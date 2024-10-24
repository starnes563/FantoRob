using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarregarLinguaSalva : MonoBehaviour
{
    public AlterarIdioma AlterarIdioma;
    public GameObject MenuInicial;
    public AudioSource Source;
    public AudioClip Clip;
    private void Start()
    {
        if(PlayerPrefs.HasKey("Escolhido"))
        {            
            ManagerGame.Instance.Idm = PlayerPrefs.GetInt("Idm");
            MenuInicial.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            ManagerGame.Instance.Idm = 1;
            switch(Application.systemLanguage)
            {
                case SystemLanguage.Portuguese:
                    ManagerGame.Instance.Idm = 0;
                    break;
                case SystemLanguage.English:
                    ManagerGame.Instance.Idm = 1;
                    break;
            }
            if (AlterarIdioma != null)
            {
                AlterarIdioma.alterarMenuInicial();
            }
        }          
    }
    public void SelecionarIdm(int idioma)
    {
        AlterarIdioma.AltIdm(idioma);
        Source.PlayOneShot(Clip);
        MenuInicial.SetActive(true);
        this.gameObject.SetActive(false);
    }
    
}
