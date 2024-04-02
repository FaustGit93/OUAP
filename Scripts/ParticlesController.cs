using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [SerializeField] ParticleSystem movementParticles;

    [SerializeField] ParticleSystem fallParticles;

    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 1f)]
    [SerializeField] float dustFormationPeriod;

    [SerializeField] Rigidbody2D playerRb;
    [SerializeField] AudioSource walkSound;

    float counter;
    bool isOnGround;

    private void Update()
    {
        counter += Time.deltaTime;  

        if (isOnGround && Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                movementParticles.Play();
                Invoke("PlayWalkSound", 0.24f);

                    counter = 0;
            }
            else
            {
                movementParticles.Stop();
                //walkSound.Stop();
            }

        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            fallParticles.Play();
            isOnGround = true;
           
           

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isOnGround = false;
        }

    }
    void PlayWalkSound() 
    {
        walkSound.Play();
    }



}
