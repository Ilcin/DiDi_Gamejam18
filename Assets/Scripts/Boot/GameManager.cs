using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private TextManager textManager;
    private CommandInput input;
    private bool starting;

    public float waitTime = 3.0f;

    private Command? nextCommand;

    private int state;

    public Text helpText;
    public Text mainText;

    // Use this for initialization
    void Start () {
        textManager = GetComponent<TextManager>();
        input = GetComponent<CommandInput>();
        input.commandQueue = GetComponent<CommandQueue>();
        state = 0;
        setupState();
        starting = true;
	}

    void nextScene()
    {
        StopAllCoroutines();
        SceneManager.LoadScene("Outside");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma))
            nextScene();

        readInputCommand();
        if (starting)
            firstScene();
    }

    private void firstScene()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown("s"))
            {
                state = 10; //you won
                setupState();
                starting = false;
            }
            else if(state < 9)
            {
                state++;
                setupState();
            }
        }
    }

    private void readInputCommand()
    {
        nextCommand = input.commandQueue.Next();
        if (nextCommand != null && nextCommand.HasValue)
        {
            switch (nextCommand)
            {
                case Command.MOVE_FORWARD:
                    if (state == 13 && textManager.nextPossible)
                    { textManager.DisplayNextSentence(false); state++; }
                    break;
                case Command.MOVE_BACKWARD:
                    if (state == 14 && textManager.nextPossible)
                    { textManager.DisplayNextSentence(false); state++; }
                    break;
                case Command.ROTATE_LEFT:
                    if (state == 15 && textManager.nextPossible)
                    { textManager.DisplayNextSentence(false); state++; }
                    break;
                case Command.ROTATE_RIGHT:
                    if (state == 16 && textManager.nextPossible)
                    { textManager.DisplayNextSentence(false); state++; setupState(); }
                    break;

                case Command.ARM_EXETEND:
                    if (state == 19 && textManager.nextPossible)
                    { textManager.DisplayNextSentence(false); state++; }
                    break;
                case Command.ARM_SHRINK:
                    if (state == 20 && textManager.nextPossible)
                    { textManager.DisplayNextSentence(false); state++; }
                    break;
                case Command.HAND_CLOSE:
                    if (state == 21 && textManager.nextPossible)
                    { textManager.DisplayNextSentence(false); state++; }
                    break;
                case Command.HAND_OPEN:
                    if (state == 22 && textManager.nextPossible)
                    { textManager.DisplayNextSentence(false); state++; }
                    break;
                case Command.ARM_LOWER:
                    if (state == 23 && textManager.nextPossible)
                    { textManager.DisplayNextSentence(false); state++; }
                    break;
                case Command.ARM_RAISE:
                    if (state == 24 && textManager.nextPossible)
                    { textManager.DisplayNextSentence(false); state++; setupState(); }
                    break;
            }
        }
    }

    private void setupState()
    {
        switch(state){
            case 0:
                textManager.SetTextUI(helpText);
                break;
            case 3:
                textManager.StartDialogue(0, false);
                break;
            case 6:
                textManager.StartDialogue(1, false);
                break;
            case 9:
                textManager.StartDialogue(2, false);
                break;
            case 10: //correct input
                textManager.StartDialogue(3, false);
                StartCoroutine(waitForAFewSeconds());
                break;
            case 11: //i am booting
                helpText.text = "";
                textManager.SetTextUI(mainText);
                textManager.StartDialogue(4, true);
                textManager.addRandomSentence(100, "os successfully loaded ."); //add random shit
                StartCoroutine(playTheDialogue());
                break;
            case 12: //preparing walk boot
                textManager.StartDialogue(5, true);
                textManager.addRandomSentence(150, "locomotion boot requires test input ...");
                StartCoroutine(playTheDialogue());
                break;
            case 13: //press moving buttons
                textManager.StartDialogue(6, true);
                break;
            case 17:
                StartCoroutine(waitForAFewSeconds());
                break;
            case 18: //preparing arm boot
                textManager.StartDialogue(7, true);
                textManager.addRandomSentence(200, "grappling boot requires test input ...");
                StartCoroutine(playTheDialogue());
                break;
            case 19: //press moving buttons
                textManager.StartDialogue(8, true);
                break;
            case 25:
                StartCoroutine(waitForAFewSeconds());
                break;
            case 26: //final boot
                textManager.StartDialogue(9, true);
                textManager.addRandomSentence(100, "all systems fully functional !");
                StartCoroutine(playTheDialogue());
                break;
            case 27: //boot finished, setup connection
                textManager.StartDialogue(10, true);
                textManager.addRandomSentence(300, "connection to camera system established .");
                StartCoroutine(playTheDialogue());
                break;
            case 28: //connection complete
                textManager.StartDialogue(11, true);
                StartCoroutine(playTheDialogue());
                break;
            case 29: //just black screen
                mainText.text = "";
                StartCoroutine(waitForAFewSeconds());
                break;
            case 30:
                nextScene();
                break;
        }
    }

    IEnumerator waitForAFewSeconds()
    {
        yield return new WaitForSeconds(waitTime);
            state++;
            setupState();
    }

    IEnumerator playTheDialogue()
    {
        while (textManager.activeDialogue)
        {
            //wait for the current sentence being finished on screen
            while (!textManager.nextPossible)
                yield return null;
            yield return new WaitForSeconds(waitTime);
            textManager.DisplayNextSentence(false);
        }
        state++;
        setupState();
    }
}
