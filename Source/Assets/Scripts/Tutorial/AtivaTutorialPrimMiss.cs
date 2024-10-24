using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaTutorialPrimMiss : MonoBehaviour
{
    public void Ativar()
    {
        StoryEvents.DarPrimeiraMissao();
        if (StoryEvents.TutorialMarcus)
        {
            StoryEvents.TutorialMarcus = false;
            StoryEvents.TutorialPrimeiraMissao = true;
            StoryEvents.contTutPrimMiss = 0;
        }
        else
        {
            StoryEvents.TutorialMarcus = false;
            StoryEvents.TutorialPrimeiraMissao = false;
            StoryEvents.contTutPrimMiss = 0;
        }
    }
}
