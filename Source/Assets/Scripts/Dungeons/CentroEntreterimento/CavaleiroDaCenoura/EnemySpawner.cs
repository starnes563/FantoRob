using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject slimePrefab;
    public GameObject batPrefab;
    public List<Vector2> SlimeSpawn;
    public List<Vector2> BatSpawn;
    public float spawnFrequency = 2f;
    private float spawnTimer;
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(CavaleiroDaCenoura.instance.Iniciou)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnFrequency)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }       
    }
    private void SpawnEnemy()    {
        
        // Randomizar qual inimigo será instanciado
        int inimigo = Random.Range(0, 2);
        int posicao = Random.Range(0, 2);
        switch(inimigo)
        {
            case 0:                                            
                Instantiate(slimePrefab, SlimeSpawn[posicao], Quaternion.identity, this.transform).GetComponent<EnemySlimeController>().playerController = playerController;
                break;
            case 1:               
                Instantiate(batPrefab, BatSpawn[posicao], Quaternion.identity, this.transform).GetComponent<EnemyBatController>().playerController = playerController;
                break;
        }        
        if (spawnFrequency > 0.75)
        {
            spawnFrequency -= 0.05f;
        }
    }
}
