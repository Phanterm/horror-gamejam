using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    public GameObject currentInterObj = null;


    void Update()
    {
        if(Input.GetButtonDown("Action") && currentInterObj)
        {
            //Do something with the object
            Debug.Log("Looks like a placeholder object.");
            currentInterObj.SendMessage("DoInteraction");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Interactable"))
        {
            currentInterObj = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Interactable")) 
        {
            if(other.gameObject == currentInterObj)
            {
                currentInterObj = null;
            }
        }
    }

}
