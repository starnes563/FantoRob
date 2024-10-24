using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverMenu : MonoBehaviour
{
    public float VelocidadeMenu;
    // Start is called before the first frame update
    public void OnEnable()
    {
        this.transform.position = new Vector3(this.transform.position.x, -3.5f);
        LeanTween.moveLocalY(this.gameObject, 0f, VelocidadeMenu);
    }

  

}
