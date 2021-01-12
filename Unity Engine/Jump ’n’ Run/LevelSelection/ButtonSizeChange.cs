using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSizeChange : MonoBehaviour
{
    public float MovingSpeed = 5.0f;
    public float ScalingSpeed = 5.0f;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public Canvas can;
    public float WidthScaler = 3.0f;
    public float HeightScaler = 3.5f;
    public float SizeScaler = 5.0f;

    float distLeft;
    float distRight;
    float distTop;
    float distBot;
    float localsize;

    #region ButtonPressInit
    Vector2 button1_start_pos;
    public bool button1_activated = false;
    bool button1_moveback = false;
    bool button1_isScaling = false;
    public bool button1_finished = true;

    Vector2 button2_start_pos;
    bool button2_activated = false;
    bool button2_moveback = false;
    bool button2_isScaling = false;
    bool button2_finished = true;

    Vector2 button3_start_pos;
    bool button3_activated = false;
    bool button3_moveback = false;
    bool button3_isScaling = false;
    bool button3_finished = true;

    Vector2 button4_start_pos;
    bool button4_activated = false;
    bool button4_moveback = false;
    bool button4_isScaling = false;
    bool button4_finished = true;

    Vector2 button5_start_pos;
    bool button5_activated = false;
    bool button5_moveback = false;
    bool button5_isScaling = false;
    bool button5_finished = true;

    Vector2 button6_start_pos;
    bool button6_activated = false;
    bool button6_moveback = false;
    bool button6_isScaling = false;
    bool button6_finished = true;
    #endregion

    // Start is called before the first frame update
    void Start()
    {

        //Berechnen von Abständen/Größen/Positionen der Buttons
        RectTransform CanRectTransform = can.GetComponent<RectTransform>();
        RectTransform Button1RectTransform = button1.GetComponent<RectTransform>();
        RectTransform Button2RectTransform = button2.GetComponent<RectTransform>();
        RectTransform Button3RectTransform = button3.GetComponent<RectTransform>();
        RectTransform Button4RectTransform = button4.GetComponent<RectTransform>();
        RectTransform Button5RectTransform = button5.GetComponent<RectTransform>();
        RectTransform Button6RectTransform = button6.GetComponent<RectTransform>();

        #region CalculateDistance
        //Mitte von links/rechts
        float canMitte = can.transform.position.x;
        //Abstand zu linkem Rand
        distLeft = can.transform.position.x - (CanRectTransform.rect.width / WidthScaler);
        //Abstand zu rechtem Rand
        distRight = can.transform.position.x + (CanRectTransform.rect.width / WidthScaler);
        //Abstand zu oberem Rand
        distTop = can.transform.position.y + (CanRectTransform.rect.height / HeightScaler);
        //Abstand zu unterem Rand
        distBot = can.transform.position.y - (CanRectTransform.rect.height / HeightScaler);
        /*
         * Variablen zum weiterarbeiten:
         * canMitte
         * distLeft
         * distRight
         * distTop
         * distBot
        */
        #endregion
        #region CalculateSize
        float size = CanRectTransform.rect.size.x / SizeScaler;
        localsize = size / 300;
        #endregion
        //Startposition festlegen
        #region SetStartPositionOfButtons
        button1.transform.position = new Vector2(distLeft, distTop);
        button2.transform.position = new Vector2(distLeft, distBot);
        button3.transform.position = new Vector2(canMitte, distTop);
        button4.transform.position = new Vector2(canMitte, distBot);
        button5.transform.position = new Vector2(distRight, distTop);
        button6.transform.position = new Vector2(distRight, distBot);
        #endregion
        //Startskalierung festlegen
        #region SetStartScalingOfButtons
        button1.transform.localScale = new Vector2(localsize, localsize);
        button2.transform.localScale = new Vector2(localsize, localsize);
        button3.transform.localScale = new Vector2(localsize, localsize);
        button4.transform.localScale = new Vector2(localsize, localsize);
        button5.transform.localScale = new Vector2(localsize, localsize);
        button6.transform.localScale = new Vector2(localsize, localsize);
        #endregion

        button1_start_pos = button1.transform.position;
        button2_start_pos = button2.transform.position;
        button3_start_pos = button3.transform.position;
        button4_start_pos = button4.transform.position;
        button5_start_pos = button5.transform.position;
        button6_start_pos = button6.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

       
        #region Button1
        if (button1_activated == true)
        {
            button1_finished = false; //Button beginnt skalierung und ist dann beendet, wenn wieder in Startposition
            //Andere Buttons deaktivieren
            button2.SetActive(false);
            button3.SetActive(false);
            button4.SetActive(false);
            button5.SetActive(false);
            button6.SetActive(false);

            buttonscale(button1);

            //Erst wenn der Button komplett skaliert ist, kann der Benutzer den Button erneut klicken
            if(button1.GetComponent<Image>().transform.localScale.y >= localsize + 0.95f)
            {
                button1_isScaling = false;
            }
        }
        //Alles Rückgängig beim 2. klick auf den Button
        else if(button1_activated == false && button1_finished == false)
        {
            //Größe vom Button zurücksetzen
            button1.GetComponent<Image>().transform.localScale = Vector2.Lerp(button1.transform.localScale, new Vector2(localsize, localsize), ScalingSpeed * Time.deltaTime);
            button1.GetComponentInChildren<Text>().transform.localScale = Vector2.Lerp(button1.GetComponentInChildren<Text>().transform.localScale, new Vector2(1, 1), ScalingSpeed * Time.deltaTime);

            //Abfragen, ob Button ursprüngliche Größe hat
            if (button1.GetComponent<Image>().transform.localScale.y >= localsize + 0.05f && button1.GetComponent<Image>().transform.localScale.y <= localsize + 0.1f)
            {
                button1_moveback = true;
            }

            //Bewege Button zu Startposition zurück
            if(button1_moveback == true)
            {
                button1.transform.position = Vector2.MoveTowards(button1.transform.position, button1_start_pos, MovingSpeed * 10 * Time.deltaTime);
            }

            //Aktiviere alle Buttons wieder wenn Button an Startposition ist
            if(button1.transform.position.y == button1_start_pos.y)
            {
                //Erst wenn der Button komplett skaliert ist, kann der Benutzer den Button erneut klicken
                button1_isScaling = false;
                //Button befindet sich wieder an Startposition
                button1_moveback = false;
                //Button ist zurückgesetzt
                button1_finished = true;

                button2.SetActive(true);
                button3.SetActive(true);
                button4.SetActive(true);
                button5.SetActive(true);
                button6.SetActive(true);
            }
        }
        #endregion

        #region Button2
        if (button2_activated == true)
        {
            button2_finished = false; //Button beginnt skalierung und ist dann beendet, wenn wieder in Startposition
            //Andere Buttons deaktivieren
            button1.SetActive(false);
            button3.SetActive(false);
            button4.SetActive(false);
            button5.SetActive(false);
            button6.SetActive(false);

            buttonscale(button2);

            //Erst wenn der Button komplett skaliert ist, kann der Benutzer den Button erneut klicken
            if (button2.GetComponent<Image>().transform.localScale.y >= localsize + 0.95f)
            {
                button2_isScaling = false;
            }
        }
        //Alles Rückgängig beim 2. klick auf den Button
        else if (button2_activated == false && button2_finished == false)
        {
            //Größe vom Button zurücksetzen
            button2.GetComponent<Image>().transform.localScale = Vector2.Lerp(button2.transform.localScale, new Vector2(localsize, localsize), ScalingSpeed * Time.deltaTime);
            button2.GetComponentInChildren<Text>().transform.localScale = Vector2.Lerp(button2.GetComponentInChildren<Text>().transform.localScale, new Vector2(1, 1), ScalingSpeed * Time.deltaTime);

            //Abfragen, ob Button ursprüngliche Größe hat
            if (button2.GetComponent<Image>().transform.localScale.y >= localsize + 0.05f && button2.GetComponent<Image>().transform.localScale.y <= localsize + 0.1f)
            {
                button2_moveback = true;
            }

            //Bewege Button zu Startposition zurück
            if (button2_moveback == true)
            {
                button2.transform.position = Vector2.MoveTowards(button2.transform.position, button2_start_pos, MovingSpeed * 10 * Time.deltaTime);
            }

            //Aktiviere alle Buttons wieder wenn Button an Startposition ist
            if (button2.transform.position.y == button2_start_pos.y)
            {
                //Erst wenn der Button komplett skaliert ist, kann der Benutzer den Button erneut klicken
                button2_isScaling = false;
                //Button befindet sich wieder an Startposition
                button2_moveback = false;
                //Button ist zurückgesetzt
                button2_finished = true;

                button1.SetActive(true);
                button3.SetActive(true);
                button4.SetActive(true);
                button5.SetActive(true);
                button6.SetActive(true);
            }
        }
        #endregion

        #region Button3
        if (button3_activated == true)
        {
            button3_finished = false; //Button beginnt skalierung und ist dann beendet, wenn wieder in Startposition
            //Andere Buttons deaktivieren
            button2.SetActive(false);
            button1.SetActive(false);
            button4.SetActive(false);
            button5.SetActive(false);
            button6.SetActive(false);

            buttonscale(button3);

            //Erst wenn der Button komplett skaliert ist, kann der Benutzer den Button erneut klicken
            if (button3.GetComponent<Image>().transform.localScale.y >= localsize + 0.95f)
            {
                button3_isScaling = false;
            }
        }
        //Alles Rückgängig beim 2. klick auf den Button
        else if(button3_activated == false && button3_finished == false)
        {
            //Größe vom Button zurücksetzen
            button3.GetComponent<Image>().transform.localScale = Vector2.Lerp(button3.transform.localScale, new Vector2(localsize, localsize), ScalingSpeed * Time.deltaTime);
            button3.GetComponentInChildren<Text>().transform.localScale = Vector2.Lerp(button3.GetComponentInChildren<Text>().transform.localScale, new Vector2(1, 1), ScalingSpeed * Time.deltaTime);

            //Abfragen, ob Button ursprüngliche Größe hat
            if (button3.GetComponent<Image>().transform.localScale.y >= localsize + 0.05f && button3.GetComponent<Image>().transform.localScale.y <= localsize + 0.1f)
            {
                button3_moveback = true;
            }

            //Bewege Button zu Startposition zurück
            if (button3_moveback == true)
            {
                button3.transform.position = Vector2.MoveTowards(button3.transform.position, button3_start_pos, MovingSpeed * 10 * Time.deltaTime);
            }

            //Aktiviere alle Buttons wieder wenn Button an Startposition ist
            if (button3.transform.position.y == button3_start_pos.y)
            {
                //Erst wenn der Button komplett skaliert ist, kann der Benutzer den Button erneut klicken
                button3_isScaling = false;
                //Button befindet sich wieder an Startposition
                button3_moveback = false;
                //Button ist zurückgesetzt
                button3_finished = true;

                button2.SetActive(true);
                button1.SetActive(true);
                button4.SetActive(true);
                button5.SetActive(true);
                button6.SetActive(true);
            }
        }
        #endregion

        #region Button4
        if (button4_activated == true)
        {
            button4_finished = false; //Button beginnt skalierung und ist dann beendet, wenn wieder in Startposition
            //Andere Buttons deaktivieren
            button2.SetActive(false);
            button3.SetActive(false);
            button1.SetActive(false);
            button5.SetActive(false);
            button6.SetActive(false);

            buttonscale(button4);

            //Erst wenn der Button komplett skaliert ist, kann der Benutzer den Button erneut klicken
            if (button4.GetComponent<Image>().transform.localScale.y >= localsize + 0.95f)
            {
                button4_isScaling = false;
            }
        }
        //Alles Rückgängig beim 2. klick auf den Button
        else if (button4_activated == false && button4_finished == false)
        {
            //Größe vom Button zurücksetzen
            button4.GetComponent<Image>().transform.localScale = Vector2.Lerp(button4.transform.localScale, new Vector2(localsize, localsize), ScalingSpeed * Time.deltaTime);
            button4.GetComponentInChildren<Text>().transform.localScale = Vector2.Lerp(button4.GetComponentInChildren<Text>().transform.localScale, new Vector2(1, 1), ScalingSpeed * Time.deltaTime);

            //Abfragen, ob Button ursprüngliche Größe hat
            if (button4.GetComponent<Image>().transform.localScale.y >= localsize + 0.05f && button4.GetComponent<Image>().transform.localScale.y <= localsize + 0.1f)
            {
                button4_moveback = true;
            }

            //Bewege Button zu Startposition zurück
            if (button4_moveback == true)
            {
                button4.transform.position = Vector2.MoveTowards(button4.transform.position, button4_start_pos, MovingSpeed * 10 * Time.deltaTime);
            }

            //Aktiviere alle Buttons wieder wenn Button an Startposition ist
            if (button4.transform.position.y == button4_start_pos.y)
            {
                //Erst wenn der Button komplett skaliert ist, kann der Benutzer den Button erneut klicken
                button4_isScaling = false;
                //Button befindet sich wieder an Startposition
                button4_moveback = false;
                //Button ist zurückgesetzt
                button4_finished = true;

                button2.SetActive(true);
                button3.SetActive(true);
                button1.SetActive(true);
                button5.SetActive(true);
                button6.SetActive(true);
            }
        }
        #endregion

        #region Button5
        if (button5_activated == true)
        {
            button5_finished = false; //Button beginnt skalierung und ist dann beendet, wenn wieder in Startposition
            //Andere Buttons deaktivieren
            button2.SetActive(false);
            button3.SetActive(false);
            button4.SetActive(false);
            button1.SetActive(false);
            button6.SetActive(false);

            buttonscale(button5);

            //Erst wenn der Button komplett skaliert ist, kann der Benutzer den Button erneut klicken
            if (button5.GetComponent<Image>().transform.localScale.y >= localsize + 0.95f)
            {
                button5_isScaling = false;
            }
        }
        //Alles Rückgängig beim 2. klick auf den Button
        else if (button5_activated == false && button5_finished == false)
        {
            //Größe vom Button zurücksetzen
            button5.GetComponent<Image>().transform.localScale = Vector2.Lerp(button5.transform.localScale, new Vector2(localsize, localsize), ScalingSpeed * Time.deltaTime);
            button5.GetComponentInChildren<Text>().transform.localScale = Vector2.Lerp(button5.GetComponentInChildren<Text>().transform.localScale, new Vector2(1, 1), ScalingSpeed * Time.deltaTime);

            //Abfragen, ob Button ursprüngliche Größe hat
            if (button5.GetComponent<Image>().transform.localScale.y >= localsize + 0.05f && button5.GetComponent<Image>().transform.localScale.y <= localsize + 0.1f)
            {
                button5_moveback = true;
            }

            //Bewege Button zu Startposition zurück
            if (button5_moveback == true)
            {
                button5.transform.position = Vector2.MoveTowards(button5.transform.position, button5_start_pos, MovingSpeed * 10 * Time.deltaTime);
            }

            //Aktiviere alle Buttons wieder wenn Button an Startposition ist
            if (button5.transform.position.y == button5_start_pos.y)
            {
                //Erst wenn der Button komplett skaliert ist, kann der Benutzer den Button erneut klicken
                button5_isScaling = false;
                //Button befindet sich wieder an Startposition
                button5_moveback = false;
                //Button ist zurückgesetzt
                button5_finished = true;

                button2.SetActive(true);
                button3.SetActive(true);
                button4.SetActive(true);
                button1.SetActive(true);
                button6.SetActive(true);
            }
        }
        #endregion

        #region Button6
        if (button6_activated == true)
        {
            button6_finished = false; //Button beginnt skalierung und ist dann beendet, wenn wieder in Startposition
            //Andere Buttons deaktivieren
            button2.SetActive(false);
            button3.SetActive(false);
            button4.SetActive(false);
            button5.SetActive(false);
            button1.SetActive(false);

            buttonscale(button6);

            //Erst wenn der Button komplett skaliert ist, kann der Benutzer den Button erneut klicken
            if (button6.GetComponent<Image>().transform.localScale.y >= localsize + 0.95f)
            {
                button6_isScaling = false;
            }
        }
        //Alles Rückgängig beim 2. klick auf den Button
        else if(button6_activated == false && button6_finished == false)
        {
            //Größe vom Button zurücksetzen
            button6.GetComponent<Image>().transform.localScale = Vector2.Lerp(button6.transform.localScale, new Vector2(localsize, localsize), ScalingSpeed * Time.deltaTime);
            button6.GetComponentInChildren<Text>().transform.localScale = Vector2.Lerp(button6.GetComponentInChildren<Text>().transform.localScale, new Vector2(1, 1), ScalingSpeed * Time.deltaTime);

            //Abfragen, ob Button ursprüngliche Größe hat
            if (button6.GetComponent<Image>().transform.localScale.y >= localsize + 0.05f && button6.GetComponent<Image>().transform.localScale.y <= localsize + 0.1f)
            {
                button6_moveback = true;
            }

            //Bewege Button zu Startposition zurück
            if (button6_moveback == true)
            {
                button6.transform.position = Vector2.MoveTowards(button6.transform.position, button6_start_pos, MovingSpeed * 10 * Time.deltaTime);
            }

            //Aktiviere alle Buttons wieder wenn Button an Startposition ist
            if (button6.transform.position.y == button6_start_pos.y)
            {
                //Erst wenn der Button komplett skaliert ist, kann der Benutzer den Button erneut klicken
                button6_isScaling = false;
                //Button befindet sich wieder an Startposition
                button6_moveback = false;
                //Button ist zurückgesetzt
                button6_finished = true;

                button2.SetActive(true);
                button3.SetActive(true);
                button4.SetActive(true);
                button5.SetActive(true);
                button1.SetActive(true);
            }
        }
        #endregion
    }

    void buttonscale(GameObject button)
    {
        //Größe von Canvas auslesen, um bei Auflösungswechsel trotzdem mittig zu bleiben
        Vector2 can_size = can.transform.position;

        //Position von Button rechts in der Mitte festlegen
        Vector2 pos = button1.transform.position;
        pos.x = distLeft;
        pos.y = can_size.y;

        //Button langsam zur Position bewegen
        button.transform.position = Vector2.MoveTowards(button.transform.position, pos, MovingSpeed * 10 * Time.deltaTime);

        //Größe vom Button anpassen
        if (button.transform.position.y == can.transform.position.y)
        {
            button.GetComponent<Image>().transform.localScale = Vector2.Lerp(button.transform.localScale, new Vector2(localsize, localsize * 2), ScalingSpeed * Time.deltaTime);
            button.GetComponentInChildren<Text>().transform.localScale = Vector2.Lerp(button.GetComponentInChildren<Text>().transform.localScale, new Vector2(1, 0.5f), ScalingSpeed * Time.deltaTime);
        }
    }



    //Funktionen, die bei Knopfdruck aufgerufen werden
    public void ButtonBigger1()
    {
        if(button1_activated == false && button1_isScaling == false)
        {
            button1_isScaling = true; //Button wird gerade Skaliert, verhindert dass während Skalierung erneut geklickt werden kann
            button1_activated = true; //Button wurde ausgewählt und soll sich vergrößern
        }
        else if(button1_activated == true && button1_isScaling == false)
        {
            button1_isScaling = true;  //Button wird gerade Skaliert, verhindert dass während Skalierung erneut geklickt werden kann
            button1_activated = false; //Button wurde ausgewählt und soll sich verkleinern
        }
    }

    public void ButtonBigger2()
    {
        if (button2_activated == false && button2_isScaling == false)
        {
            button2_isScaling = true; //Button wird gerade Skaliert, verhindert dass während Skalierung erneut geklickt werden kann
            button2_activated = true; //Button wurde ausgewählt und soll sich vergrößern
        }
        else if (button2_activated == true && button2_isScaling == false)
        {
            button2_isScaling = true;  //Button wird gerade Skaliert, verhindert dass während Skalierung erneut geklickt werden kann
            button2_activated = false; //Button wurde ausgewählt und soll sich verkleinern
        }
    }

    public void ButtonBigger3()
    {
        if (button3_activated == false && button3_isScaling == false)
        {
            button3_isScaling = true; //Button wird gerade Skaliert, verhindert dass während Skalierung erneut geklickt werden kann
            button3_activated = true; //Button wurde ausgewählt und soll sich vergrößern
        }
        else if (button3_activated == true && button3_isScaling == false)
        {
            button3_isScaling = true;  //Button wird gerade Skaliert, verhindert dass während Skalierung erneut geklickt werden kann
            button3_activated = false; //Button wurde ausgewählt und soll sich verkleinern
        }
    }

    public void ButtonBigger4()
    {
        if (button4_activated == false && button4_isScaling == false)
        {
            button4_isScaling = true; //Button wird gerade Skaliert, verhindert dass während Skalierung erneut geklickt werden kann
            button4_activated = true; //Button wurde ausgewählt und soll sich vergrößern
        }
        else if (button4_activated == true && button4_isScaling == false)
        {
            button4_isScaling = true;  //Button wird gerade Skaliert, verhindert dass während Skalierung erneut geklickt werden kann
            button4_activated = false; //Button wurde ausgewählt und soll sich verkleinern
        }
    }

    public void ButtonBigger5()
    {
        if (button5_activated == false && button5_isScaling == false)
        {
            button5_isScaling = true; //Button wird gerade Skaliert, verhindert dass während Skalierung erneut geklickt werden kann
            button5_activated = true; //Button wurde ausgewählt und soll sich vergrößern
        }
        else if (button5_activated == true && button5_isScaling == false)
        {
            button5_isScaling = true;  //Button wird gerade Skaliert, verhindert dass während Skalierung erneut geklickt werden kann
            button5_activated = false; //Button wurde ausgewählt und soll sich verkleinern
        }
    }

    public void ButtonBigger6()
    {
        if (button6_activated == false && button6_isScaling == false)
        {
            button6_isScaling = true; //Button wird gerade Skaliert, verhindert dass während Skalierung erneut geklickt werden kann
            button6_activated = true; //Button wurde ausgewählt und soll sich vergrößern
        }
        else if (button6_activated == true && button6_isScaling == false)
        {
            button6_isScaling = true;  //Button wird gerade Skaliert, verhindert dass während Skalierung erneut geklickt werden kann
            button6_activated = false; //Button wurde ausgewählt und soll sich verkleinern
        }
    }
}
