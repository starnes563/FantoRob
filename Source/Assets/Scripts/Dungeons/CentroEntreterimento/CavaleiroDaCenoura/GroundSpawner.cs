using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    // 0 - meio 1 - esquerda 2 - direita
    public List<GameObject>terrainPrefab;
    Vector3 Background;
    public Transform TransformBack;
    public List<Vector3> Alturas = new List<Vector3>(3);
    int terrainCount = 10;
    int actualHeigh =0;
    int nextChange;
    bool hole;
    int holeSize;
    int holecount;
    float contador;
    public float ContadorGround;
    public PlayerController Player;
    private void Start()
    {
        Background = Alturas[0];
        contador = ContadorGround;
    }    
    private void FixedUpdate()
    {
        if (CavaleiroDaCenoura.instance.Iniciou)
        {
            if (Player.MeuEstado == PlayerController.Estado.ANDANDO || Player.MeuEstado == PlayerController.Estado.PULANDO)
            {
                contador += Time.deltaTime;
                if (contador >= ContadorGround)
                {
                    GenerateTerrain();
                    contador = 0f;
                }
            }
        }
    }

    public void GenerateTerrain()
    {
        if(!hole)
        {
            int tile = 0;
            if(terrainCount ==0)
            {
                tile = 1;
            }
            else if(terrainCount == nextChange)
            {
                tile = 2;
            }
            GroundTile g = Instantiate(terrainPrefab[tile], Background, Quaternion.identity, TransformBack).GetComponent<GroundTile>();
            g.GerenciadorChao = this;
            terrainCount++;
            if (terrainCount >= nextChange)
            {
                changeHeigh();
                terrainCount = 0;
                nextChange = Random.Range(5, 12);
                if (Random.Range(0, 101) > 90)
                {
                    hole = true;
                    holecount = 0;
                    holeSize = Random.Range(2, 4);
                }
            }
        }
        else
        {
            holecount++;
            if (holecount >= holeSize) { hole = false; }
        }
        
    }
    void changeHeigh()
    {
        if(Random.Range(0,101)>80)
        {
            switch(actualHeigh)
            {
                case 0:
                    Background = Alturas[1];
                    actualHeigh = 1;
                    break;
                case 1:
                    if (Random.Range(0, 101) > 50)
                    {
                        Background = Alturas[2];
                        actualHeigh = 2;
                    }
                    else
                    {
                        Background = Alturas[0];
                        actualHeigh = 3;
                    }
                        break;
                case 2:
                    if (Random.Range(0, 101) > 50)
                    {
                        Background = Alturas[1];
                        actualHeigh = 1;
                    }
                    else
                    {
                        Background = Alturas[0];
                        actualHeigh = 0;
                    }
                    break;
            }
           
        }

    }

}
