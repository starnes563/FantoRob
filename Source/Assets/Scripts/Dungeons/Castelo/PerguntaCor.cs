using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerguntaCor : MonoBehaviour
{
    public Dialogo Pergunta;
    public Dialogo Acerto;
    public Dialogo Errado;
    public bool TurnoComputador;
    public bool TurnoJogador;
    private CaixaDialogo caixaDialogo;
    public string cor;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        caixaDialogo = GameObject.FindWithTag("MainCamera").transform.GetChild(0).GetComponent<CaixaDialogo>();
    }
    IEnumerator Iniciar()
    {
        Walk p = GameObject.FindWithTag("Player").GetComponent<Walk>();
        p.PararDeAndar();
        TurnoComputador = true;
        caixaDialogo.ReceberDialogo(Pergunta);
        while(caixaDialogo.gameObject.activeSelf == true)
        {
            yield return null;
        }
        TurnoJogador = true;
        TurnoComputador = false;
        p.CanIWalk = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Acertou()
    {
        caixaDialogo.ReceberDialogo(Acerto);
        Lapis.RetirarLapis(cor);
    }
    public void Errou()
    {
        caixaDialogo.ReceberDialogo(Errado);
        Debug.Log("Atacou");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(coroutine != null) { StopCoroutine(coroutine); }
            coroutine = Iniciar();
            StartCoroutine(coroutine);
        }
    }
}
