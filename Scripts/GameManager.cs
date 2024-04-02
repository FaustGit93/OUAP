// OLD GAME MANAGER
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : MonoBehaviour
{
    public static GameObject PlayerSpawner;
    public Transform playerSpawn;
    bool gameHasEnded = false;
    bool gamePaused = false;
    public float restartDelay = 1f;
 


public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void PauseGame()
    {
        if (gamePaused == false) 
        {
            gamePaused = true;
        }

    }
    /*public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;

            Debug.Log("Game Over!");
            Invoke("Restart", restatDelay);
        }
        
    }*/



   public void EndGame(Damageable damageable)
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("Game Over!");
            this.Invoke(() => Restart(damageable), restartDelay);
        }
    }

    public void Restart(Damageable damageable)
    {
        gameHasEnded = false;
        damageable.IsAlive = true;
        damageable._currentHealth = damageable.MaxHealth;
        //SceneManager.LoadScene(PlayerPrefs.GetString("SceneName"));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnApplicationQuit()
    {
        

        PlayerPrefs.Save();
    }
}



public static class Utility
{
    public static void Invoke(this MonoBehaviour mb, Action f, float delay)
    {
        mb.StartCoroutine(InvokeRoutine(f, delay));
    }

    private static IEnumerator InvokeRoutine(System.Action f, float delay)
    {
        yield return new WaitForSeconds(delay);
        f();
    }
}


