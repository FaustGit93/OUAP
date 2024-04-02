using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Destroyable))]
public class DestroyableLife : MonoBehaviour
{
    [Header("Enemy Movement")]
    public float walkSpeed = 3f;
    public float walkStopRate = 0.1f;





    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    //PlayerController playerController;

    Animator animator;
    Destroyable damageable;

    private Vector2 walkDirectionVector = Vector2.right;

  


    public bool CanMove
    {

        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public float AttackCooldown
    {
        get
        {

            return animator.GetFloat(AnimationStrings.attackCooldown);

        }

        private set
        {
            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0));

        }
    }

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Destroyable>();

    }
    void Update()
    {

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;


        }

    }
    private void FixedUpdate()
    {
    
        if (!damageable.isHit)
        {
            if (CanMove)

                rb.velocity = new Vector2(walkSpeed * -walkDirectionVector.x, rb.velocity.y);

            else rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);





        }
        if (damageable.isHit)
        {
            if (CanMove)
                rb.velocity = new Vector2(walkSpeed * -walkDirectionVector.x, rb.velocity.y);


            else rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);





        }

        // change - o + based on the sprite used 

        //else rb.velocity = new Vector2(0, rb.velocity.y);

    }




    public void OnHit(int damage, Vector2 knockback)
    {


        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }

 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            //playerController.damageable._currentHealth.tag == damageable._currentHealth -= 1;
        }
    }

}
