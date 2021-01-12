using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Upgrade_Confirm : MonoBehaviour
{
    public void Next_Level()
    {
        SceneManager.LoadScene("World 1-1", LoadSceneMode.Single);
    }
}
