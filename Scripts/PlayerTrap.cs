using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
[RequireComponent(typeof(Interactor))]

public class PlayerTrap : MonoBehaviour


{
    private Rigidbody2D rb;
    private Animator anim;
    public Damageable damageable;
    // Start is called before the first frame update
  private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();    
        damageable = GetComponent<Damageable>();
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))

        {

             Die();
           
            damageable._currentHealth = damageable.MaxHealth-1;
           


        }
    }
    private void Die() 
    {
        rb.bodyType = RigidbodyType2D.Static;

        //anim.SetTrigger("death_respawn");
        Invoke("PlayHitAnimation",0.1f);
    }
    private void RestartLevel() 
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }


}
