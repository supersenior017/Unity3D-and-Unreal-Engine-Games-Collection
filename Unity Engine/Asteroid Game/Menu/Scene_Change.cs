using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Change : MonoBehaviour
{

  
        public void ChangeScene(string sceneName)
        {

        Time.timeScale = 1;

             SceneManager.LoadScene(sceneName);
        }



}
