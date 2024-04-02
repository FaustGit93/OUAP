using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TouchingDirections), typeof(Damageable))]
public class BetterJump : MonoBehaviour
{
    TouchingDirections touchingDirections;
    Rigidbody2D rb;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float jumpPower = 10f; // La potenza del salto
    public float maxJumpTime = 2f; // Durata massima del salto in secondi
    private float jumpTimeCounter = 0f; // Contatore del tempo di salto

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !IsJumpInputPressed())
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if (IsJumpInputPressed() && touchingDirections.IsGrounded)
        {
            if (jumpTimeCounter < maxJumpTime)
            {
                // Se il tempo di salto è inferiore al tempo massimo, continua a saltare
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpTimeCounter += Time.deltaTime;
            }
        }
        else
        {
            // Resetta il contatore del tempo di salto quando il giocatore smette di saltare
            jumpTimeCounter = 0f;
        }
    }

    bool IsJumpInputPressed()
    {
        // Verifica se l'input del gamepad o della tastiera è attivo per il salto
        // Verifica se l'input del gamepad o della tastiera è attivo per il salto
        float keyboardJumpInput = Input.GetButton("Jump") ? 1f : 0f;
        float gamepadJumpInput = Gamepad.current?.buttonEast.ReadValue() ?? 0f;
        return keyboardJumpInput > 0f || gamepadJumpInput > 0f;


        //float jumpInput = Input.GetButtonDown("Jump") ? 1f : Gamepad.current?.buttonEast.ReadValue() ?? 0f;
        //return jumpInput > 0f;
    }
}
