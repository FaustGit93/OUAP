using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Npc : MonoBehaviour
{
    [SerializeField] AudioSource NpcDialogueSound;
    public DialogueTrigger trigger;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") == true)
         trigger.StartDialogue();
        NpcDialogueSound.Play();    
    }

}
