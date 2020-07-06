using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorMovement : RoomMovement
{
    public Animator anim;
    public AudioSource sfx;

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
            StartCoroutine("DoorTransition");
        }
    }
    protected override void OnTriggerStay2D(Collider2D other)
    {
        target = other;
        if (Input.GetButtonDown("Action"))
        {
            if (other.CompareTag("Player") && teleportOnPress)
            {
                StartCoroutine("DoorTransition");
                activated.Invoke();
            }
        }
    }

    public IEnumerator DoorTransition()
    {
        sfx.Play();
        anim.SetBool("open", true);
        //Play our animation.
        new WaitForSeconds(10f);
        roomTransition.SetTrigger("MapFadeOut");
        yield return new WaitForSeconds(transitionTime);
        Vector3 tempPosition = target.transform.position;

        cam.levelBounds = roomSettings.levelBounds;

        tempPosition.x = playerDestination.transform.position.x;
        tempPosition.y = playerDestination.transform.position.y;
        tempPosition.z = 1;

        target.transform.position = tempPosition;
        roomTransition.SetTrigger("MapFadeIn");
        anim.SetBool("open", false);
        deactivated.Invoke();
        //StopCoroutine("MapTransition");
        //Wait for animation to stop playing.
    }
}
