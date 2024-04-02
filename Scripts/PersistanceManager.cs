using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistanceManager : MonoBehaviour
{
    // Start is called before the first frame update
    public PersistanceManager instance;
    [SerializeField] public int maxHealth;
    public int currentHealth;
    public void Awake()
    {
        if (instance != null)
        {

            Destroy(gameObject);
            return;
        }
        instance = this;
        currentHealth = maxHealth;
        DontDestroyOnLoad(gameObject);
        

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public int RestoreCurrentHealth()

    {
        Debug.Log("Restore current Health");
        return currentHealth;



    }

    internal void SetCurrentHealth(int currentHealth)
    {
       this.currentHealth = currentHealth;
    }
}
