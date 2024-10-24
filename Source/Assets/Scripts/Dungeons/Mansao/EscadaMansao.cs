using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscadaMansao : MonoBehaviour
{
    public float slideSpeed; // a velocidade com que a escada escorrega
    private bool isSliding; // se a escada está escorregando ou não
    public Sprite slidingSprite;
    public Sprite originalSprite;
    public SpriteRenderer SpriteRenderer;
    private Vector2 originalPosition;
    public AudioClip slideSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSliding)
        {
            transform.Translate(Vector2.down * slideSpeed * Time.deltaTime);            
        }
    }
   public void StartSliding()
    {
        isSliding = true;
        SpriteRenderer.sprite = slidingSprite;
        audioSource.PlayOneShot(slideSound);
    }
   public void StopSliding()
    {
        isSliding = false;
        SpriteRenderer.sprite = originalSprite;
        transform.position = originalPosition;
    }


}
