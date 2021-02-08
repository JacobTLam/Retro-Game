using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Replay()
    {
        FindObjectOfType<GameManager>().Reset();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
