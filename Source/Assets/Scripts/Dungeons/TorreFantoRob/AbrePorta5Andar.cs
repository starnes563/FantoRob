using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbrePorta5Andar : MonoBehaviour
{
    public int MeuNumero;
    public Text Texto;
    public Portas5Andar GestorPortas;
    public SpriteRenderer Renderer;
    public Sprite Apertado;
    public Sprite Desapertado;
    public AudioSource Source;
    public AudioClip SomApertar;
    // Start is called before the first frame update
    void Start()
    {
        Texto.text = MeuNumero.ToString();
    }
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Renderer.sprite = Apertado;
            GestorPortas.Apertarbotao(MeuNumero);
            Source.PlayOneShot(SomApertar);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Renderer.sprite = Desapertado;
        }
    }
}
