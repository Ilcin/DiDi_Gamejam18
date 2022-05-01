using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandBlockerTrigger : MonoBehaviour {

	public CommandExecutor CommandExecutor;

	public Command BlockedCommand;

	void OnTriggerEnter(Collider collider) {
		if (collider.CompareTag("blocking")) {
			CommandExecutor.IncrementBlockerCount(BlockedCommand);
		}
	}

	void OnTriggerExit(Collider collider) {
		if (collider.CompareTag("blocking")) {
			CommandExecutor.DecrementBlockerCount(BlockedCommand);
		}
	}
}
