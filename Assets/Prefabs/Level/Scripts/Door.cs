using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public int doorID;
    public AudioManager audioManager;

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
            audioManager.Play("SFX - Door Open");
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
    }
}
