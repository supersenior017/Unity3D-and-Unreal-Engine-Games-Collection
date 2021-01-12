using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public string SceneNameMainMenu;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneNameMainMenu, LoadSceneMode.Single);
    }
}
