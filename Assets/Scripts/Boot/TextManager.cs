using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://www.youtube.com/watch?v=_nRzoTzeyxU
public class TextManager : MonoBehaviour {
    private Queue<string> texts;
    public Text UItext;
    private string currentText;
    private Coroutine cour;
    private bool cherry;
    public static float waitTime;
    public bool nextPossible;

    public bool activeDialogue;

    [System.Serializable]
    public class Dialogue
    {
        [TextArea (3,10)]
        public string[] texts;
    }
    [SerializeField]
    public Dialogue[] dialogues;

    // Use this for initialization
    public TextManager() {
        texts = new Queue<string>();
	}

    public void SetTextUI(Text ui)
    {
        UItext = ui;
    }
	
	public void StartDialogue(int i, bool cherry)
    {
        activeDialogue = true;
        texts.Clear();

        foreach (string sentence in dialogues[i].texts)
        {
            texts.Enqueue(sentence);
        }
        this.cherry = cherry;
        nextPossible = true;
        DisplayNextSentence(true);
    }

    public void DisplayNextSentence(bool clearPrevious)
    {
        if (nextPossible)
        {
            if (texts.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = texts.Dequeue();
            if (cour != null)
                StopCoroutine(cour);
            cour = StartCoroutine(TypeSentence(sentence, cherry, clearPrevious));
            nextPossible = false;
        }
    }

    public void addRandomSentence(int length, string next)
    {
        string s = "" + '\n';
        for (int i = 0; i < length; i++)
            s += (char)Random.Range(32, 123);
        texts.Enqueue(s + '\n' + next);
    }

    IEnumerator TypeSentence(string sentence, bool cherry, bool clearText)
    {
        if (clearText)
            currentText = "";
        foreach(char letter in sentence.ToCharArray())
        {
            currentText += letter;
            UItext.text = currentText;
            if (cherry)
                UItext.text += " _";
            yield return new WaitForSeconds(waitTime);
        }
        nextPossible = true;
    }

    public void EndDialogue()
    {
        texts.Clear();
        UItext.text = "";
        currentText = "";
        nextPossible = true;
        activeDialogue = false;
    }
}
