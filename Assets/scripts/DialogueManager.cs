using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {


    public TextMeshProUGUI dialogueText;
    public Animator animator;
    private Queue<string> sentences;

    // Use this for initialization
    void Start () {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("itsopen", true);

        // Clear any old sentences from a previous conversation
        sentences.Clear();

        // Add the new sentences to the queue
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        // Show the first sentence immediately
        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        // Check if we ran out of sentences
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // Get the next sentence from the queue and print it
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
{
    dialogueText.text = "";
    foreach (char letter in sentence.ToCharArray())
    {
        dialogueText.text += letter;
        yield return null;
    }
}
    void EndDialogue()
    {
        animator.SetBool("itsopen", false);
    }

}