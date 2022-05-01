using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour {

 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            int s = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(s);
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            int s = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(s + 1);
        }
    }
}
