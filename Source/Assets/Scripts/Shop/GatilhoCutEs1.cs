using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatilhoCutEs1 : GatilhoCutscene
{         
     void Update()
    {
        if (PlayerObjects.Miss�es != null && PlayerObjects.Miss�es.Count > 0)
       {
            if (!mostrou && PlayerObjects.Miss�es[0].Completo && !ManagerGame.Instance.EmBatalha)
            {
                Iniciar();
            }
         }
    }
  
}
