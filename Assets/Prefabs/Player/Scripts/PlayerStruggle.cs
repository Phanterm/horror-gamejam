using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStruggle : MonoBehaviour
{
    //Gets targets damage function
    public PlayerHealth entity = null;
    public bool TriggeredByKeypress = false;

void OnTriggerStay2D(Collider2D entity) 

{
    //Gets player health (other)
    PlayerHealth other = entity.GetComponent<PlayerHealth>();

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
    }
}

    // Update is called once per frame
    void Update()
    {
    
    }
}
