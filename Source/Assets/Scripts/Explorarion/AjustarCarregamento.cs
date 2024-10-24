using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AjustarCarregamento : MonoBehaviour
{
    float l;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("Cinemachine"))
        {
            l = GameObject.FindWithTag("Cinemachine").GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize;
            this.transform.localScale = new Vector3(l * 0.002333f, l * 0.002333f, l * 0.002333f);
        }
    }

}
