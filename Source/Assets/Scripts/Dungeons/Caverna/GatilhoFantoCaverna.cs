using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoFantoCaverna : MonoBehaviour
{
    public FantoMascaraFugitivo MeuNpc;
    bool podefalar = false;
    Walk Player;
    public int Animacao;
    [HideInInspector]
    public CaixaDialogo CaixaDialogo;
    // Start is called before the first frame update
    void Start()
    {
        CaixaDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (podefalar && Input.GetButtonDown("Fire1") && !CaixaDialogo.gameObject.activeSelf && !ManagerGame.Instance.EmBatalha && !ManagerGame.Instance.Transitando
           || podefalar && Input.GetButtonDown("Fire1") && !CaixaDialogo.gameObject.activeSelf && !ManagerGame.Instance.EmBatalha && !ManagerGame.Instance.Transitando)
        {
            Diretor.DesativarMenuPlayer();
            MeuNpc.Falar(Player);
            MeuNpc.actualCicle = Animacao;
            MeuNpc.moveCalc();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!MeuNpc.Persegue && other.tag == "Player" && other.GetComponent<Walk>().CanIWalk)
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
