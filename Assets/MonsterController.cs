using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform _target;
    public float walkSpeed;
    private Vector3 _change;
    private SpriteRenderer _spriteRenderer;
    private Animator _anim;
    protected Vector3 _lastPosition;

    #region Struggle
    public AudioManager audioManager;
    public AudioSource audioClip;
    public bool TriggeredByKeypress = false;
    public PlayerMain playerObject = null;
    private bool _monsterAttacking = false;
    public bool _playerFailed = false;
    public LevelLoader levelLoader;
    #endregion

    private bool _testMonster = false;

    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindWithTag("Player").transform;
        _anim = GetComponent<Animator>();
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetButtonDown("TestButton"))
        {
            _testMonster = true;
        }

        if (!_monsterAttacking && _testMonster)
        {
            MoveTowardsTarget();
        }

        var _currentPosition = transform.position; //Establishes the object's current position at beginning of each frame.     
        SetEnemyFacing();
        _lastPosition = _currentPosition; //Checks and sets the object's new position at the end of the frame.

        if (playerObject != null)
        {
            StruggleCheck();
        }

    }
    public void SetEnemyFacing()
    {
        // Calculate how much horizontal or vertical movement using |transform.position - lastPosition|.
        float horizontalMovement = transform.position.x - _lastPosition.x;
        float verticalMovement = transform.position.y - _lastPosition.y;
        _change.x = horizontalMovement;
        _change.y = verticalMovement;
        UpdateAnimationAndMove();
    }


        void UpdateAnimationAndMove() //Sets values in our animator to animate our character accordingly.
    {
        if (_change != Vector3.zero)
        {
            _anim.SetFloat("moveX", _change.x);
            _anim.SetFloat("moveY", _change.y);
        }
    }

    public void MoveTowardsTarget()
    {
        if (!_monsterAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, walkSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D entity)
    {
        if (entity.CompareTag("Player"))
        {
            //Gets player health (other)
            PlayerMain other = entity.GetComponent<PlayerMain>();
            playerObject = other;

            if (other.inStruggleEvent == false && !other.CheckPlayerFrozen())
            {
                other.FreezePlayer();
                audioManager.Stop("BGM");
                audioManager.Play("StruggleSFX");
                _monsterAttacking = true;
                other.TakeDamageOverTime(2);
                playerObject.spriteRenderer.enabled = false;
                _anim.SetTrigger("Attacking");
            }
        }
        else return;
    }
    private void StruggleCheck()
    {
        if (_playerFailed == true || playerObject.health == 0)
        {
            audioClip.Stop();
            playerObject.inStruggleEvent = false;
            playerObject.StopCoroutine("TakeDamageOverTimeCo");
            playerObject.strugglePressed = 0;
            levelLoader.LoadNameLevel("GameOver");
        }
        else if (!_playerFailed && playerObject.strugglePressed >= 25 && playerObject.inStruggleEvent == true)
        {
            audioClip.Stop();
            playerObject.inStruggleEvent = false;
            playerObject.UnfreezePlayer();
            playerObject.StopCoroutine("TakeDamageOverTimeCo");
            playerObject.strugglePressed = 0;
            Debug.Log("Struggle broken!");
            playerObject.audioSource.enabled = true;
            playerObject.spriteRenderer.enabled = true;
            playerObject = null;
            _monsterAttacking = false;
            gameObject.SetActive(false);
        }
    }


}
