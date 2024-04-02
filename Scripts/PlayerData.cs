using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public class PlayerData
{
    public int level;
    //public int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    // Variabile per memorizzare il nome della scena

    public int health;
    public float[] position;

    //public PlayerData (PlayerController player,StartMenu menu)
    public PlayerData(PlayerController player)

    {
        level = SceneManager.GetActiveScene().buildIndex;
        //insert damageable component for the health
        //health = damageable.player.health;
        

        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

    }

}
