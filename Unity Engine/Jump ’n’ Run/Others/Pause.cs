using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    private bool pauseActivated = false;
    public bool pause = false;
    public Image Background;
    public Button PauseButton;
    public Button ContinueButton;
    public Button LeaveGameButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && pauseActivated == false)
        {
            PauseActivated();
            pause = true;
        }
        else if (Input.GetKeyUp(KeyCode.Escape) && pauseActivated == true)
        {
            pause = false;
            PauseDeactivated();
        }
    }

    //Pause aktiviert mit Esc
    void PauseActivated()
    {
        Time.timeScale = 0.0f;

        //Alle Buttons und Hintergrund aktivieren (ohne deren Funktion)
        PauseButtonText(true);
        PauseBackground(true);
        PauseContinue(true);
        PauseLeaveGame(true);

        pauseActivated = true;
    }

    //Pause deaktiviert durch erneutes drücken von Esc oder durch klicken auf Continue
    void PauseDeactivated()
    {
        Time.timeScale = 1.0f;

        //Alle Buttons und Hintergrund deaktivieren (ohne deren Funktion)
        PauseButtonText(false);
        PauseBackground(false);
        PauseContinue(false);
        PauseLeaveGame(false);

        pauseActivated = false;
    }

    //Anzeigen des Pause Buttons (Button hat keine Funktion)
    void PauseButtonText(bool status)
    {
        PauseButton.GetComponent<Button>().enabled = status;
        PauseButton.GetComponent<Image>().enabled = status;
        PauseButton.GetComponentInChildren<Text>().enabled = status;
    }

    //Anzeigen des Hintergrunds im Pausemenü
    void PauseBackground(bool status)
    {
        Background.enabled = status;
    }

    //Anzeigen des Continue Buttons
    void PauseContinue(bool status)
    {
        ContinueButton.GetComponent<Button>().enabled = status;
        ContinueButton.GetComponent<Image>().enabled = status;
        ContinueButton.GetComponentInChildren<Text>().enabled = status;
    }

    //Anzeigen des Leave Game Buttons
    void PauseLeaveGame(bool status)
    {
        LeaveGameButton.GetComponent<Button>().enabled = status;
        LeaveGameButton.GetComponent<Image>().enabled = status;
        LeaveGameButton.GetComponentInChildren<Text>().enabled = status;
    }

    //Wenn auf Continue geklickt wird, deaktiviere Pausemenü:
    public void ContinueGame()
    {
        PauseDeactivated();
    }

    //Wenn auf Leave Game geklickt wird, schließe Spiel:
    public void LeaveGame()
    {
        Application.Quit();
    }
}
