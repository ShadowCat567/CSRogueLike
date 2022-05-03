using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingGame : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //if the player runs into the exit, go to the victory scene
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(2);
        }
    }
}
