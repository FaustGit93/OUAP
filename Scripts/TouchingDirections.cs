using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class TouchingDirections : MonoBehaviour
{

 public ContactFilter2D castFilter;
public float groundDistance; //0.05f;
    public float wallDistance;  // 0.2f;
    public float ceilingDistance; // 0.05f;
    public bool canDoubleJump = true;
    public bool canTripleJump = true;
    public bool canWallJump = true;

    CapsuleCollider2D touchingCol;
    PlayerController controller;
    Animator animator;


    RaycastHit2D [] groundHits = new RaycastHit2D[1];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];


    [SerializeField]
    private bool _isGrounded;

    public bool IsGrounded { get {
        
            return _isGrounded;


        }
        private
            
            set { _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);


        }

    }
    public void SetIsGrounded(bool isGrounded)  
    {
        IsGrounded = isGrounded;
        

    }


    [SerializeField]
    private bool _isOnWallRight;
    public bool IsOnWallRight
    { 
        get
        {

            return _isOnWallRight;

        }
        private

            set
        {
            _isOnWallRight = value;
            
            animator.SetBool(AnimationStrings.isOnWall, value);

        }



    }

    


    [SerializeField]
    private bool _isOnWall;

    public bool IsOnWall
    {
        get
        {

            return _isOnWall;

        }
        private

            set
        {
            _isOnWall = value;
           
            animator.SetBool(AnimationStrings.isOnWall, value);

        }

    }
    [SerializeField]
    private bool _isOnCeiling;

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnCeiling
    {
        get
        {

            return _isOnCeiling && _isGrounded;
            

        }
        private

            set
        {

           _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }

    }

    // Start is called before the first frame update
    private void Awake()
    {
           touchingCol = GetComponent<CapsuleCollider2D>(); 
            animator = GetComponent<Animator>();
    }

    // Update is called once per frame

    void FixedUpdate()
    {

       IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
      //IsOnWallLeft = touchingCol.Cast(Vector2.left, castFilter, wallHits, wallDistance) > 0;
      //IsOnWallRight = touchingCol.Cast(Vector2.right, castFilter, wallHits, wallDistance) > 0;
       IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
       



        }
      


    }
    

