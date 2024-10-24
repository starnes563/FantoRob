using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public Animator MyAnimator;

    // Start is called before the first frame update
    void Awake()
    {

        MyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame


    public void AnimacaoOn(string boolName)
    {
        MyAnimator.SetTrigger(boolName);
        
    }

    public void AnimacaoOFF(string boolName)
    {
        MyAnimator.SetBool(boolName, false);
        
    }
}
