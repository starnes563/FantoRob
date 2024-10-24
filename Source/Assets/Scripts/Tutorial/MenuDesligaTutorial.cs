using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDesligaTutorial : MonoBehaviour
{
    public Diretor Diretor;
    public void Clicar(bool tutorial)
    {
        DesligaTutorial.Tutorial = tutorial;
        StoryEvents.TutorialPrimeiraMissao = false;
        StoryEvents.TutorialMarcus = tutorial;
        ManagerGame.Instance.SceneToLoad = 1;
        Diretor.TrocarACena();
    }
}
