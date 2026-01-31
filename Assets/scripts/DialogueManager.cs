using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    public TextMeshProUGUI dialogueText;
    public Animator animator;
    
    // --- NEW AUDIO VARIABLES ---
    public AudioSource audioSource;  // Drag the AudioSource component here
    public AudioClip typingSound;    // Drag your "blip" or "click" sound here
    public float typingSpeed = 0.05f; // Speed of typing (0.05 is standard)
    // ---------------------------

    private Queue<string> sentences;

    void Start () {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("itsopen", true);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

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

            // --- PLAY SOUND HERE ---
            // Only play sound if we have one, and maybe skip spaces
            if (!char.IsWhiteSpace(letter) && audioSource != null)
                {
                    audioSource.PlayOneShot(typingSound);
                }
            // -----------------------

            // Use WaitForSeconds instead of null so the sound has room to breathe
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        // 1. Stop the typing loop immediately
        StopAllCoroutines();

        // 2. Stop any sound currently playing
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        // 3. Close the animation
        animator.SetBool("itsopen", false);
        
        Debug.Log("End of conversation.");
    }
}