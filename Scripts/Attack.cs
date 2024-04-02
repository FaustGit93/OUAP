using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage;
    public Vector2 knockback = Vector2.zero;
    public GameObject Blood;
    //public Animator doorAnim;
    [SerializeField] private AudioSource hitSoundEffect;
    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null) 
        
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? -knockback : new Vector2(knockback.x, knockback.y);
            // Hit the target
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);
            Instantiate(Blood, transform.position, Quaternion.identity);
            if (gotHit)
               hitSoundEffect.Play();
            Debug.Log(collision.name + "hit for" + attackDamage);


           

        }
        /*if(collision.tag == "Leverage") 
        {
            doorAnim.Play("Opening_door1");
        }*/

    }
    void AttackWall(Collider2D wallCollider)
    {
        // Controlla se l'oggetto che il giocatore ha attaccato è un muro
        if (wallCollider.CompareTag("Wall"))
        {
            // Ottieni il componente Wall del muro
            Wall wall = wallCollider.GetComponent<Wall>();

            // Controlla se il componente Wall è stato trovato
            if (wall != null)
            {
                // Chiamare il metodo TakeDamage() del muro e passare il danno del giocatore
                wall.TakeDamage(attackDamage);
            }
        }
    }



}
