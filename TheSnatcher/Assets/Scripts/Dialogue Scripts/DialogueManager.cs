using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    [SerializeField]
    private Animator dialogueBox;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private TextMeshProUGUI nameText;

    public void Awake()
    {
        sentences = new Queue<string>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {                      
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        if(nameText != null)
        {
            nameText.text = dialogue.characterName;
            Debug.Log("Starting Dialogue with " + dialogue.characterName);
        }
           
        foreach(string s in dialogue.sentences)
        {
            sentences.Enqueue(s);
        }

        DisplayNextSentence();        
    }

    public void DisplayNextSentence()
    {
        text.CrossFadeAlpha(0, 0f, false); //for fade out text animation

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string currentSentence = sentences.Dequeue();
        text.text = currentSentence; 

        text.CrossFadeAlpha(1, 1f, false); //for fade in text animation
    }    
    public void EndDialogue()
    {
        Debug.Log("Dialogue ended");
        gameObject.SetActive(false); 
    }
}
