using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelBack : MonoBehaviour
{
    private AudioSource changeLevelSound;

    private bool hadEnterDoor = false;

    public bool isNextScene = true;

    //public GameObject Flag;
    public GameObject Flag_back;

    public Animator transition;
    public float transitionTime =  3f;



    [SerializeField]
    public SceneInfo sceneInfo;


    private void Start()
    {

        changeLevelSound = GetComponent<AudioSource>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !hadEnterDoor)
        {
            changeLevelSound.Play();
            hadEnterDoor = true;
            sceneInfo.isNextScene = isNextScene;
            changeLevel();

        }
    }

    private void changeLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));



    }
    IEnumerator LoadLevel(int levelIndex)

    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

}

