using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoSecreta : MonoBehaviour
{
    public SpriteRenderer Renderer;
    public Sprite BotaoApertado;
    public AudioSource Source;
    public AudioClip SomAperta;
    bool pode = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && pode)
        {
            pode = false;
            Renderer.sprite = BotaoApertado;
            Source.PlayOneShot(SomAperta);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {           
            pode = true;              
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            pode = false;
        }
    }
}
