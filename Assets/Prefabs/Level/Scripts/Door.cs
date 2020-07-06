using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int doorID;
    public AudioManager audioManager;
    public string doorSFX;

    protected virtual void Start()
    {
        EventManager.current.onTriggered += UnlockGate;
    }
    public virtual void Update()
    {
        
    }

    public virtual void UnlockGate(int id)
    {
        if (id == this.doorID)
        {
            audioManager.Play(doorSFX);
            this.gameObject.SetActive(false);
        }
    }
}
