using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicInOutdoor : MonoBehaviour {

	public bool PlayerInDoorTrigger;

	public Animator RobotAnimator;
	public Animator DoorAnimator;
	public Collider NextLevelTrigger;
	public Collider DoorCollider;

    public Image fadeImage;
    public float fadeSpeed = 1.0f;

    private void Start()
    {
        fadeImage.CrossFadeAlpha(0, fadeSpeed, false);   
    }

    // Update is called once per frame
    void Update () {
		if (PlayerInDoorTrigger && RobotAnimator.GetBool("extended")) {
			DoorAnimator.SetBool("opened", true);
			NextLevelTrigger.enabled = true;
			DoorCollider.transform.position = new Vector3(1000, 0, 0);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			PlayerInDoorTrigger = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player")) {
			PlayerInDoorTrigger = false;
		}
	}

}
