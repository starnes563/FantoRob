using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IniciarCena : MonoBehaviour
{
    public Diretor Diretor;
    public Animator Animator;
    private Scene scene;
    private void OnEnable()
    {
        Animator.speed = 0;
        scene = SceneManager.GetActiveScene();
    }
    private void Update()
    {
        if(scene.isLoaded)
        {
            Animator.speed = 1;
        }
    }
    public void Destroy()
    {       
        Destroy(gameObject);
    }
    public void PodeIniciar()
    {
        Diretor.PodeIniciar = true;
    }


}
