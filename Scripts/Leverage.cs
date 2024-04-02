using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leverage : MonoBehaviour
{
    public Transform doorTransform; // Riferimento al trasform dell'oggetto che si muove
    public Transform referenceObject; // Oggetto di riferimento per la posizione Y della porta
    public float openingSpeed = 2f; // Velocità di apertura della porta
    public float stopThreshold = 0.1f; // Soglia di distanza dalla posizione finale per fermare la porta
    public bool isDoorOpened = false; // Aggiungi una variabile per tenere traccia dello stato della porta
   

    void Start()
    {
        // Carica lo stato della porta se è già stata aperta in precedenza
        if (PlayerPrefs.HasKey("IsDoorOpened"))
        {
            isDoorOpened = PlayerPrefs.GetInt("IsDoorOpened") == 1;
            if (isDoorOpened)
            {
                OpenDoor();
            }
        }

        // Aggiungi il metodo OnApplicationQuit alla callback dell'evento Application
        Application.quitting += OnApplicationQuit;
    }

     private void OnTriggerEnter2D(Collider2D collision)
     {
         if (collision.CompareTag("Player"))
         {
             if (!isDoorOpened) // Controlla se la porta non è ancora stata aperta
             {
                 OpenDoor();
                 isDoorOpened = true;
                 // Salva lo stato della porta
                 PlayerPrefs.SetInt("IsDoorOpened", isDoorOpened ? 1 : 0);
                 PlayerPrefs.Save(); // Salva i dati in modo persistente
                
             }
         }
     }




    void OpenDoor()
    {
        StartCoroutine(OpenDoorCoroutine());
    }

    IEnumerator OpenDoorCoroutine()
    {
        float openYPosition = referenceObject.position.y; // Imposta la posizione Y di apertura sulla base della posizione Y dell'oggetto di riferimento

        while (Mathf.Abs(doorTransform.position.y - openYPosition) > stopThreshold)
        {
            float newY = Mathf.MoveTowards(doorTransform.position.y, openYPosition, openingSpeed * Time.deltaTime);
            doorTransform.position = new Vector3(doorTransform.position.x, newY, doorTransform.position.z);
            yield return null;
        }

        // Fissa la posizione Y della porta dopo l'apertura
        doorTransform.position = new Vector3(doorTransform.position.x, openYPosition, doorTransform.position.z);
    }

    void OnApplicationQuit()
    {
        // Reimposta lo stato delle porte aperte quando l'applicazione viene chiusa
        PlayerPrefs.DeleteKey("IsDoorOpened");
    }
}
