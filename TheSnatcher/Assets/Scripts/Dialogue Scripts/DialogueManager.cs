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
        //Is the player in the scene? If so, block movement temporarily 
        if(Player.player != null)
        {
            Player.player.AllowMovement(false);
        }
           

        //Clears all previous sentences 
        sentences.Clear();

        //Is text box assigned? 
        if(nameText != null)
        {
            nameText.text = dialogue.characterName;
           // Debug.Log("Starting Dialogue with " + dialogue.characterName);
        }
        
        //Adds the sentences from the dialogue to the Queue
        foreach(string s in dialogue.sentences)
        {
            sentences.Enqueue(s);
        }

        //to display sentences
        DisplayNextSentence();        
    }

    public void DisplayNextSentence()
    {
        text.CrossFadeAlpha(0, 0f, false); //for fade out text animation

        //chekcs if there are still sentences to show 
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        //gets the sentence from the Queue and assings it to text
        string currentSentence = sentences.Dequeue();
        text.text = currentSentence;

        //for fade in text animation
        text.CrossFadeAlpha(1, 1f, false); 
    }    
    public void EndDialogue()
    {
       // Debug.Log("Dialogue ended");
       //if statements for scene loading after First dialogue and thrid level 
        if (GameStateManager.m_GameState == GameStateManager.GAMESTATE.StartDialogue)
        {
            Debug.Log("Loading first level from Dialogue Manager");
            GameStateManager.FirstLevel();
        }
        else if (GameStateManager.m_GameState == GameStateManager.GAMESTATE.EndDialogue)
        {
            Debug.Log("Loading thrid level from Dialogue Manager");
            GameStateManager.PlayerWins();
        }
        else
        {
            gameObject.SetActive(false);
            Player.player.AllowMovement(true);
        }
           
    }
}