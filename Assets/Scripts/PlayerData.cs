using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public float health; //@TO-DO: Create an array for this info to account for different patient health amounts.
    //public float[] position; //We don't need this for now, as you always spawn in the same location, the nurse's office.

    public PlayerData (PlayerMain player)
    {
        health = player.health;

        //position = new float[3];
        //position[0] = player.transform.position.x;
        //position[1] = player.transform.position.y;
        //position[2] = player.transform.position.z;
    }

}
