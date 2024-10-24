using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XFX : MonoBehaviour
{
    public Color MinhaCor;
    public BattleManager BattleManager;
    public WeaponMethods WeaponMethod;
    public float Dano;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = MinhaCor;
    }

    // Update is called once per frame

    public void FinalizarAnimacao ()
    {
        BattleManager.ExecutarDano(1f, 0);
        Destroy(gameObject);
    }

}
