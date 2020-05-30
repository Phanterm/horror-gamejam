using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour //KeyEvent handles generic activation logic and fetching prerequisites for individual puzzles.
{
    public static EventManager current;

    private void Awake()
    {
        current = this;
    }

    public event System.Action<int> onTriggered;

    public void UnlockGate(int id)
    {
        onTriggered?.Invoke(id);
    }
}
