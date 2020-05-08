using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineManager : MonoBehaviour
{
    private bool _fix = false;
    public Animator animator;
    public RuntimeAnimatorController anim;
    public PlayableDirector director;
    public PlayerMain player;

    // Start is called before the first frame update
    void OnEnable()
    {
        player.playerIsImmobile = true;
        anim = animator.runtimeAnimatorController;
        animator.runtimeAnimatorController = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(director.state != PlayState.Playing && !_fix)
        {
            animator.runtimeAnimatorController = anim;
            player.playerIsImmobile = false;
        }
    }
}
