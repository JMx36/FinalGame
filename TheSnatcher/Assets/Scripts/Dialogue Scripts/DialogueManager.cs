using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Emily Chavez
public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;

    [SerializeField]
    private Animator dialogueBox;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private TextMeshProUGUI nameText;

    private bool passed;

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
        //checks if there are still sentences to show 
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        //for fade out text animation
        text.CrossFadeAlpha(0, 0f, false);

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
            passed = true;
        }
        else if (GameStateManager.m_GameState == GameStateManager.GAMESTATE.EndDialogue)
        {
            Debug.Log("Loading Win Screen from Dialogue Manager");
            GameStateManager.PlayerWins();
            passed = true;
        }
        else if(!passed)
        {
            gameObject.SetActive(false);
            Player.player.AllowMovement(true);
        }
           
    }
}
