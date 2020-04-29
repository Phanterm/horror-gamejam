using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMain : MonoBehaviour
{

    //PlayerMovement

    public float walkSpeed;
    public float sprintSpeed;
    private Rigidbody2D _rigidBody;
    private Vector3 _change;
    private Animator _animator;
    private AudioSource _audio;
    private SpriteRenderer _spriteRenderer;

    //PlayerHealth
    public float healthBar;
    public float health;
    private float currentHealth; 


    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent <SpriteRenderer>();

        health = 100;
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        _change = Vector3.zero;
        _change.x = Input.GetAxisRaw("Horizontal");
        _change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
        if (_change.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if(_change.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
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
        }
        else
        {
            _animator.SetBool("walking", false);
            _animator.SetBool("sprinting", false);
            _audio.volume = 0;
        }
    }

    void MoveCharacter()
    {
        if (Input.GetButton("Action"))
        {
            _animator.SetBool("walking", false);
            _animator.SetBool("sprinting", true);
            _rigidBody.MovePosition(
            transform.position + _change * sprintSpeed * Time.deltaTime);
        }
        else
        {
            _animator.SetBool("walking", true);
            _animator.SetBool("sprinting", false);
            _rigidBody.MovePosition(
            transform.position + _change * walkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar = currentHealth; 
        
    if(currentHealth <= 0)
        {
            Debug.Log("You're Dead!");  
            currentHealth = 0;
        }
    }
}
