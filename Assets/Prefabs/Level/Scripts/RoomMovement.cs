using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomMovement : MonoBehaviour
{
    protected CameraMovement cam;
    public Animator roomTransition;
    public Transform playerDestination;
    public RoomSettings roomSettings; //Holds the target room's data.
    public float transitionTime = 1f;
    public bool teleportOnPress;
    protected Collider2D target;
    public AudioManager audioManager;
    public UnityEvent activated;
    public UnityEvent deactivated;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        target = other;
        if (other.CompareTag("Player") && !teleportOnPress)
        {
            StartCoroutine("MapTransition");
            activated.Invoke();
        }
    }
    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        target = other;
        if (Input.GetButtonDown("Action"))
        { 
            if (other.CompareTag("Player") && teleportOnPress)
            {
                StartCoroutine("MapTransition");
                activated.Invoke();
            }
        }
    }

    public IEnumerator MapTransition()
    {
        roomTransition.SetTrigger("MapFadeOut");
        yield return new WaitForSeconds(transitionTime);
        Vector3 tempPosition = target.transform.position;

        cam.levelBounds = roomSettings.levelBounds;

        tempPosition.x = playerDestination.transform.position.x;
        tempPosition.y = playerDestination.transform.position.y;
        tempPosition.z = 1;

        target.transform.position = tempPosition;
        roomTransition.SetTrigger("MapFadeIn");
        deactivated.Invoke();
        StopCoroutine("MapTransition");
        //Wait for animation to stop playing.
    }
}
