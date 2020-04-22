using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    protected float _walkTime = 0.1f;
    private Rigidbody2D _rigidBody;
    private Vector3 _change;
    private Animator _animator;
    private AudioSource _audio;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent <SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        _change = Vector3.zero;
        _change.x = Input.GetAxisRaw("Horizontal");
        _change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
        if(_change.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if(_change.x > 0)
        {
            _spriteRenderer.flipX = false;
        }

        //if (_animator.GetBool("moving") && _audio.isPlaying == false)
        //{
        //    _audio.Play();
       // }
    }

    void UpdateAnimationAndMove() //Sets values in our animator to animate our character accordingly.
    {
        if (_change != Vector3.zero)
        {
            _audio.pitch = Random.Range(0.8f, 1f);
            _audio.volume = 1;
            MoveCharacter();
            _animator.SetFloat("moveX", _change.x);
            _animator.SetFloat("moveY", _change.y);
            _animator.SetBool("moving", true);
        }
        else
        {
            _animator.SetBool("moving", false);
            _audio.volume = 0;
        }
    }

    void MoveCharacter()
    {
        _walkTime++;
        _rigidBody.MovePosition(
            transform.position + _change * speed * Time.deltaTime
            );
    }
}
