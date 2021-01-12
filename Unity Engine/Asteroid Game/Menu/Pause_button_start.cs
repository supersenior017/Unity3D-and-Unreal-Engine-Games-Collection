using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_button_start : MonoBehaviour
{

    public GameObject Pause;
    // Start is called before the first frame update
    void Start()
    {

        this.Pause.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public void ButtonPause_start()
    {
        if (Time.timeScale == 1.0f)
        {
            PauseGame();
            this.Pause.SetActive(true);
        }
    }

    public void ButtonPause_stop()
    {

        ResumeGame();
        this.Pause.SetActive(false);

    }

    public void ButtonRestart()
    {

        ResumeGame();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        
    }





    private void PauseGame()
    {
        Time.timeScale = 0;
        //Disable scripts that still work while timescale is set to 0
    }

    private void ResumeGame()
    {

        Time.timeScale = 1;

    }

}
