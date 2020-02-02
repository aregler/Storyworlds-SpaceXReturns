using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager instance;
    public Text dialogueText;
    public Animator animator;

    private Queue<string> sentences;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        sentences = new Queue<string>();
    }

    public void onFadeOut() {
        displayNextSentence();
    }

    public void startDialogue(Dialogue dialogue) {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        displayNextSentence();
    }

    public void displayNextSentence() {
        if (sentences.Count == 0) {
            endDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void endDialogue() {
        sentences.Clear();
        animator.StopPlayback();
        dialogueText.text = "";
        animator.enabled = false;
    }
}
