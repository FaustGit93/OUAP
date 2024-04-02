using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public Animator animator;
    [SerializeField] private AudioSource deathSoundEffect;
    //public PersistanceManager persistanceManager;
    [SerializeField]
    [Header("Health variables")]

    private int _maxHealth = 5;
    
    public int MaxHealth

    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    [SerializeField]

    private int _Currenthealth;
    public int _currentHealth
    {

        get
        {
            return _Currenthealth;
        }
        set
        {
            _Currenthealth = value;

            // if health is 0, character's death
            //if (_health <= 0)
            if (_Currenthealth <= 0)

            {

                IsAlive = false;
                deathSoundEffect.Play();

                //Destroy(gameObject,6f);

            }

        }
    }
    [SerializeField]
    public bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;

    public bool isHit
    {
        get
        {
            return animator.GetBool(AnimationStrings.isHit);
        }
        private set
        {

            animator.SetBool(AnimationStrings.isHit, value);


        }
    }

    private float timeSinceHit = 0;
    public float invicibilityTime = 0.25f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("Is Alive set" + value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _currentHealth = MaxHealth;

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invicibilityTime)
            // Removing invincibility
            {
                isInvincible = false;
                timeSinceHit = 0;

            }
            timeSinceHit += Time.deltaTime;
        }

    }
    // Start is called before the first frame update
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            _currentHealth -= damage;
            isInvincible = true;
            isHit = true;

            animator.SetTrigger(AnimationStrings.Hit);
            damageableHit?.Invoke(damage, knockback);
        
            return true;
        }
        else
        { // unable to be hit
            CancelInvoke();

        }
        return false;
    }
    public void Heal (int healthRestore) 
    {
        if(IsAlive) 
        {
           
            int maxHeal = Mathf.Max (MaxHealth - _currentHealth,0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            //_currentHealth += healthRestore;

            //_currentHealth += Mathf.Min(MaxHealth, healthRestore);
            //_currentHealth = Mathf.Min(_currentHealth + healthRestore, MaxHealth);
            _currentHealth += actualHeal;


        }

    }

}
