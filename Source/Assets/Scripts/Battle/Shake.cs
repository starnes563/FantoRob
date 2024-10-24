using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private Animator meuAnimator;
    // Start is called before the first frame update
    void Start()
    {
        meuAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void MechidaFraca()
    {
        meuAnimator.SetTrigger("shake1");
    }
    public void MechidaForte()
    {
        meuAnimator.SetTrigger("shake2");
    }
}
