using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Selection : MonoBehaviour
{
    public string SceneNameLevelSelection;
    public string SceneNameAchievements;
    public string SceneNameHighscores;
    public string SceneNameRemoveAds;

    public void LevelSelection()
    {
        SceneManager.LoadScene(SceneNameLevelSelection, LoadSceneMode.Single);
    }

    public void Achievements()
    {
        SceneManager.LoadScene(SceneNameAchievements, LoadSceneMode.Single);
    }

    public void Highscores()
    {
        SceneManager.LoadScene(SceneNameHighscores, LoadSceneMode.Single);
    }

    public void RemoveAds()
    {
        SceneManager.LoadScene(SceneNameRemoveAds, LoadSceneMode.Single);
    }
}
