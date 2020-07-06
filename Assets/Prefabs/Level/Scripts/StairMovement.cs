using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StairMovement : RoomMovement
{
    public string soundName;
    public int startDelay;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !teleportOnPress)
        {
            activated.Invoke();
            StartCoroutine("StairTransition");
        }
    }
    protected override void OnTriggerStay2D(Collider2D other)
    {
        target = other;
        if (Input.GetButtonDown("Action"))
        {
            if (other.CompareTag("Player") && teleportOnPress)
            {
                activated.Invoke(); // Invoke the event, which freezes the player and starts the timeline cutscene.
                StartCoroutine("StairTransition");
            }
        }
    }

    public IEnumerator StairTransition()
    {
        audioManager.StartCoroutine(audioManager.FadeIn(soundName, 0.5f)); //Fade in the transition SFX.
        yield return new WaitForSeconds(startDelay); //Any user-added delay before the fade out occurs.
        roomTransition.SetTrigger("MapFadeOut"); //Triggers the transition animation.
        yield return new WaitForSeconds(transitionTime); //User-added delay after the transition itself. Useful for playing SFX in between rooms.
        Vector3 tempPosition = target.transform.position; //Define a new Vector3 for later.
            cam.levelBounds = roomSettings.levelBounds; //Apply the new camera bounds for the destination map.

            tempPosition.x = playerDestination.transform.position.x; //Apply to tempPosition the assigned destination coordinates.
            tempPosition.y = playerDestination.transform.position.y;
            tempPosition.z = 1;

            target.transform.position = tempPosition; //Teleport the player to the target destination with our new coordinates.
        audioManager.StartCoroutine(audioManager.FadeOut(soundName, 0.5f)); //Fade out the transition SFX.
        roomTransition.SetTrigger("MapFadeIn"); //Triggers the fade in transition animation.
        deactivated.Invoke(); //Invoke events such as unfreezing the player.
        StopCoroutine("MapTransition"); //Stop the current coroutine to wrap up.
    }
}
