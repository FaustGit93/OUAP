using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{

    public Camera cam;
    public Transform followTarget;
    // Start is called before the first frame update
    // starting position for the parallax obj 
    Vector2 startingPosition;
    // starting Z Value of the parallax obj
    float startingZ;

    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;
    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;


    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.localPosition.z;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3 (newPosition.x, newPosition.y, startingZ);

        DontDestroyOnLoad(gameObject);
    }
}
