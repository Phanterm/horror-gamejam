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
        anim = animator.runtimeAnimatorController;
        animator.runtimeAnimatorController = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(director.state != PlayState.Playing && !_fix)
        {
            _fix = true;
            animator.runtimeAnimatorController = anim;
        }
    }

    public void RecedeAnimator()
    {
        player.animator.runtimeAnimatorController = anim;
        director.ClearGenericBinding(this.gameObject);
        animator.runtimeAnimatorController = null;
        animator.runtimeAnimatorController = anim;
    }
}
