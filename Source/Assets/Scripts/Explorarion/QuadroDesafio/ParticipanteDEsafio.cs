using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticipanteDEsafio
{
    public Sprite SpriteParticipante;
    public string Nome;
    [HideInInspector]
    public int PosicaoAtual;
    [HideInInspector]
    public int PontuacaoAtual;
    public bool Aleatorio = true;
}
