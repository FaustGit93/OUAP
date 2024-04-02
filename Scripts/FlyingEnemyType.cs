using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyType : MonoBehaviour

{


    public float flightSpeed = 3f;
    public float waypointReachedDistance = 0.1f;
    public DetectionZone biteDetectionZone;
    public int damage;
    public List<Transform> waypoints;
    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;


    Transform nextWaypoint;
    int waypointNum = 0;

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            {
                _hasTarget = value;
                animator.SetBool(AnimationStrings.hasTarget, value);

            }

        }
    }
    public bool CanMove
    {

        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }


    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {

            if (CanMove)
            {
                Flight();
            } else
            {
                rb.velocity = Vector3.zero;
            }


        } else
        {
            //Dead flying enemy falls applies gravity in death momentum
            rb.gravityScale = 2f;

        }
    }

    private void Flight()
    {
        //Flying from one point to another
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        //Check if we have reached the waypoint already
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.velocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        if (distance <= waypointReachedDistance)
        {
            //Switch to next waypoint 
            waypointNum++;
            if (waypointNum >= waypoints.Count)
            {
                //Loop back
                waypointNum = 0;
            }
            nextWaypoint = waypoints[waypointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;
        if (transform.localScale.x > 0)
        //Facing right
        {
            if (rb.velocity.x < 0)
            // Flip
            {
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        } else
        //Facing left
        {
            if (rb.velocity.x > 0)
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {

            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null) 
            {
                player.OnHit(damage, Vector2.zero);
                //player.HealthDisplay._currentHealth =- 1;
            }

        }
    }

}
