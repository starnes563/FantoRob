using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AjustarMenu : MonoBehaviour
{
    public CinemachineVirtualCamera Camera;
    public Vector3 Posicao;
    public Vector3 Escala;
    // Start is called before the first frame update
    void Start()
    {
        float l = Camera.m_Lens.OrthographicSize;                
        this.transform.localPosition = new Vector3(l*Posicao.x,l*Posicao.y,10f);
        this.transform.localScale= new Vector3(l*Escala.x, l*Escala.y, l * Escala.z);           
   }       
}
