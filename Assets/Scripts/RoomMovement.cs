using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMovement : MonoBehaviour
{
    public Vector2 cameraShift;
    public Vector3 playerShift;
    private CameraMovement cam;
    public RoomSettings targetRoom;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
        targetRoom = gameObject.GetComponent<RoomSettings>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            cam.MinPosition += cameraShift;
            cam.MaxPosition += cameraShift;
            other.transform.position += playerShift;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

        }
    }
}
