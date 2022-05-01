using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTextController : MonoBehaviour {

	public TextManager TextManager;

	public float Delay = 1f;

	public float SecondsVisible = 5f;

	// Use this for initialization
	void Start () {
		StartCoroutine("run");
	}

	IEnumerator run() {
		yield return new WaitForSeconds(Delay);
		TextManager.StartDialogue(0, true);
		yield return new WaitForSeconds(SecondsVisible);
		TextManager.EndDialogue();
	}
}
