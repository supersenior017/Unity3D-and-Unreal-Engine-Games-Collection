using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPlanets : MonoBehaviour
{
    public GameObject Planet1;
    public GameObject Planet2;
    [Space(20)]
    public Button ButtonLeft;
    public Button ButtonRight;
    public Canvas Canvas;
    [Space(20)]
    public float speed;
    [Space(20)]
    public Text PlanetName;
    public Text PlanetHighscore;

    private float startTime;
    private float step;
    private Vector3 PosCanvas;
    private bool PressedButtonRight;
    private bool PressedButtonLeft;

    private float Planet1FinalPosX;
    private float distLengthPlanet1;
    private bool Planet1Moving;

    private float Planet2FinalPosX;
    private float distLengthPlanet2;
    private bool Planet2Moving;

    // Start is called before the first frame update
    void Start()
    {
        Planet1Moving = false;
        Planet2Moving = false;
        PosCanvas = Canvas.transform.position;
        step = Planet2.transform.position.x - PosCanvas.x;
        ButtonLeft.interactable = false;
        ButtonRight.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Planet1Moving == true && Planet2Moving == true)
        {
            ButtonLeft.interactable = false;
            ButtonRight.interactable = false;
        }
        else
        {
            if ((int)Planet1.transform.position.x == (int)Canvas.transform.position.x)
            {
                ButtonRight.interactable = true;
                PlanetName.text = "Erde";
            }

            if ((int)Planet2.transform.position.x == (int)Canvas.transform.position.x)
            {
                ButtonLeft.interactable = true;
                PlanetName.text = "Eisplanet";
            }
        }

        //oder mit ungefähr verlgiech if(Mathf.Approximately(Planet1.transform.position.x,Planet1FinalPosX) )
        if ((int)Planet1.transform.position.x == (int)Planet1FinalPosX && (int)Planet2.transform.position.x == (int)Planet2FinalPosX)
        {
            PressedButtonRight = false;
            PressedButtonLeft = false;
            Planet1Moving = false;
            Planet2Moving = false;
        }

        //Für Planet 1
        if (Planet1Moving == true)
        {
            Vector3 Planet1Destination = new Vector3(Planet1FinalPosX, Planet1.transform.position.y, Planet1.transform.position.z);

            float distCoveredPlanet1 = (Time.time - startTime) * speed;
            float journeyPlanet1 = distCoveredPlanet1 / distLengthPlanet1;
            Planet1.transform.position = Vector3.Lerp(Planet1.transform.position, Planet1Destination, journeyPlanet1);

            if (PressedButtonRight == true && (int)Planet1.transform.position.x <= (int)Planet1FinalPosX + 1)
            {
                Planet1.transform.position = Planet1Destination;
            }
            if (PressedButtonLeft == true && (int)Planet1.transform.position.x >= (int)Planet1FinalPosX - 1)
            {
                Planet1.transform.position = Planet1Destination;
            }
        }

        //Für Planet 2
        if (Planet2Moving == true)
        {
            Vector3 Planet2Destination = new Vector3(Planet2FinalPosX, Planet2.transform.position.y, Planet2.transform.position.z);

            float distCoveredPlanet2 = (Time.time - startTime) * speed;
            float journeyPlanet2 = distCoveredPlanet2 / distLengthPlanet2;
            Planet2.transform.position = Vector3.Lerp(Planet2.transform.position, Planet2Destination, journeyPlanet2);

            if (PressedButtonRight == true && (int)Planet2.transform.position.x <= (int)Planet2FinalPosX + 1)
            {
                Planet2.transform.position = Planet2Destination;
            }
            if (PressedButtonLeft == true && (int)Planet2.transform.position.x >= (int)Planet2FinalPosX - 1)
            {
                Planet2.transform.position = Planet2Destination;
            }
        }
    }

    public void ButtonRightClicked()
    {
        Planet1Moving = true;
        Planet2Moving = true;
        PressedButtonRight = true;

        //Bewege Planeten um 2000 nach links, wenn rechter button gedrückt
        Planet1FinalPosX = Planet1.transform.position.x - step;
        distLengthPlanet1 = Vector3.Distance(Planet1.transform.position, new Vector3(Planet1FinalPosX, Planet1.transform.position.y, Planet1.transform.position.z));

        Planet2FinalPosX = Planet2.transform.position.x - step;
        distLengthPlanet2 = Vector3.Distance(Planet2.transform.position, new Vector3(Planet2FinalPosX, Planet2.transform.position.y, Planet2.transform.position.z));

        startTime = Time.time;
    }

    public void ButtonLeftClicked()
    {
        Planet1Moving = true;
        Planet2Moving = true;
        PressedButtonLeft = true;

        //Bewege Planeten um 2000 nach rechts, wenn rechter button gedrückt
        Planet1FinalPosX = (int)Planet1.transform.position.x + step;


        distLengthPlanet1 = Vector3.Distance(Planet1.transform.position, new Vector3(Planet1FinalPosX, Planet1.transform.position.y, Planet1.transform.position.z));

        Planet2FinalPosX = Planet2.transform.position.x + step;
        distLengthPlanet2 = Vector3.Distance(Planet2.transform.position, new Vector3(Planet2FinalPosX, Planet2.transform.position.y, Planet2.transform.position.z));

        startTime = Time.time;
    }
}
