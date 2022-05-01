using System;
using System.Collections;
using UnityEngine;

public class CommandExecutor : MonoBehaviour
{

    private GrabItem grabItemScript;
    private Jump JumpScript;
    private Punch PunchScript;

    private AudioSource raiseAudio;
    private AudioSource shrinkArmAudio;
    private AudioSource fallSound;
    private AudioSource[] audios;
    public CommandQueue CommandQueue;

    public Animator RobotAnimator;

    public ScreenController screen;
    public float SecondsPerTile;

    public float SecondsPerQuarterRotation;

    public float? CurrentCommandStartTime { get; private set; }

    private uint[] commandBlockerCounts = new uint[Enum.GetValues(typeof(Command)).Length];

    void Update()
    {
        if (!CurrentCommandStartTime.HasValue)
        {
            StartCoroutine("execute");
        }
    }

    private void Start()
    {
        grabItemScript = GetComponent<GrabItem>();
        JumpScript = GetComponent<Jump>();
        PunchScript = GetComponent<Punch>();
        audios = GetComponents<AudioSource>();
        for (int i = 0; i < audios.Length; i++)
        {
            String x = audios[i].clip.name;
            switch (x)
            {
                case "ArmRiseLower": raiseAudio = audios[i];
                    break;
                case "ShrinkExtendArm": shrinkArmAudio = audios[i];
                    break;
                case "Bump": fallSound = audios[i];
                    break;
                default: break;

            }
        }
        raiseAudio.Stop();
        shrinkArmAudio.Stop();
        fallSound.Stop();
    }

    private IEnumerator execute()
    {
        Command? command;
        while ((command = CommandQueue.Next()).HasValue)
        {
            if (commandBlockerCounts[(int)command] > 0)
            {
                continue;
            }
            string coroutineName;
            object coroutineValue;
            switch (command)
            {
                case Command.MOVE_FORWARD:
                    coroutineName = "move";
                    coroutineValue = 1;
                    break;
                case Command.MOVE_BACKWARD:
                    coroutineName = "move";
                    coroutineValue = -1;
                    break;
                case Command.ROTATE_LEFT:
                    coroutineName = "rotate";
                    coroutineValue = -90f;
                    break;
                case Command.ROTATE_RIGHT:
                    coroutineName = "rotate";
                    coroutineValue = 90f;
                    break;
                case Command.ARM_RAISE:
                    coroutineName = "raise";
                    coroutineValue = 0;
                    break;
                case Command.ARM_LOWER:
                    coroutineName = "lower";
                    coroutineValue = 0;
                    break;
                case Command.HAND_CLOSE:
                    coroutineName = "grab";
                    coroutineValue = 0;
                    break;
                case Command.HAND_OPEN:
                    coroutineName = "release";
                    coroutineValue = 0;
                    break;
                case Command.ARM_EXETEND:
                    coroutineName = "extend";
                    coroutineValue = 0;
                    break;
                case Command.ARM_SHRINK:
                    coroutineName = "shrink";
                    coroutineValue = 0;
                    break;
                default: yield break;
            }
            CurrentCommandStartTime = Time.time;
            yield return StartCoroutine(coroutineName, coroutineValue);
            CurrentCommandStartTime = null;
        }
    }
    private IEnumerator raise(float val)
    {
        //Make Animation
        bool wasDown = RobotAnimator.GetBool("down");
        RobotAnimator.SetBool("down", false);
        //sound raise
        if (wasDown)
        {
            raiseAudio.Play();
        }
        yield return null;
    }

    private IEnumerator lower(float val)
    {
        //Animate
        bool wasDown = RobotAnimator.GetBool("down");
        RobotAnimator.SetBool("down", true);
        if (!wasDown)
        {
            raiseAudio.Play();
        }
        yield return null;
    }

    private IEnumerator extend(float val)
    {

        //Make Animation
        bool wasExtended = RobotAnimator.GetBool("extended");
        if (RobotAnimator.GetBool("extended") == false)
        {


            if (RobotAnimator.GetBool("down"))
            {
                StartCoroutine(Jump());
                screen.SetImage(1);
            }
            else
            {
                PunchScript.doPunch();
                screen.SetImage(2);
            }

            RobotAnimator.SetBool("extended", true);
            if (!wasExtended)
            {
                shrinkArmAudio.Play();
            }
        }

        yield return null;
    }

    private IEnumerator shrink(float val)
    {
        bool wasExtended = RobotAnimator.GetBool("extended");
        //Animate
        RobotAnimator.SetBool("extended", false);
        if (wasExtended)
        {
            shrinkArmAudio.Play();
        }
        yield return null;
    }

    private IEnumerator Jump()
    {
        //if Combo successful and not blocked move backwards
        if (JumpScript.doJump() && commandBlockerCounts[(int)Command.JUMP] == 0)
        {
            yield return new WaitForSeconds(.4f);

            float distance = -1;
            if (commandBlockerCounts[(int)Command.LONG_JUMP] == 0) // Jump two gridspaces when free
                distance = -2;

            CurrentCommandStartTime = Time.time;


            Vector3 initialPosition = transform.position;
            Vector3 targetPosition = initialPosition + (transform.forward * distance);
            while (transform.position != targetPosition)
            {
                if (!CurrentCommandStartTime.HasValue)
                {
                    yield break;
                }
                transform.position = Vector3.Lerp(initialPosition, targetPosition, (Time.time - CurrentCommandStartTime.Value) / SecondsPerTile);
                yield return null;
            }
            fallSound.Play();
            CurrentCommandStartTime = null;
        }
    }

    private IEnumerator move(float distance)
    {
        RobotAnimator.SetBool("moving", true);
        RobotAnimator.SetInteger("moveValue", (int)distance);
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = initialPosition + (transform.forward * distance);
        while (transform.position != targetPosition)
        {
            if (!CurrentCommandStartTime.HasValue)
            {
                yield break;
            }
            transform.position = Vector3.Lerp(initialPosition, targetPosition, (Time.time - CurrentCommandStartTime.Value) / SecondsPerTile);
            yield return null;
        }
        RobotAnimator.SetBool("moving", false);
    }

    private IEnumerator rotate(float angle)
    {
        RobotAnimator.SetBool("moving", true);
        RobotAnimator.SetInteger("moveValue", (int)angle);
        Quaternion initialRotation = transform.rotation.normalized;
        Quaternion targetRotation = (initialRotation * Quaternion.Euler(0, angle, 0)).normalized;
        while (transform.rotation != targetRotation)
        {
            if (!CurrentCommandStartTime.HasValue)
            {
                yield break;
            }
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, (Time.time - CurrentCommandStartTime.Value) / SecondsPerQuarterRotation).normalized;
            yield return null;
        }
        RobotAnimator.SetBool("moving", false);
    }

    private IEnumerator grab()
    {
        if (RobotAnimator.GetBool("grab") == false)
        {
            grabItemScript.grab();
            RobotAnimator.SetBool("grab", true);
        }
        yield return null;
    }

    private IEnumerator release()
    {
        if (RobotAnimator.GetBool("grab") == true)
        {
            RobotAnimator.SetBool("grab", false);
            grabItemScript.ungrab();
        }
        yield return null;
    }

    public void IncrementBlockerCount(Command command)
    {
        commandBlockerCounts[(int)command]++;
    }

    public void DecrementBlockerCount(Command command)
    {
        commandBlockerCounts[(int)command]--;
    }
}