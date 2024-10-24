using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlteraDirVagoneta : MonoBehaviour
{
    public List<GameObject> ColliderAtivar;
    public List<GameObject> ColliderDesativar;
    public AudioSource Source;
    public AudioClip Clip;
    public SpriteRenderer Renderer;
    public Sprite Apertado;
    public Sprite Desapertado;
    // Start is called before the first frame update
   public void Alterar()
    {
        if(ColliderAtivar.Count>0)
        {
            foreach (GameObject gb in ColliderAtivar)
            {
                gb.SetActive(true);
            }
        }
        if (ColliderDesativar.Count > 0)
        {
            foreach (GameObject gb in ColliderDesativar)
            {
            gb.SetActive(false);
            }
        }
        Source.PlayOneShot(Clip);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Renderer.sprite = Apertado;
            Alterar();
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
