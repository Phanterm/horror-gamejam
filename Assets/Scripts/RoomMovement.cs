using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMovement : MonoBehaviour
{
    public Vector2 cameraShift;
    public Vector3 playerShift;
    private CameraMovement cam;
    public Transform teleportTarget;
    
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
            cam.MinPosition += cameraShift;
            cam.MaxPosition += cameraShift;
            tempPosition.x = teleportTarget.transform.position.x;
            tempPosition.y = teleportTarget.transform.position.y;
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
