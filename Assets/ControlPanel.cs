using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlPanel : MonoBehaviour
{
    [SerializeField]
    private UnityEvent activated;
    public PlayerMain player;
    public int requiredItemID;
    private bool eventFinished = false;
    /*
    NT_Shot = 0,
    Retrophone = 1,
    Ticket = 2,
    Loyalty_Voucher = 3,
    Bust_Head = 4,
    Blowtorch = 5,
    AI_Chip = 6,
    ORKA_Card = 7,
    Token_HID = 8,
    Crumpled_Paper = 9, 
    */
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
            Debug.Log("A strange power box with a round indentation on the front of the panel.");
            ControlPanelActivate();
        }

        else if (Input.GetButtonDown("Action"))
        {
            Debug.Log("Dialogue: It's on. You can hear electricity humming in the walls.");
        }
    }

    private void ControlPanelActivate()
    {
        if (player.SearchInventoryByID(requiredItemID))
        {
            Debug.Log("The strange token fits perfectly into the round indentation, and you hear the mall come alive.");
            activated.Invoke();
            //EventManager.current.UnlockGate(1);
        }

        else Debug.Log("Dialogue: Nothing happened. Maybe it's missing something...");
    }
    
}
