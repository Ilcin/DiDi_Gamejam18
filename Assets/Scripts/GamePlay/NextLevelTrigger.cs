using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour {

    public string NextLevel;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player"))
        {
            SceneManager.LoadScene(NextLevel);
        }
    }



}
