using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBeh : MonoBehaviour
{
    public void UnPause(GameObject pauseObj)
    {
        pauseObj.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        //go to the first scene
        SceneManager.LoadScene(0);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }
}
