using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Healing")]
public class HealingObject : ItemObject
{
    private float restoreHealthValue = 40f;
    public void Awake()
    {
        type = ItemType.Healing;
    }
}