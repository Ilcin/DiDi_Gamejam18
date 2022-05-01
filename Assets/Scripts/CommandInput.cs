using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandInput : MonoBehaviour {
    private static readonly Dictionary<KeyCode, Command> keyCodes = new Dictionary<KeyCode, Command>() {
        {KeyCode.W, Command.MOVE_FORWARD},
        {KeyCode.UpArrow, Command.MOVE_FORWARD},
        {KeyCode.S, Command.MOVE_BACKWARD},
        {KeyCode.DownArrow, Command.MOVE_BACKWARD},
        {KeyCode.A, Command.ROTATE_LEFT},
        {KeyCode.LeftArrow, Command.ROTATE_LEFT},
        {KeyCode.D, Command.ROTATE_RIGHT},
        {KeyCode.RightArrow, Command.ROTATE_RIGHT},
        {KeyCode.R, Command.ARM_RAISE},
        {KeyCode.L, Command.ARM_LOWER},
        {KeyCode.X, Command.ARM_EXETEND},
        {KeyCode.Y, Command.ARM_SHRINK },
        {KeyCode.G, Command.HAND_CLOSE },
        {KeyCode.H, Command.HAND_OPEN }

    };
    
    public CommandQueue commandQueue;

    void Update() {
        foreach (var it in keyCodes) {
            if (Input.GetKeyDown(it.Key)) {
                commandQueue.Queue(it.Value);
            }
        }
    }
}