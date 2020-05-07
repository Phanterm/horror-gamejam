using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMain : MonoBehaviour
{

    #region PlayerMovement
    public float walkSpeed;
    public float sprintSpeed;
    public bool playerIsImmobile = false;
    private Rigidbody2D _rigidBody;
    private Vector3 _change;
    private Animator _animator;
    #endregion

    [SerializeField]
    private Animator _healthAnimator;
    private AudioSource _audio;
    private SpriteRenderer _spriteRenderer;

    //PlayerHealth
    private float healthBar;
    public float health;
    protected float _currentHealth;

    //StruggleEvents
    public int strugglePressed = 0;
    public bool inStruggleEvent = false;


    //Player Inventory
    public InventoryObject inventory;

    public void OnTriggerEnter2D(Collider2D other) //When the player collides with an item they can pick up, add it to the inventory.
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }
    private void OnApplicationQuit() //When we quit play, reset the inventory to empty.
    {
        inventory.Container.Clear();
    }

    //Save Data
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }
    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        health = data.health;
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent <SpriteRenderer>();

        health = 100;
        _currentHealth = health;
        _healthAnimator.Play("HealthTree");
        _healthAnimator.SetFloat("healthReading", 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerIsImmobile)
        {
            _change = Vector3.zero;
            _change.x = Input.GetAxisRaw("Horizontal");
            _change.y = Input.GetAxisRaw("Vertical");
            UpdateAnimationAndMove();
            if (_change.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else if (_change.x > 0)
            {
                _spriteRenderer.flipX = false;
            }
        }

        if (Input.GetButtonDown("Action") && inStruggleEvent == true)
        {
            strugglePressed++;
            //Debug.Log(strugglePressed);
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

    /*public IEnumerator TakeDamageOverTimeCo(int damageAmount, float duration)
    {
        float amountDamaged = 0;
        float damagePerLoop = damageAmount / duration;
        while (amountDamaged < damageAmount)
        {
            health -= damagePerLoop;
            Debug.Log(health.ToString());
            amountDamaged += damagePerLoop;
            yield return new WaitForSeconds(5f);
        }
    }*/

    public IEnumerator TakeDamageOverTimeCo(int damageAmount)
    {
        inStruggleEvent = true;
        playerIsImmobile = true;
        while (strugglePressed < 25 && inStruggleEvent == true) //While the player has not yet pressed the struggle button enough times...
        {
            health -= damageAmount; //...take damage every loop in...
            Debug.Log($"{health.ToString()} {inStruggleEvent}");
            yield return new WaitForSeconds(0.25f); //...the amount of time between repeating loops.
        }
    }
}
