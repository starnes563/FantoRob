using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameOver : MonoBehaviour
{
    public AudioSource Source;
    public AudioClip SomConfirma;
    public void TocarSom()
    {
        Source.PlayOneShot(SomConfirma);
    }
    public void MenuInicial()
    {
        ManagerGame.Instance.SceneToLoad = 0;
        ManagerGame.Instance.LoadNextScene();
        this.gameObject.SetActive(false);
    }
    public void FecharJogo()
    {
        Application.Quit();
    }
    public void Reiniciar()
    {

    }

}
