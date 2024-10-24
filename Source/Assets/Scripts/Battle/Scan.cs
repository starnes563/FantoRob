using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    [HideInInspector]
    public WeaponMethods MyWeapon;
    public AudioClip MeuSom;
    // Start is called before the first frame update
    void Start()
    {
        MyWeapon.animacaoexecutando = true;
        SomFantoRob.Instancia.PlayOneShot(MeuSom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy()
    {
        MyWeapon.animacaoexecutando = false;
        Destroy(gameObject);
    }
}
