using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBeh : MonoBehaviour
{
    public void UnPause(GameObject pauseObj)
    {
        pauseObj.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Debug.Log("Exited game");
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        //go to the first scene
        Debug.Log("Went to main menu");
    }
}
