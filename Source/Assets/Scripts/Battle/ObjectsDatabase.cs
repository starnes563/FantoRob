using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsDatabase : MonoBehaviour
{
    public List<GameObject> Itens = new List<GameObject>();    
    public List<GameObject> Weapons = new List<GameObject>();   
    public List<GameObject> PlayerRobots = new List<GameObject>();
    public List<GameObject> EnemyRobots = new List<GameObject>();

    void Awake()
    {
        if(ManagerGame.Instance.Item.Count>0)
        {
            foreach (GameObject item in ManagerGame.Instance.Item)
            {
                if (item != null)
                {
                    Itens.Add(item);
                }

            }
        }
        
    }
}
