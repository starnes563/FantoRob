using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoConfirmaMenu : MonoBehaviour
{
    public GameObject ParentObject;
    public AudioSource Source;
    public AudioClip AudioClip;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf && Input.GetButtonDown("Fire1"))
        {
            ParentObject.SetActive(false);
            if(Source !=null)
            {
                Source.PlayOneShot(AudioClip);
            }
        }
    }
}