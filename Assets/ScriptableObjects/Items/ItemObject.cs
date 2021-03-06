﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Healing,
    Default
}

public abstract class ItemObject : ScriptableObject
{
    public Sprite icon;
    public GameObject prefab;
    public ItemType type;
    [TextArea(15,20)]
    public string description;

}