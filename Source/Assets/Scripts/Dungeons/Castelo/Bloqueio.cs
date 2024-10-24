using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloqueio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip SomArrastando;
    Rigidbody2D rb2D;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (rb2D.velocity.sqrMagnitude > 0 && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(SomArrastando);
        }
        if (rb2D.velocity.sqrMagnitude < 0.001f && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && StoryEvents.DesafiosCamp[3].Itemdesafio)
        {
            rb2D.constraints = RigidbodyConstraints2D.None;
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
