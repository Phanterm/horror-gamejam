using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShutdown : MonoBehaviour
{
    public PlayerMain player;
    public Animator animator;
    public AudioClip shutdownSfx;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<PlayerMain>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator ShutdownCoroutine()
    {
        yield return new WaitForSeconds(5f);
    }
}
