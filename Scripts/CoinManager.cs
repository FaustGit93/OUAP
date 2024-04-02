using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance; // Singleton per garantire un'unica istanza

    public int coins = 0;
    public Text coinsText; // Assicurati di assegnare il riferimento a questo Text nell'Editor Unity

    [SerializeField] private AudioSource coincollectedSoundEffect;

    private void Awake()
    {
        // Assicurati che ci sia solo un'istanza di CoinManager tra le scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCoinsText();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coins++;
            Debug.Log("Coins: " + coins);
            UpdateCoinsText();
            coincollectedSoundEffect.Play();
        }
    }

    private void UpdateCoinsText()
    {
        if (coinsText != null)
        {
            coinsText.text = coins.ToString();
        }
    }
}
