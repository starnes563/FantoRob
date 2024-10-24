using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoCutEs1 : GatilhoCutscene
{         
     void Update()
    {
        if (PlayerObjects.Missões != null && PlayerObjects.Missões.Count > 0)
       {
            if (!mostrou && PlayerObjects.Missões[0].Completo && !ManagerGame.Instance.EmBatalha)
            {
                Iniciar();
            }
         }
    }
  
}
