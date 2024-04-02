using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    public int damage = 2;
    //public float moveSpeed = 3f;
    public Vector2 moveSpeed = new Vector2(3f, 0);
    public Vector2 knockback = new Vector2(0, 0);
    public GameObject Blood;
    [SerializeField] private AudioSource spellSoundEffect;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
           rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x,moveSpeed.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? -knockback : new Vector2(knockback.x, knockback.y);
            // Hit the target
            bool gotHit = damageable.Hit(damage, deliveredKnockback);
            Instantiate(Blood, transform.position, Quaternion.identity);
            if (gotHit)
                spellSoundEffect.Play();
            Debug.Log(collision.name + "hit for" + damage);

            //Destroy(gameObject);

        }
    }

}
