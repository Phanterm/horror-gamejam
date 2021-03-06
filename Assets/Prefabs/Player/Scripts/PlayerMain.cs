﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMain : MonoBehaviour
{
    #region Components
    [SerializeField]
    private Animator _healthAnimator;
    public AudioSource audioSource;
    public SpriteRenderer spriteRenderer;
    private GameObject newPlayer;
    #endregion
    #region PlayerMovement
    public float walkSpeed;
    public float sprintSpeed;
    public bool freezePlayer = false;
    private Rigidbody2D _rigidBody;
    private Vector3 _change;
    public Animator animator;
    #endregion
    #region Player Health
    private float healthBar;
    public float health;
    protected float _currentHealth;
    #endregion
    #region Struggle Interactions
    public int strugglePressed = 0;
    public bool inStruggleEvent = false;
    #endregion
    #region Player Inventory
    public InventoryObject inventory;

    public void TriggerMonster()
    {
        //Cause the monster to appear.
        Debug.Log("Boo");
    }

    public void OnTriggerEnter2D(Collider2D other) //When the player collides with an item they can pick up, add it to the inventory.
    {
        var item = other.GetComponent<Item>();
        var audio = other.GetComponent<AudioSource>();
        var renderer = other.GetComponent<SpriteRenderer>();
        var collider = other.GetComponent<BoxCollider2D>();

        if (item)
        {
            inventory.AddItem(item.item, 1);
            audio.PlayOneShot(audio.clip, 1f);
            renderer.enabled = !renderer.enabled;
            collider.enabled = !collider.enabled;
            Destroy(other.gameObject, 1.5f);
        }
    }
    private void OnApplicationQuit() //When we quit play, reset the inventory to empty.
    {
        inventory.Container.Clear();
    }
    #endregion
    #region Saving & Loading Data
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        inventory.SaveInventory();
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        inventory.LoadInventory();

        health = data.health;
        //Vector3 position;
        //position.x = data.position[0]; //Not necessary, as the player always loads/saves in the same spot. We can populate this later.
        //position.y = data.position[1];
        //position.z = data.position[2];
        //transform.position = position;
    }
    #endregion

    public bool SearchInventoryByID(int id)
    {
        foreach(InventorySlot item in inventory.Container)
        {
            if(id == item.ID)
            {
                return true;
            }
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent <SpriteRenderer>();
        newPlayer = this.gameObject;

        health = 100;
        _currentHealth = health;
        _healthAnimator.Play("HealthTree");
        _healthAnimator.SetFloat("healthReading", 100);
    }
    
  /*  public enum PlayerFacing
    {
        LEFT = 1,
        RIGHT = 2,
        UP = 3,
        DOWN = 4,
    }
    public bool SetPlayerFacing(PlayerFacing facing)
    {
        switch (facing)
        {
            case LEFT:
                return _change.x -= 1;
            case RIGHT:
                return _change.x += 1;
            case UP:
                return _change.y += 1;
            case DOWN:
                return _change.y -= 1;
        }
    }*/

    public bool CheckPlayerFrozen()
    {
        return freezePlayer;
    }

    public void FreezePlayer()
    {
        animator.SetBool("walking", false);
        animator.SetBool("sprinting", false);
        audioSource.volume = 0;
        _change = Vector2.zero;
        //@TO-DO: Add a custom logic here to disable player inventory while frozen.
        freezePlayer = true;
    }

    public void UnfreezePlayer()
    {
        //@TO-DO: Add a custom logic here to enable player inventory unfrozen.
        freezePlayer = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Start")) //@TO-DO: Move this to the appropriate save point object.
        {
            SavePlayer();
            Debug.Log("Game Saved!");
        }
        if (Input.GetButtonDown("Select"))
        {
            LoadPlayer();
            Debug.Log("Game Loaded!");
        }

        if (!CheckPlayerFrozen())
        {
            _change = Vector3.zero;
            _change.x = Input.GetAxisRaw("Horizontal");
            _change.y = Input.GetAxisRaw("Vertical");
            UpdateAnimationAndMove();
            if (_change.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (_change.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }

        if (Input.GetButtonDown("Action") && inStruggleEvent == true)
        {
            strugglePressed++;
        }


    }

    void UpdateAnimationAndMove() //Sets values in our animator to animate our character accordingly.
    {
        if (_change != Vector3.zero)
        {
            audioSource.pitch = Random.Range(0.8f, 1f);
            audioSource.volume = 1;
            MoveCharacter();
            animator.SetFloat("moveX", _change.x);
            animator.SetFloat("moveY", _change.y);
        }
        else
        {
            animator.SetBool("walking", false);
            animator.SetBool("sprinting", false);
            audioSource.volume = 0;
        }
    }

    void MoveCharacter()
    {
        if (Input.GetButton("Action"))
        {
            animator.SetBool("walking", false);
            animator.SetBool("sprinting", true);
            _rigidBody.MovePosition(
            transform.position + _change * sprintSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("walking", true);
            animator.SetBool("sprinting", false);
            _rigidBody.MovePosition(
            transform.position + _change * walkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        healthBar = _currentHealth; 
        
    if(_currentHealth <= 0)
        {
            Debug.Log("You're Dead!");  
            _currentHealth = 0;
        }
    }
    public void TakeDamageOverTime(int damageAmount)
    {
        StartCoroutine(TakeDamageOverTimeCo(damageAmount));
    }

    public IEnumerator TakeDamageOverTimeCo(int damageAmount)
    {
        inStruggleEvent = true;
        FreezePlayer();
        while (strugglePressed < 25 && inStruggleEvent == true) //While the player has not yet pressed the struggle button enough times...
        {
            health -= damageAmount; //...take damage every loop in...
            Debug.Log($"{health.ToString()} {inStruggleEvent}");
            yield return new WaitForSeconds(0.25f); //...the amount of time between repeating loops.
        }
    }
}
