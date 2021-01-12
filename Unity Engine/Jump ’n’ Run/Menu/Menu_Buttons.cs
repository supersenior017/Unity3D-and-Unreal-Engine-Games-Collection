using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Buttons : MonoBehaviour
{
    public void Start_Game()
    {
        //Start Game in World 1-1 and close all opened Scenes
        SceneManager.LoadScene("World 1-1", LoadSceneMode.Single);
    }

    public void Exit_Game()
    {
        //Exit Game
        Application.Quit();
    }
}

