using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStruggle : MonoBehaviour
{
    //Gets targets damage function
    public PlayerMain entity = null;
    private GameObject Player;
    public AudioSource audioClip;

    public bool TriggeredByKeypress = false;
    public PlayerMain playerObject = null;
    

    void Update()
    {
        if (playerObject != null)
        {
            StruggleCheck();
        }
    }

    void OnTriggerStay2D(Collider2D entity)
    {
        //Gets player health (other)
        PlayerMain other = entity.GetComponent<PlayerMain>();
        playerObject = other;

        if (TriggeredByKeypress == true && other.inStruggleEvent == false)
        {
            if (Input.GetButtonDown("Action"))
            {
                other.TakeDamageOverTime(1);
                audioClip.Play();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D entity)
    { 
        //Gets player health (other)
        PlayerMain other = entity.GetComponent<PlayerMain>();
        playerObject = other;

        if (!TriggeredByKeypress && other.inStruggleEvent == false)
        {
            other.TakeDamageOverTime(1);
            audioClip.Play();
        }
    }
    private void StruggleCheck()
    {
        if (playerObject.strugglePressed >= 25 && playerObject.inStruggleEvent == true)
        {
            audioClip.Stop();
            playerObject.inStruggleEvent = false;
            playerObject.playerIsImmobile = false;
            playerObject.StopCoroutine("TakeDamageOverTimeCo");
            playerObject.strugglePressed = 0;
            playerObject = null;
            Debug.Log("Struggle broken!");
            gameObject.SetActive(false);
   
        }
    }
}
