using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GenericEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent activated;
    private bool eventFinished = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Action") && !eventFinished)
        {
            EventActivate();
        }
    }

    private void EventActivate()
    {
        activated.Invoke();
    }
}
