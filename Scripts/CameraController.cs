using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera; // Riferimento alla Virtual Camera

    private void Start()
    {
        // Ottenere il componente Cinemachine Virtual Camera
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        // Trova l'oggetto con il tag "Player" e impostalo come target della Virtual Camera
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            virtualCamera.Follow = player.transform;
        }
        else
        {
            Debug.LogError("Player non trovato nella scena! Assicurati di assegnare il tag 'Player' all'oggetto del giocatore.");
        }
    }
}
