using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public PlayerMain player;
    public int requiredItemID;
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
        if (Input.GetButtonDown("Action"))
        {
            if (player.SearchInventoryByID(requiredItemID))
            {
                EventManager.current.UnlockGate(1);
            } 
        }
    }
    
}
