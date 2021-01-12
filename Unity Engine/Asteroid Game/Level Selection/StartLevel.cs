using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartLevel : MonoBehaviour
{
    public GameObject Planet1;
    public GameObject Planet2;
    public Canvas Canvas;
    public string Planet1Name;
    public string Planet2Name;

    public void ButtonStart()
    {
        if((int)Planet1.transform.position.x == (int)Canvas.transform.position.x)
        {
            SceneManager.LoadScene(Planet1Name, LoadSceneMode.Single);
        }
        if ((int)Planet2.transform.position.x == (int)Canvas.transform.position.x)
        {
            SceneManager.LoadScene(Planet2Name, LoadSceneMode.Single);
        }
    }
}
