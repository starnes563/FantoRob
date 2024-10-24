using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{    
    public float backgroundMoveSpeed = 2f;
    public Transform backgroundTransform;
    public Transform CavaleiroDaCenoura;
    public PlayerController Player;

    private void FixedUpdate()
    {
      if(Player.MeuEstado == PlayerController.Estado.ANDANDO || Player.MeuEstado == PlayerController.Estado.PULANDO)
        {
            MoveBackground();
        }
    }

    private void MoveBackground()
    {
        backgroundTransform.Translate(Vector2.right * backgroundMoveSpeed * -1f * Time.deltaTime);
    }


}
