using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Image actorImage;
    public Text actorName;
    public Text messageText;
    public RectTransform backgroundBox;
   // public AudioSource actorAudioSource;

    Message[] currentMessages;
    Actor[] currentActor;
    int activeMessage = 0;
    public static bool isActiveDialogue = false;


    public void OpenDialogue(Message[] messages, Actor[] actors) 
    {
        currentMessages = messages;
        currentActor = actors;
        activeMessage = 0;
        isActiveDialogue = true;
    //    actorAudioSource.Play();
        backgroundBox.LeanScale(Vector3.one, 0.3f);
        Debug.Log("First NPC dialogue" + messages.Length);
        DisplayMessage();

    }

    void DisplayMessage()
    {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;
        
        Actor actorToDisplay = currentActor[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;
      //  actorAudioSource.Play();
        AnimateTextColor();

    }
     
    public void NextMessage() 
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length) 
        {
            DisplayMessage();
        } else 
        {
       //     actorAudioSource.Stop();
            Debug.Log("Conversation Ended");
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            isActiveDialogue = false;
        }

    }


    void AnimateTextColor() 
    {
        LeanTween.textAlpha(messageText.rectTransform, 0, 0);
        LeanTween.textAlpha(messageText.rectTransform, 1, 0.5f);


    }

    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isActiveDialogue == true || (Gamepad.current.crossButton.wasPressedThisFrame && isActiveDialogue == true))
        {
            NextMessage();

        }
    }
}
