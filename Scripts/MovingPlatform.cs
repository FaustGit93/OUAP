using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint; // Punto di partenza
    public Transform endPoint;   // Punto di arrivo
    public float speed = 1.0f;   // Velocità di movimento

    private float startTime;
    private float journeyLength;

    private bool movingToEndPoint = true; // Flag per indicare la direzione del movimento

    void Start()
    {
        // Calcola la distanza tra i due punti
        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
        startTime = Time.time;
    }

    void Update()
    {
        // Calcola il tempo trascorso dall'inizio del movimento
        float distCovered = (Time.time - startTime) * speed;

        // Calcola la frazione del percorso completato
        float fracJourney = distCovered / journeyLength;

        // Muove l'oggetto lungo il percorso tra i due punti
        if (movingToEndPoint)
        {
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fracJourney);
        }
        else
        {
            transform.position = Vector3.Lerp(endPoint.position, startPoint.position, fracJourney);
        }

        // Se l'oggetto ha raggiunto il punto di arrivo o di partenza, cambia direzione
        if (fracJourney >= 1.0f)
        {
            movingToEndPoint = !movingToEndPoint;
            startTime = Time.time;
        }
    }
}
