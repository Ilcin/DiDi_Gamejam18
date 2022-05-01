using System.Collections.Generic;
using UnityEngine;

public class CommandQueue : MonoBehaviour {

    public Command? bufferedCommand { get; private set; }

    public void Queue(Command command) {
        if (bufferedCommand.HasValue) {
            return;
        }
        bufferedCommand = command;
    }

    public Command? Next() {
        var command = bufferedCommand;
        bufferedCommand = null;
        return command;
    }

}