using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivaElevaCast : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {        
     if (other.tag == "Player" )
     {
       if(!StoryEvents.ElevadorCast){ StoryEvents.ElevadorCast = true; }
     }        
    }
}
