using UnityEngine;

public class CameraControllerDown : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocità di movimento della telecamera
    public GameObject cam;
    void Update()
    {
        // Input del giocatore
        float verticalInput = Input.GetAxis("Vertical");

        // Sposta la telecamera verso il basso se la freccia giù viene tenuta premuta
        if (verticalInput < 0)
        {
            Vector3 newPosition = cam.transform.position + Vector3.down * moveSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }
}
