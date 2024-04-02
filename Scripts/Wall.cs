using UnityEngine;



public class Wall : MonoBehaviour
   
{
    // Salute iniziale del muro
    public int health = 100;
    Damageable damageable;
    // Funzione chiamata quando il muro viene attaccato


    public void TakeDamage(int attackDamage)
    {

        // Sottrai il danno alla salute del muro
        health -= attackDamage;

        // Controlla se la salute del muro è inferiore o uguale a zero
        if (health <= 0)
        {
            // Se la salute è <= 0, distruggi il muro
            DestroyWall();
        }
    }

    // Funzione per distruggere il muro
    void DestroyWall()
    {
        // Distruggi questo gameObject
        Destroy(gameObject);
    }
}
