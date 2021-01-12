using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slowly : MonoBehaviour
{
    //For Pause
    public GameObject PauseObject;


    // slowly area
    public GameObject Slowly_area;

    //for Cooldown
    public Transform Defensive_Cooldown_Image;
    public bool Cooldown_Activated = false;

    public GameObject CooldownObject;

    //how long should the wall stay
    public float wall_time = 4.0f;
    float current_time = 0.0f;
    public GameObject Object_Ghosty;

    //Variables for scaling Wall
    public float height = 2f;
    public float width = 0.2f;

    //needed to move and scale cloudy if button is pressed
    //bool move_cloudy = false;
    bool scale_cloudy = false;

    //Check if Wall is activated
    bool Wall_Active = false;

    //needed for rescaling
    Vector3 normal_scale;

    //Needed for reset
    bool IsResetted = true;



    // Start is called before the first frame update
    void Start()
    {
        normal_scale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        if (Defensive_Cooldown_Image.GetComponent<Image>().fillAmount == 1 && PauseObject.GetComponent<Pause>().pause == false && CooldownObject.GetComponent<Cooldown>().DefensiveSlowlyIsActivated == true) //Cooldown fertig? && Pause deaktivert?
        {
            if (Input.GetKeyDown("3") && Wall_Active == false)
            {
                IsResetted = false;

           

                current_time = 0;

                Slowly_area.GetComponent<SpriteRenderer>().enabled = true;
                Slowly_area.GetComponent<BoxCollider2D>().enabled = true;
                //disable Cloudy Controller that Cloudy can move down to Ghosty
                gameObject.GetComponent<Cloudy_Controller>().enabled = false;
                gameObject.GetComponent<Energy_Dart>().enabled = false;
                gameObject.GetComponent<Thunderbolt>().enabled = false;
                gameObject.GetComponent<Wall>().enabled = false;
                gameObject.GetComponent<Cloudy_Energy>().enabled = false;
                //Move Cloudy down to Ghosty
                //move_cloudy = true;
                //Scale Cloudy
                scale_cloudy = true;
                //Wall is activated
                Wall_Active = true;
            }
        }

        /*

        //move cloudy in front of ghosty and keep staying there
        if (move_cloudy == true)
        {

            

            if (Object_Ghosty.GetComponent<Transform>().localScale.x == 1)
            {

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Object_Ghosty.transform.position.x + 10, Object_Ghosty.transform.position.y), 2);
                if (transform.position.x == Object_Ghosty.transform.position.x + 10)
                {
                    GetComponent<BoxCollider2D>().enabled = true;
                    move_cloudy = false;
                }


            }


            if (Object_Ghosty.GetComponent<Transform>().localScale.x == -1)
            {

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Object_Ghosty.transform.position.x - 10, Object_Ghosty.transform.position.y), 2);
                if (transform.position.x == Object_Ghosty.transform.position.x - 10)
                {
                    GetComponent<BoxCollider2D>().enabled = true;
                    move_cloudy = false;
                }


            }



        }

    */

        //scale cloudy to wall
        if (scale_cloudy == true)
        {

            current_time = current_time + Time.deltaTime;
            //transform.localScale = new Vector3.(width, height, 1f);

            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(width, height, 1f),0.2f);


        }

        if (current_time >= wall_time && IsResetted == false)
        {
            //Activate Cooldown
            Cooldown_Activated = true;

            //Wall has been deactivated
            Wall_Active = false;

            //dont move cloudy anymore to the side
           // move_cloudy = false;

            //rescale Cloudy
            scale_cloudy = false;
            transform.localScale = normal_scale;

            //enable Cloudy Controller and disable Box Collider again
            Slowly_area.GetComponent<SpriteRenderer>().enabled = false;
            Slowly_area.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Cloudy_Controller>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Energy_Dart>().enabled = true;
            gameObject.GetComponent<Thunderbolt>().enabled = true;
            gameObject.GetComponent<Wall>().enabled = true;
            gameObject.GetComponent<Cloudy_Energy>().enabled = true;

            //Cloudy is Resetted
            IsResetted = true;
        }
    }
}
