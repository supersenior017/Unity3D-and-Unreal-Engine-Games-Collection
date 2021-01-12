using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thunderbolt : MonoBehaviour
{
    //For Pause
    public GameObject PauseObject;

    //for Cooldown
    public Transform Offensive_Cooldown_Image;
    public GameObject CooldownObject;
    public bool Cooldown_Activated = false;

    // Cloudy_Energy (Energyverbrauch)
    public int energy_amount; 
    private Cloudy_Energy cloudy_energy;  // script wird eingefügt
   
    //Thunderbolt emitter
    public GameObject Thunderbolt_Emitter;
    //Prefab for Trhunderbolt
    public GameObject Thunderbolt_Prefab;
    //aim for Thunderbolt
    public GameObject Thunderbolt_aim;
    //Ghosty needed to change Attack left/right
    private Transform ghosty;

    //speed of Cloudy
    public float speed_Cloudy;
    private float speed_old;
    public float distance;

 
    //variable for changing direction of aim
    bool looking_right = true;  // schaut Ghosty nach rechts?
    bool identic = true;        // damit während des Zielvorgangs die Richtung beibehält


    // Start is called before the first frame update
    void Start()
    {

        cloudy_energy = this.GetComponent<Cloudy_Energy>();
        ghosty = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed_old = speed_Cloudy;

           
    }

    // Update is called once per frame
    void Update()
    {
    

        if (cloudy_energy.energy >= energy_amount)   //Bedingung - reicht die Energy für diese Attacke??
        {

            if (Offensive_Cooldown_Image.GetComponent<Image>().fillAmount == 1 &&
                CooldownObject.GetComponent<Cooldown>().OffensiveThunderboltIsActivated == true
                && PauseObject.GetComponent<Pause>().pause == false) //Cooldown fertig && Fähigkeit Thunderbolt gewählt && Pause deaktiviert?
            {


                if (Vector2.Distance(transform.position, ghosty.position) > distance)
                {

                    speed_Cloudy = 0;
                }

                if (Vector2.Distance(transform.position, ghosty.position) > distance + 5)
                {

                    speed_Cloudy = -20;
                }

                if (Vector2.Distance(transform.position, ghosty.position) < distance)
                {
                    speed_Cloudy = speed_old;

                }


                if (looking_right == identic)   //identic wird immer bei der Funktion shoot gleichgesetzt, sodass dann bei Update()- looking_right übernommen werden kann
                {
                    looking_right = (ghosty.localScale.x == 1 ? true : false);   // Ghosty rechts laufend ist im localscale.x positiv 1

                    if (looking_right != identic)   // falls Ghostly sich hin und her bewegt, soll bitte looking_right und identic identisch bleiben. bis left shift gedrückt wird
                    {

                        identic = looking_right;
                    }
                }


                if (Input.GetKey("space"))
                {

                    identic = looking_right ? false : true; // hier gibt man identic das gegenteil von looking Right, sodass beim Zielvorgang die richtung beibehält

                    //disable Cloudy Controller if Space is pressed and enable it if space is not pressed anymore
                    GetComponent<Cloudy_Controller>().enabled = false;
                    //show Thunderbolt aim if space is pressed
                    Thunderbolt_aim.GetComponent<SpriteRenderer>().enabled = true;

                    if (looking_right == true)
                    {
                        gameObject.transform.Translate(Vector2.right * speed_Cloudy * Time.deltaTime);  //if ghosty looks right, cloudy moves right
                    }
                    if (looking_right == false)
                    {
                        gameObject.transform.Translate(Vector2.left * speed_Cloudy * Time.deltaTime);  //if ghosty looks left, cloudy moves left
                    }                    
                }


                if (Input.GetKeyUp("space"))
                {
                    Shoot();

                    Cooldown_Activated = true;

                    cloudy_energy.use_ability(-energy_amount);  // Energy abziehen


                    //reset Cloudy Controller and Sprite renderer of Thunderbolt aim
                    GetComponent<Cloudy_Controller>().enabled = true;
                    Thunderbolt_aim.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }

    void Shoot()
    {
        //The Energy Dart instantiation happens here

        identic = looking_right;

        GameObject Temporary_Energy_Dart_Handler;
        Temporary_Energy_Dart_Handler = Instantiate(Thunderbolt_Prefab, Thunderbolt_Emitter.transform.position, Thunderbolt_Emitter.transform.rotation) as GameObject; //(Object, Emitter, Rotation)

        //Retrieve the Rigidbody component from the instantiated Energy Dart and control it
        Rigidbody2D Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Energy_Dart_Handler.GetComponent<Rigidbody2D>();
    }
}
