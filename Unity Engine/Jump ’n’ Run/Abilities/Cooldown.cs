using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    #region Variables_Universal
    public GameObject Cloudy;                               //Cloudy als GameObject für Parent der Fähigkeiten
    public GameObject Pool_object;

    public Transform DefensiveLoadingBar;                   //Cooldown-Kreis für Defensive Fähigkeiten
    public Transform DefensiveTextTime;                     //Cooldown-Text für Defensive Fähigkeiten
    public Transform OffensiveLoadingBar;                   //Cooldown-Kreis für Offensive Fähigkeiten
    public Transform OffensiveTextTime;                     //Cooldown-Text für Offensive Fähigkeiten
    public Transform PoolingLoadingBar;                     //Cooldown-Kreis für Pooling Fähigkeit
    public Transform PoolingTextTime;                       //Cooldown-Text für Pooling Fähigkeit

    public bool OffensiveThunderboltIsActivated;    //Welche Offensive Fähigkeit ist wurde gewählt?
    public bool OffensiveEnergyDartIsActivated;     //Welche Offensive Fähigkeit ist wurde gewählt?

    public bool DefensiveWallIsActivated;           // Welche Defensive Fähigkeit wurde gewählt. Oben Gramatikfehler!!!
    public bool DefensiveSlowlyIsActivated;
    #endregion

    #region Variables_Wall
    [SerializeField] private float WallCooldownTime;  //Cooldown-Zeit

    private float Wall_timer = 0.0f;                    //Timer für Wall
    private float Wall_time;                            //Zeit für Sekundenberechnung
    private bool Wall_Cooldown_activated = false;       //Abfrage, um festzustellen ob Cooldown aktiviert wurde
    #endregion


    #region Variables_Slowly
    [SerializeField] private float SlowlyCooldownTime;  //Cooldown-Zeit

    private float Slowly_timer = 0.0f;                    //Timer für Wall
    private float Slowly_time;                            //Zeit für Sekundenberechnung
    private bool Slowly_Cooldown_activated = false;       //Abfrage, um festzustellen ob Cooldown aktiviert wurde
    #endregion

    #region Variables_Thunderbolt
    [SerializeField] private float ThunderboltCooldownTime;

    private float Thunderbolt_timer = 0.0f;
    private float Thunderbolt_time;
    private bool Thunderbolt_Cooldown_activated = false;
    #endregion

    #region Variables_EnergyDart
    [SerializeField] private float EnergydartCooldownTime;

    private float Energydart_timer = 0.0f;
    private float Energydart_time;
    private bool Energydart_Cooldown_activated = false;
    #endregion

    #region Variables_Pooling
    [SerializeField] private float PoolingCooldownTime;

    private float Pooling_timer = 0.0f;
    private float Pooling_time;
    private bool Pooling_Cooldown_activated = false;
    #endregion

    private void Start()
    {
        Wall_time = WallCooldownTime;
        Thunderbolt_time = ThunderboltCooldownTime;
        Energydart_time = EnergydartCooldownTime;
        Pooling_time = PoolingCooldownTime;
    }
    // Update is called once per frame
    void Update()
    {
        /*************************************
        ***       Cooldown for Wall        ***
        *************************************/

        #region Cooldown_Wall
        if (Cloudy.GetComponent<Wall>().Cooldown_Activated == true)
        {
            Wall_Cooldown_activated = true;
        }

        //If Cooldown has been activated

        if (DefensiveWallIsActivated)
        {


            if (Wall_Cooldown_activated == true)
            {
                //enable Text Field for Cooldown Timer
                DefensiveTextTime.GetComponent<Text>().enabled = true;
                DefensiveLoadingBar.GetComponent<Image>().enabled = true;

                //Calculate seconds
                Wall_timer += Time.deltaTime;
                float seconds = Mathf.RoundToInt(Wall_timer % 60);

                //decrease fillAmount of Image from 1 to 0 in "Cooldown_Time"-seconds and show remaining Time
                if (Wall_time > 0)
                {
                    Wall_time -= Time.deltaTime;
                    DefensiveLoadingBar.GetComponent<Image>().fillAmount = Wall_time / WallCooldownTime;
                    DefensiveTextTime.GetComponent<Text>().text = (WallCooldownTime - seconds).ToString();
                }
                //deactivate Cooldown after Cooldown_Time so Ability can be used again
                if (DefensiveLoadingBar.GetComponent<Image>().fillAmount == 0)
                {
                    Cloudy.GetComponent<Wall>().Cooldown_Activated = false;
                    Wall_Cooldown_activated = false;
                }
            }
            //reset everything
            else
            {
                DefensiveTextTime.GetComponent<Text>().enabled = false;
                DefensiveLoadingBar.GetComponent<Image>().enabled = false;
                Wall_timer = 0.0f;
                DefensiveLoadingBar.GetComponent<Image>().fillAmount = 1;
                DefensiveTextTime.GetComponent<Text>().text = WallCooldownTime.ToString();
                Wall_time = WallCooldownTime;
            }

        }
        #endregion




        /*************************************
       ***       Cooldown for Slowly        ***
       *************************************/

        #region Cooldown_Slowly
        if (Cloudy.GetComponent<Slowly>().Cooldown_Activated == true)
        {
            Slowly_Cooldown_activated = true;
        }

        //If Cooldown has been activated

        if (DefensiveSlowlyIsActivated)
        {


            if (Slowly_Cooldown_activated == true)
            {
                //enable Text Field for Cooldown Timer
                DefensiveTextTime.GetComponent<Text>().enabled = true;
                DefensiveLoadingBar.GetComponent<Image>().enabled = true;

                //Calculate seconds
                Slowly_timer += Time.deltaTime;
                float seconds = Mathf.RoundToInt(Slowly_timer % 60);

                //decrease fillAmount of Image from 1 to 0 in "Cooldown_Time"-seconds and show remaining Time
                if (Slowly_time > 0)
                {
                    Slowly_time -= Time.deltaTime;
                    DefensiveLoadingBar.GetComponent<Image>().fillAmount = Slowly_time / SlowlyCooldownTime;
                    DefensiveTextTime.GetComponent<Text>().text = (SlowlyCooldownTime - seconds).ToString();
                }
                //deactivate Cooldown after Cooldown_Time so Ability can be used again
                if (DefensiveLoadingBar.GetComponent<Image>().fillAmount == 0)
                {
                    Cloudy.GetComponent<Slowly>().Cooldown_Activated = false;
                    Slowly_Cooldown_activated = false;
                }
            }
            //reset everything
            else
            {
                DefensiveTextTime.GetComponent<Text>().enabled = false;
                DefensiveLoadingBar.GetComponent<Image>().enabled = false;
                Slowly_timer = 0.0f;
                DefensiveLoadingBar.GetComponent<Image>().fillAmount = 1;
                DefensiveTextTime.GetComponent<Text>().text = WallCooldownTime.ToString();
                Slowly_time = SlowlyCooldownTime;
            }

        }
        #endregion




        /*************************************
        ***    Cooldown for Thunderbolt    ***
        *************************************/

        #region Cooldown_Thunderbolt
        if (OffensiveThunderboltIsActivated == true)
        {
            if (Cloudy.GetComponent<Thunderbolt>().Cooldown_Activated == true)
            {
                Thunderbolt_Cooldown_activated = true;
            }

            //If Cooldown has been activated
            if (Thunderbolt_Cooldown_activated == true)
            {
                //enable Text Field for Cooldown Timer
                OffensiveTextTime.GetComponent<Text>().enabled = true;
                OffensiveLoadingBar.GetComponent<Image>().enabled = true;

                //Calculate seconds
                Thunderbolt_timer += Time.deltaTime;
                float seconds = Mathf.RoundToInt(Thunderbolt_timer % 60);



                //decrease fillAmount of Image from 1 to 0 in "Cooldown_Time"-seconds and show remaining Time
                if (Thunderbolt_time > 0)
                {
                    Thunderbolt_time -= Time.deltaTime;
                    OffensiveLoadingBar.GetComponent<Image>().fillAmount = Thunderbolt_time / ThunderboltCooldownTime;
                    OffensiveTextTime.GetComponent<Text>().text = (ThunderboltCooldownTime - seconds).ToString();
                }
                //deactivate Cooldown after Cooldown_Time so Ability can be used again
                if (OffensiveLoadingBar.GetComponent<Image>().fillAmount == 0)
                {
                    Cloudy.GetComponent<Thunderbolt>().Cooldown_Activated = false;
                    Thunderbolt_Cooldown_activated = false;
                }
            }
            //reset everything
            else
            {
                OffensiveTextTime.GetComponent<Text>().enabled = false;
                OffensiveLoadingBar.GetComponent<Image>().enabled = false;
                Thunderbolt_timer = 0.0f;
                OffensiveLoadingBar.GetComponent<Image>().fillAmount = 1;
                OffensiveTextTime.GetComponent<Text>().text = ThunderboltCooldownTime.ToString();
                Thunderbolt_time = ThunderboltCooldownTime;
            }
        }
        #endregion



        /*************************************
        ***    Cooldown for Energy Dart    ***
        *************************************/

        #region Cooldown_EnergyDart
        if (OffensiveEnergyDartIsActivated == true)
        {
            if (Cloudy.GetComponent<Energy_Dart>().Cooldown_Activated == true)
            {
                Energydart_Cooldown_activated = true;
            }

            //If Cooldown has been activated
            if (Energydart_Cooldown_activated == true)
            {
                //enable Text Field for Cooldown Timer
                OffensiveTextTime.GetComponent<Text>().enabled = true;
                OffensiveLoadingBar.GetComponent<Image>().enabled = true;

                //Calculate seconds
                Energydart_timer += Time.deltaTime;
                float seconds = Mathf.RoundToInt(Energydart_timer % 60);

                //decrease fillAmount of Image from 1 to 0 in "Cooldown_Time"-seconds and show remaining Time
                if (Energydart_time > 0)
                {
                    Energydart_time -= Time.deltaTime;
                    OffensiveLoadingBar.GetComponent<Image>().fillAmount = Energydart_time / EnergydartCooldownTime;
                    OffensiveTextTime.GetComponent<Text>().text = (EnergydartCooldownTime - seconds).ToString();
                }
                //deactivate Cooldown after Cooldown_Time so Ability can be used again
                if (OffensiveLoadingBar.GetComponent<Image>().fillAmount == 0)
                {
                    Cloudy.GetComponent<Energy_Dart>().Cooldown_Activated = false;
                    Energydart_Cooldown_activated = false;
                }
            }
            //reset everything
            else
            {
                OffensiveTextTime.GetComponent<Text>().enabled = false;
                OffensiveLoadingBar.GetComponent<Image>().enabled = false;
                Energydart_timer = 0.0f;
                OffensiveLoadingBar.GetComponent<Image>().fillAmount = 1;
                OffensiveTextTime.GetComponent<Text>().text = EnergydartCooldownTime.ToString();
                Energydart_time = EnergydartCooldownTime;
            }
        }
        #endregion



        /*************************************
        ***    Cooldown for Pooling        ***
        *************************************/

        #region Cooldown_Pooling
        if (Pool_object.GetComponent<Pool>().Cooldown_Activated == true)
        {
            Pooling_Cooldown_activated = true;
        }

        //If Cooldown has been activated
        if (Pooling_Cooldown_activated == true)
        {
            //enable Text Field for Cooldown Timer
            PoolingTextTime.GetComponent<Text>().enabled = true;
            PoolingLoadingBar.GetComponent<Image>().enabled = true;

            //Calculate seconds
            Pooling_timer += Time.deltaTime;
            float seconds = Mathf.RoundToInt(Pooling_timer % 60);

            //decrease fillAmount of Image from 1 to 0 in "Cooldown_Time"-seconds and show remaining Time
            if (Pooling_time > 0)
            {
                Pooling_time -= Time.deltaTime;
                PoolingLoadingBar.GetComponent<Image>().fillAmount = Pooling_time / PoolingCooldownTime;
                PoolingTextTime.GetComponent<Text>().text = (PoolingCooldownTime - seconds).ToString();
            }
            //deactivate Cooldown after Cooldown_Time so Ability can be used again
            if (PoolingLoadingBar.GetComponent<Image>().fillAmount == 0)
            {
                Pool_object.GetComponent<Pool>().Cooldown_Activated = false;
                Pooling_Cooldown_activated = false;
            }
        }
        //reset everything
        else
        {
            PoolingTextTime.GetComponent<Text>().enabled = false;
            PoolingLoadingBar.GetComponent<Image>().enabled = false;
            Pooling_timer = 0.0f;
            PoolingLoadingBar.GetComponent<Image>().fillAmount = 1;
            PoolingTextTime.GetComponent<Text>().text = PoolingCooldownTime.ToString();
            Pooling_time = PoolingCooldownTime;
        }
        #endregion
        
          
    }
}
