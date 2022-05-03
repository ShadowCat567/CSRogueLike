using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBeh : MonoBehaviour
{
    //unPause the game and hide the Pause panel
    public void UnPause(GameObject pauseObj)
    {
        pauseObj.SetActive(false);
        Time.timeScale = 1;
    }

    //exit the game
    public void ExitGame()
    {
        Application.Quit();
    }

    //go to the main menu scene
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //go to the game scene
    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }
}
