using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMovement : MonoBehaviour
{
    private CameraMovement cam;
    public Transform playerDestination;
    public RoomSettings roomSettings; //Holds the target room's data.
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Vector3 tempPosition = other.transform.position;

            cam.levelBounds = roomSettings.levelBounds;
            
            tempPosition.x = playerDestination.transform.position.x;
            tempPosition.y = playerDestination.transform.position.y;
            tempPosition.z = 1;

            other.transform.position = tempPosition;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}
