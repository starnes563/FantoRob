using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetaGPS : MonoBehaviour
{
    [HideInInspector]
    public List<Transform> PointsToLook = new List<Transform>();
    // Update is called once per frame
    void FixedUpdate()
    {
        if(PointsToLook.Count==1)
        {
            ApontarPara(PointsToLook[0]);
        }
        else if(PointsToLook.Count>1)
        {
            ApontarPara(PontoMaisProximo());
        }
    }
    void ApontarPara(Transform trans)
    {        
        Vector3 dir = trans.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg-90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    Transform PontoMaisProximo()
    {       
        Transform t = PointsToLook[0];
        float dis = 1000000;       
        foreach (Transform trans in PointsToLook)
        {
            float d = Vector2.Distance(trans.position, transform.position);
            if (d < dis) { t = trans; dis = d; }
        }       
        return t;
    }
    public void Ativar(Transform trans)
    {
        PointsToLook.Add(trans);
        this.gameObject.SetActive(true);
    }
}
