using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : Door
{
    public BoxCollider2D boxCollider;

    protected override void Start()
    {
        base.Start();
    }

    public override void UnlockGate(int id)
    {
        boxCollider.enabled = true;
        base.UnlockGate(id);
    }
}
