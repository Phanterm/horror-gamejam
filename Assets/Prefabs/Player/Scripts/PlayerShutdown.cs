using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShutdown : MonoBehaviour
{
    #region VR Shutdown
    public PlayerMain player;
    public Animator animator;
    public LevelLoader levelLoader;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<PlayerMain>();
        animator = gameObject.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            player.FreezePlayer();
            StartCoroutine("ShutdownCo");
        }
    }

    public IEnumerator ShutdownCo()
    {
        FindObjectOfType<AudioManager>().Play("VrDisconnect");
        animator.SetBool("disconnecting", true);
        yield return new WaitForSeconds(8.5f);
        FindObjectOfType<AudioManager>().Play("VrExit");
        FinishShutdown();
    }

    public void FinishShutdown()
    {
        StopCoroutine("ShutdownCo");
        player.UnfreezePlayer();
        levelLoader.LoadNextLevel();
    }
}
