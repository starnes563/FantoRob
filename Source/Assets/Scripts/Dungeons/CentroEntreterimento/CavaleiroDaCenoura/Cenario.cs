using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cenario : MonoBehaviour
{
    public float backgroundMoveSpeed = 2f;
    public Transform backgroundTransform;
    public PlayerController Player;
    public Vector3 PosicaoRet;
    public Vector3 PosicaoBase;
    private void FixedUpdate()
    {
        if (Player.MeuEstado == PlayerController.Estado.ANDANDO || Player.MeuEstado == PlayerController.Estado.PULANDO)
        {
            MoveBackground();
        }
        if(backgroundTransform.localPosition.x <= PosicaoRet.x)
        {
            backgroundTransform.localPosition = PosicaoBase;
        }
    }

    private void MoveBackground()
    {
        backgroundTransform.Translate(Vector2.right * backgroundMoveSpeed * -1f * Time.deltaTime);
    }
}
