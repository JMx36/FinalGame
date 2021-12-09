using UnityEngine;

//Emily Chavez
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    // Start is called before the first frame update
    public void Start()
    {
        //Finds the DialogueManager in the scene and passes the dialogue through StartDialogue
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
