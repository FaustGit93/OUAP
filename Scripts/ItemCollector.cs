using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public int coins = 0;

   [SerializeField] private Text coinsText;
   [SerializeField] private AudioSource coincollectedSoundEffect;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin")) 
        {

            Destroy(collision.gameObject);
            coins++;
            Debug.Log("Coins: " + coins);
            coinsText.text = "" + coins;
            GameData.playerCoins = coins;
            coincollectedSoundEffect.Play();

        }

    }

   


}
