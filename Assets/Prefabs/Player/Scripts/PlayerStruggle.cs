using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStruggle : MonoBehaviour
{
    //Gets targets damage function
    public PlayerMain entity = null;
    private GameObject Player;
    public bool TriggeredByKeypress = false;

void OnTriggerStay2D(Collider2D entity) 

{
    //Gets player health (other)
    PlayerMain other = entity.GetComponent<PlayerMain>();

    if (TriggeredByKeypress == true)
    {
        if (Input.GetButtonDown("Action"))
        {
            Debug.Log("PlayerStruggle");   
        } 
    }
    else if (!TriggeredByKeypress)
    {
        Debug.Log("DyingSounds");  
        other.TakeDamage(10);
    }
}

}
