using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoAmigo : MonoBehaviour
{
    public Amigo SeuAmigo;
    bool podefalar = false;
    Walk Player;
    public string Animacao;
    // Update is called once per frame
    void Update()
    {
        if (podefalar && Input.GetButtonDown("Fire1") && Player.CanIWalk)
        {
            SeuAmigo.Falar(Player);
            SeuAmigo.TocarAnimacao(Animacao);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player = other.GetComponent<Walk>();
            podefalar = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (podefalar) { podefalar = false; }
    }
}
