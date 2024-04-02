using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
[RequireComponent(typeof(Interactor))]

public class PlayerController : MonoBehaviour
{

    [Header("")]
    public PersistanceManager persistanceManager;
    public PlayerController instance;


    [Header("Movement Variables")]

    public float walkSpeed;
    public float runSpeed;
    public float airWalkSpeed;
    public float jumpImpulse;
    public float canCharge;

    [Header("Interactions & Spawns")]

    public Vector3 respawnPoint;
    //private string spawnPositionKey = "SpawnPosition";
    public Vector3 spawnPointNext;
    public Vector3 spawnPointBack;
    public Vector3 spawnPointDown;
    public Vector3 spawnPointUp;
    public GameObject fallDetector;

    [Header("Sound Effects")]

    [SerializeField] private AudioSource walkSoundEffect;
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource attackSoundEffect;
    [SerializeField] private AudioSource dashSoundEffect;
    [SerializeField] private AudioSource hitSoundEffect;
    [SerializeField] private AudioSource chargeSoundEffect;
    [SerializeField] private AudioSource fallSoundEffect;

    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;
    Rigidbody2D rb;
    Animator animator;


    [Header("Dash Variables")]

    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashPower;
    [SerializeField] float dashTime;
    [SerializeField] float dashCooldown;
    private TrailRenderer dashTrail;
    [SerializeField] float dashGravity;
    private float normalGravity;
    private float waitTime;


    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {


                        if (isRunning)
                        {

                            return runSpeed;

                        }
                        else
                        {
                            return walkSpeed;

                        }

                    }
                    // Air Control Checks
                    else
                    {
                        return airWalkSpeed;

                    }

