using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy_Dart : MonoBehaviour
{
    //For Pause
    public GameObject PauseObject;

    //For Cooldown
    public Transform Offensive_Cooldown_Image;
    public GameObject CooldownObject;
    public bool Cooldown_Activated = false;

    //For Energy
    public int energy_amount;
    private Cloudy_Energy cloudy_energy;  // script wird eingefügt

    //Energy Dart starts flying at Emitter (Cloudy)
    public GameObject Energy_Dart_Emitter;
    //the Prefab of the Energy Dart
    public GameObject Energy_Dart_Prefab;
    //the aim
    public GameObject Aim_Object;
    //Ghosty needed to change Attack left/right
    private Transform ghosty;

    //Speed of the shot Energy Dart
    public float Energy_Dart_Speed;
    //Speed of rotation of the aim
    public float Aim_Speed;

    //variable for changing direction of aim
    bool aim_direction = false; //false = down, true = up

    bool looking_right = true;  // schaut Ghosty nach rechts?
    bool identic = true;        // damit während des Zielvorgangs die Richtung beibehält
    private Vector3 reset;      // kann man auch offset nennen

  

    // Start is called before the first frame update
    void Start()
    {

        cloudy_energy = this.GetComponent<Cloudy_Energy>();

        ghosty = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        reset = transform.position - Aim_Object.transform.position;  // offset zwischen Cloudy und Aim, damit man die Anordnung immer gleich hat, egal wie sich Cloudy bewegt

        Aim_Object.transform.eulerAngles = new Vector3(0, 0, 270);   // also fängt genau unter Cloudy an. Die Zeile kann auch weg gelassen werden. da schon Update() steht

    }


    // Update is called once per frame
    void Update()
    {

        if (cloudy_energy.energy >= energy_amount && PauseObject.GetComponent<Pause>().pause == false)   //Bedingung - reicht die Energy für diese Attacke?? && Pause deaktiviert?
        {
            if (Offensive_Cooldown_Image.GetComponent<Image>().fillAmount == 1 && CooldownObject.GetComponent<Cooldown>().OffensiveEnergyDartIsActivated == true) //Cooldown fertig und Energy Dart als Offensive Fähigkeit gewählt?
            {
                if (looking_right == identic)   //identic wird immer bei der Funktion shoot gleichgesetzt, sodass dann bei Update()- looking_right übernommen werden kann
                {
                    looking_right = (ghosty.localScale.x == 1 ? true : false);   // Ghosty rechts laufend ist im localscale.x positiv 1
                    Aim_Object.transform.eulerAngles = new Vector3(0, 0, 270);   // Winkel immer bei 270 grad beginnen.

                    if (looking_right != identic)   // falls Ghostly sich hin und her bewegt, soll bitte looking_right und identic identisch bleiben. bis left shift gedrückt wird
                    {

                        identic = looking_right;
                    }

                }


                //Place aim //// only show aim if left shift is pressed
                if (Input.GetKey("left shift"))
                {


                    identic = looking_right ? false : true; // hier gibt man identic das gegenteil von looking Right, sodass beim Zielvorgang die richtung beibehält

                    //get x-y-coords from aim and emitter
                    float y_position_aim = Aim_Object.transform.position.y;
                    float x_position_aim = Aim_Object.transform.position.x;
                    float y_position_emitter = Energy_Dart_Emitter.transform.position.y;
                    float x_position_emitter = Energy_Dart_Emitter.transform.position.x;


                    //enable Sprite Renderer that the Object aim is shown
                    Aim_Object.gameObject.GetComponent<SpriteRenderer>().enabled = true;

                    //Rotate the Object aim around the Emitter Position
                    if ((y_position_aim >= y_position_emitter + 10) && looking_right)
                    {
                        aim_direction = false;    // im Uhrzeigersinn wenn Ghosty rechts schaut. Wenn also oben rechts ist -> bewegt sich im uhrzeigersinn zurück
                    }


                    if ((y_position_aim >= y_position_emitter + 10) && looking_right == false)
                    {
                        aim_direction = true;    // gegen Uhrzeigersinn wenn Ghosty links schauend. wenn er links oben ist -> gegen uhrzeigersinn zrück
                    }


                    if ((x_position_aim <= x_position_emitter) && looking_right)
                    {
                        aim_direction = true;   // gegen Uhrzeigersinn wenn Ghosty rechts schauend. wenn er genau unten mittig ist -> bewegt sich gegen uhrzeigersinn zurück
                    }


                    if ((x_position_aim >= x_position_emitter) && looking_right == false)
                    {
                        aim_direction = false;   // im Uhrzeigersinn. Unten mittig im uhrzeigersinn nach oben
                    }


                    //Ghosty is looking right  ( Mann muss von (unten/leicht rechts)nach rechts oben wandern lassen) und dann nach schuss wieder reseten

                    if (aim_direction == true)   //gegen uhrzeigersinn
                    {
                        Aim_Object.transform.RotateAround(Energy_Dart_Emitter.transform.position, Vector3.forward, 20 * Aim_Speed * Time.deltaTime);
                    }
                    if (aim_direction == false)  // im uhrzeigersinn
                    {
                        Aim_Object.transform.RotateAround(Energy_Dart_Emitter.transform.position, Vector3.back, 20 * Aim_Speed * Time.deltaTime);
                    }

                }
                else
                {
                    Aim_Object.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }

                //after selecting position with aim, shoot the Energy Dart to it's position
                if (Input.GetKeyUp("left shift"))
                {
                    cloudy_energy.use_ability(-energy_amount);
                    Shoot();
                    Cooldown_Activated = true;
                }
            }
        }


    }


    void Shoot()
    {

        identic = looking_right; // In Update() looking_right kann sich jetzt wieder bei neuem Schuss ändern
        //get coords of aim
        float x_position_aim = Aim_Object.transform.position.x;
        float y_position_aim = Aim_Object.transform.position.y;
        float z_position_aim = Aim_Object.transform.position.z;

        //get angles of aim
        var angle = Aim_Object.transform.eulerAngles;
    
        //The Energy Dart instantiation happens here
        GameObject Temporary_Energy_Dart_Handler;
        Temporary_Energy_Dart_Handler = Instantiate(Energy_Dart_Prefab, Energy_Dart_Emitter.transform.position, Energy_Dart_Emitter.transform.rotation) as GameObject; //(Object, Emitter, Rotation)

        //Retrieve the Rigidbody component from the instantiated Energy Dart and control it
        Rigidbody2D Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Energy_Dart_Handler.GetComponent<Rigidbody2D>();

        //shoot out of Cloudy
        Temporary_RigidBody.transform.Rotate(angle);
        //Temporary_RigidBody.AddForce(new Vector2(x_position_aim, y_position_aim) * 10 * Energy_Dart_Speed);
        Vector2 MoveDirection = (Aim_Object.transform.position - transform.position).normalized * Energy_Dart_Speed;
        Temporary_RigidBody.velocity = new Vector2(MoveDirection.x, MoveDirection.y);

        Aim_Object.transform.position = transform.position-reset;  // Aim wird wieder genau mittig unten bei Cloudy gesetzt. Also das Offset liegt zwischen denen

    }

}