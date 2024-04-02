// SPAWN DO NOT TOUCH
using System.ComponentModel.Design;
using UnityEngine;

public class NewGameSpawner : MonoBehaviour
{
    public GameObject playerToSpawn;
    public GameObject cameraToSpawn;
    //public ObjectSpawner instance;

    public Vector3 spawnPoint;

    //public StartMenu menu;
    void Start()
    {
        //GameObject.FindObjectOfType<StartMenu>();
        // Cerca l'oggetto nella scena per il suo nome
        PlayerData data = SaveSystem.LoadPlayer();

        GameObject existingObject = GameObject.Find(playerToSpawn.name);


        //spawnPoint = new Vector3(data.position[0], data.position[1], data.position[2]);

        // Se l'oggetto non è presente nella scena, lo istanzia
        if (existingObject == null)
        {

            Instantiate(playerToSpawn, spawnPoint, Quaternion.identity);

            Instantiate(cameraToSpawn, spawnPoint, Quaternion.identity);
        }

        else
        {
            Debug.Log("L'oggetto è già presente nella scena.");
        }
    }
}



