using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFantomascara : MonoBehaviour
{
    public bool Aleatorio;
    Animator meuAnimator;
    float contador;
    float proximaanim;
    // Start is called before the first frame update
    void Start()
    {
        meuAnimator = GetComponent<Animator>();
        contador = 0;
        proximaanim = Random.Range(2,6) ;
        if(Aleatorio) { AnimAleatoria(); }
    }

    // Update is called once per frame
    void Update()
    {
        if(Aleatorio)
        {
            contador += Time.deltaTime;
            if(contador >= proximaanim)
            {
                AnimAleatoria();
                contador = 0;
                proximaanim = Random.Range(2, 6);
            }
        }
    }
    void AnimAleatoria()
    {
        int anim = Random.Range(0, 4);
        switch (anim)
        {
            case 0:
                meuAnimator.Play("IdleFrente");
                break;
            case 1:
                meuAnimator.Play("IdleCostas");
                break;
            case 2:
                meuAnimator.Play("IdleDireita");
                break;
            case 3:
                meuAnimator.Play("IdleEsquerda");
                break;
        }
    }
}
