using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDoor : Door
{
    public PlayerMain player;

    protected override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void UnlockGate(int id)
    {
        //Get the player's current inventory when UnlockGate is called. It will be checked for the correct item.
        base.UnlockGate(id);
    }
}
