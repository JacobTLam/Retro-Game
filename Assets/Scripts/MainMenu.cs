using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string lvToLoad;

    public int dfltLives;

    private void start()
    {
        Debug.Log(dfltLives);
        dfltLives = 3;
        PlayerPrefs.SetInt("CurrentLives", dfltLives);
        
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(lvToLoad);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
