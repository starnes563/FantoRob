using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAirsoft : MonoBehaviour
{
    public GameObject Bala;
    public Vector3 Culatra;
    public AudioSource Source;
    public AudioClip SomAtirar;
    float contador;
    public GerenciadorAirSoft MeuGerenciador;
    // Start is called before the first frame update
    void Start()
    {
        contador = 1;
    }

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if(MeuGerenciador.Jogou && Input.GetButtonDown("Fire1")&&contador>=0.5f)
        {
            Instantiate(Bala, this.transform);
            Source.PlayOneShot(SomAtirar);
            contador = 0;
            GetComponent<Animator>().SetTrigger("gatilho");
        }
    }
}