                    // Idle speed is 0
                }
                else
                {

                    return 0;



                }


            }
            else
            {
                return 0;
            }
        }
    }




    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving
    {
        get
        {
            return _isMoving;

        }
        private set

        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);



        }
    }

    [SerializeField]
    private bool _isRunning = false;
    public bool isRunning
    {

        get
        {

            return _isRunning;
        }
        set
        {

            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }



    public bool _IsFacingRight = true;

    public bool IsFacingRight
    {
        get
        {

            return _IsFacingRight;

        }
        private

            set
        {
            if (_IsFacingRight != value)
            {
                //flip the local scale to make the player face the opposite direction

                transform.localScale *= new Vector2(-1, 1);

            }

            _IsFacingRight = value;

        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);


        }
    }






    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }


   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTrail = GetComponent<TrailRenderer>();
        normalGravity = rb.gravityScale;
        animator = GetComponent<Animator>();
        respawnPoint = rb.transform.position;
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
        if (persistanceManager.instance != null)

        {
            damageable._currentHealth = persistanceManager.RestoreCurrentHealth();
        }

        Debug.Log(damageable._currentHealth);


    }

    // Added for Dash Mechanic


    private void Start()
        {
        if (instance != null)
        {

            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

    }
    

    private void Update()
    {
        waitTime += Time.deltaTime;
          // Velocità di movimento della telecamera

  



}





    private void FixedUpdate()
    {
        // Dialogue System Implementation

        //   if(DialogueManager.isActiveDialogue == true)
        //      {
        //          return;
        //      }
        //    Debug.Log(moveInput.x);
        SetFacingDirection(moveInput);
        if (!damageable.isHit)
        {
            Movement();
        }
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
        DontDestroyOnLoad(fallDetector);
        //



        if (touchingDirections.IsGrounded || (touchingDirections.IsOnWall && moveInput.x != 0)) // && controller.IsFacingRight)) // || (IsOnWallRight && controller.IsFacingRight)) // (InputAction.CallbackContext context)) // && wallDistance > 0.01) //&& 
        {
            touchingDirections.canDoubleJump = true;
           

        }

    }

    private void Movement()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fall Detector")

        {
            
            transform.position = respawnPoint;
           
            damageable._currentHealth = damageable._currentHealth -= 1;
            
            fallSoundEffect.Play();
            Invoke("PlayHitAnimation", 0.25f);


        }

        //    if (collision.tag == "Enemy")
        //    {
        //        damageable.OnHit();
        //    }
        else if (collision.tag == "Checkpoint")
        {
            
            respawnPoint = transform.position;
            Debug.Log("Checkpoint");


        }
        Debug.Log("Trigger 2D");
        Debug.Log(collision.tag);


        if (collision.tag == "ChangeLevelBack")
        {
            Debug.Log("spawnPoint");
            Debug.Log(transform.position);
            Invoke("WaitForSpawnBack", 2f);
            
        }
        if (collision.tag == "ChangeLevelNext")
        {
            Debug.Log("spawnPoint");
            Debug.Log(transform.position);
            Invoke("WaitForSpawnNext", 2f);

        }
        if (collision.tag == "ChangeLevelDownNext")
        {
            Debug.Log("spawnPoint");
            Debug.Log(transform.position);
            Invoke("WaitForSpawnDownNext", 2f);

        }
        if (collision.tag == "ChangeLevelUpBack")
        {
            Debug.Log("spawnPoint");
            Debug.Log(transform.position);
            Invoke("WaitForSpawnUpBack", 2f);

        }
        if (collision.tag == "SavePoint")
        {
            // SAVE MECHANIC
            Debug.Log("Saving");
            damageable._currentHealth = damageable.MaxHealth;
            SavePlayer();




        }




    }

    public void WaitForSpawnNext()
    {
        transform.position = transform.position + spawnPointNext;
    }
    public void WaitForSpawnDownNext()
    {
        transform.position = transform.position + spawnPointDown;
        //TO DO, modify gravity when the player falls in a new area. NOT try to modify the player's normal or dash gravity


        //normalGravity = 0.05f;
    }
    public void WaitForSpawnBack()
    {
        transform.position = transform.position + spawnPointBack;
    }
    public void WaitForSpawnUpBack()
    {
        transform.position = transform.position + spawnPointUp;
    }


    public void PlayHitAnimation()
    {
        animator.Play("player_hit");
    }


    public void OnMove(InputAction.CallbackContext context)
    {

        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {

            IsMoving = moveInput != Vector2.zero;

            // walkSoundEffect.Play();


        }
        else
        {
            IsMoving = false;
            

        }



    }



    public void SetFacingDirection(Vector2 moveInput)
    {
        if (IsAlive) { 
        if (moveInput.x > 0 && !IsFacingRight)

        {

            //face the right
            IsFacingRight = true;


        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // face the left
            IsFacingRight = false;
        }
      }

    }



    public void OnRun(InputAction.CallbackContext context)


    {

        if (context.started)
        {
            isRunning = true;
        }
        else if (context.canceled)
        {
            isRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context) // and Double Jump

    {
        // To do, check if alive as well // Double Jump correct


        if ((context.started && CanMove && touchingDirections.IsGrounded) || (context.started && CanMove && touchingDirections.canDoubleJump))

        {

            if (!touchingDirections.IsGrounded && touchingDirections.canDoubleJump) //||)
            {
                // Limita la durata tra 0 e 1 secondo

                touchingDirections.canDoubleJump = false;
              

            }
            Debug.Log("Long pressed");
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            jumpSoundEffect.Play();


        }

    }


    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            if (waitTime >= dashCooldown)
            {
                waitTime = 0;
                Invoke("Dash", 0);
            
                
            }
        }

    }
    public void Dash()
    {
    
            canDash = false;
            isDashing = true;
            dashTrail.emitting = true;
            rb.gravityScale = dashGravity;
            animator.Play("player_dash");
            dashSoundEffect.Play();
            


            if (moveInput.x == 0)
            {
                rb.velocity = new Vector2(transform.localScale.x * dashPower, 0);

            }
        
        else
        {
            rb.velocity = new Vector2(moveInput.x * dashPower, 0);
        }
        Invoke("StopDash", dashTime);
    }
    public void StopDash()
    {
        if (IsAlive)
        {
            animator.Play("player_idle");
            canDash = true;
            isDashing = false;
            dashTrail.emitting = false;
            rb.gravityScale = normalGravity;

        }
    }



    public void OnAttack(InputAction.CallbackContext context)
    {
        if (IsAlive)
        {
            if (context.started && _isMoving == false)

            {

                animator.SetTrigger(AnimationStrings.attackTrigger);
                attackSoundEffect.Play();

            }
        }
    }
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (IsAlive)
        {

            if (context.started && _isMoving == false)// && touchingDirections.IsGrounded == true)
            {
                animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
                chargeSoundEffect.Play();
                //damageable._currentHealth = damageable._currentHealth + 1;
                //insert condition to block the player while charging


            }
            else if (context.canceled || _isMoving == true)

            {
               animator.Play("player_idle");
               chargeSoundEffect.Stop();


            }

        }
    } 

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
        hitSoundEffect.Play();


    }

    public void SavePlayer() 
    {

        SaveSystem.SavePlayer(this);

    }

  


}






