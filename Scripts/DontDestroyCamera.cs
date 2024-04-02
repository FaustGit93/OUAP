using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCamera : MonoBehaviour
{
    private static DontDestroyCamera instance;

    private void Awake()
    {
        // Cerca un'istanza esistente di DontDestroyCamera nella scena
        if (instance != null && instance != this)
        {
            // Se ne esiste già uno diverso da questo, distruggi questo oggetto
            Destroy(gameObject);
            return;
        }

        // Se non esiste un'istanza già presente o se l'istanza presente è questa, impostala come istanza corrente
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
