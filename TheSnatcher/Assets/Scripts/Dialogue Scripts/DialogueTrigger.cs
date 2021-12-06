using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    // Start is called before the first frame update
    public void Start()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
