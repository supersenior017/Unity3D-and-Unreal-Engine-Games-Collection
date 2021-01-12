using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/// <summary>
///
///Rocket is flying through the position of the mouse-click
///Change asset_rotation
///
/// </summary>


public class Rocket_start : MonoBehaviour
{
    private Camera cam;

    private Renderer Rocket_renderer;
    private CapsuleCollider2D Rocket_collider;


    public bool cool_down_now = false;


    private Vector3 targetDirection;
    Vector3 point = new Vector3();

    float angle;
    public bool go = false;

    public float rocketSpeed = 10f;
    public float rotate_speed = 5f;


    // dot lines

    public Sprite Dot;
    [Range(0.01f, 1f)]
    public float Size;
    [Range(0.1f, 2f)]
    public float Delta;


    Touch Touching;


    //upgrades

    //speed Upgrade
    private float backup_rocketspeed;
    float Timer_speed_upgrade;

    float backup_cooldown;

    //piercing Upgrade
    public bool PiercingUpgrade_on = false;
    private float Timer_piercing_upgrade;

    //multishot Upgrade
    bool multishot_on = false;
    float Timer_multishot_upgrade;

    public GameObject left_Rocket;
    public GameObject right_Rocket;

    public GameObject left_Rocket_Stand;
    public GameObject right_Rocket_Stand;
    

    void Start()
    {
        cam = Camera.main;

        Rocket_renderer = transform.GetComponent<Renderer>();
        Rocket_collider = transform.GetComponent<CapsuleCollider2D>();


        backup_rocketspeed = rocketSpeed;

        backup_cooldown = GetComponent<Rocket_cool_down>().cooldownCounter;

        left_Rocket.SetActive(false);
        left_Rocket_Stand.SetActive(false);
        right_Rocket.SetActive(false);
        right_Rocket_Stand.SetActive(false);
            
    }

    //write mouse position in the variable point
    void OnGUI()
    {
        if (Time.timeScale == 1)
        {

            Event currentEvent = Event.current;
            Vector2 mousePos = new Vector2();

            // Get the mouse position from Event.
            // Note that the y position from Event is inverted.


            //für maus**********
            mousePos.x = currentEvent.mousePosition.x;
            mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;


            if (Input.touchCount > 0)
            {

                Touching = Input.touches[0];

                point = cam.ScreenToWorldPoint(new Vector3(Touching.position.x, Touching.position.y, cam.nearClipPlane));
            }
            else
            {

                // we need worldPoint Vector. Save it in point
                point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

            }
        }
    }


    void Update()
    {

        if(multishot_on == true)
        {
            if (left_Rocket != null)
            {
                left_Rocket.transform.position = transform.position - new Vector3(0.5f, 0, 0);
                left_Rocket.transform.rotation = transform.rotation;
            }
            if (right_Rocket != null)
            {
                right_Rocket.transform.position = transform.position + new Vector3(0.5f, 0, 0);
                right_Rocket.transform.rotation = transform.rotation;
            }

        }
            //Speed Upgrade Timer
            if (Timer_speed_upgrade > 0)
            {

            Timer_speed_upgrade -= Time.deltaTime;

            }

            if (Timer_speed_upgrade <= 0)
            {

            rocketSpeed = backup_rocketspeed;
            GetComponent<Rocket_cool_down>().backup_cooldownCounter = backup_cooldown;
             }


            //Piercing Upgrade Timer
            if (Timer_piercing_upgrade > 0)
            {
                Timer_piercing_upgrade -= Time.deltaTime;
            }

            if (Timer_piercing_upgrade <= 0)
            {
                PiercingUpgrade_on = false;
            }

            //Scale Upgrade Timer
            if (Timer_multishot_upgrade > 0)
            {
                Timer_multishot_upgrade -= Time.deltaTime;
            }

            if (Timer_multishot_upgrade <= 0)
            {
                left_Rocket.SetActive(false);
                left_Rocket_Stand.SetActive(false);
                right_Rocket.SetActive(false);
                right_Rocket_Stand.SetActive(false);

                multishot_on = false;
        }


            if (Input.GetKey(KeyCode.Mouse0) && go == false)
            {
                targetDirection = (point - transform.position);
            // targetDirection = new Vector3(targetDirection.x, targetDirection.y, 0);

            angle = Vector3.Angle(targetDirection, transform.up);

            if (angle < 90.0f)
            {
                GetComponent<Dot_lines>().DrawDottedLine(transform.position, point);
            }

            }



            if (Input.GetButtonUp("Fire1") && go == false)
            {

                targetDirection = (point - transform.position);//.normalized;


                //change z Vector of targetDirection to zero, because it would go through Background
                targetDirection = new Vector3(targetDirection.x, targetDirection.y, 0);


                angle = Vector3.Angle(targetDirection, transform.up);



                //angle is always positiv. If we are moving to right, than the angle is negativ. with the clock-direction
                if (targetDirection.x > transform.position.x)
                {
                    angle = -(angle);

                }

            //change rotation of the rocket but without lerp
            //transform.Rotate(0.0f, 0.0f, angle);
            //dont shoot above 90 grad left and right

                if (angle < 90.0f && angle > -90.0f)
                {
                    go = true;
                }

            }



            if (go == true)
            {

                //change position of the rocket
                //transform.GetComponent<Rigidbody2D>().velocity = targetDirection * rocketSpeed;
                transform.position += targetDirection.normalized * rocketSpeed * Time.deltaTime; //AddForce(targetDirection * rocketSpeed);

                //Change rotation with rotate_speed
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), Time.deltaTime * rotate_speed);

            }



        if (go == false && multishot_on == true && (left_Rocket.activeSelf == false|| right_Rocket.activeSelf == false))
        {
            left_Rocket.SetActive(true);
            right_Rocket.SetActive(true);

        }
        
    }

    
    
    // asteroids works with collision -> Rigidbody2d 
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (PiercingUpgrade_on == false)
        {
            if (go == true && (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Asteroid2") || collision.gameObject.tag == "Asteroid3")
            {

                //Destroy(this.gameObject);

                Rocket_renderer.enabled = false;
                Rocket_collider.enabled = false;

                cool_down_now = true;
            }
        }
    }

    // ufo works with trigger. destroy ufo here. 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PiercingUpgrade_on == false)
        {
            if (go == true && collision.gameObject.tag == "Ufo")
            {

                Rocket_renderer.enabled = false;
                Rocket_collider.enabled = false;

                //Destroy(collision.gameObject);

                cool_down_now = true;
            }
        }

        if(PiercingUpgrade_on == true)
        {
            if (go == true && collision.gameObject.tag == "Ufo")
            {

                Destroy(collision.gameObject);
                
            }
        }
    }


    void OnBecameInvisible()
    {
        //Destroy(gameObject);

        Rocket_renderer.enabled = false;
        Rocket_collider.enabled = false;

        cool_down_now = true;
    }


    public void enable_speed_upgrade(float time, float value)
    {
        Timer_speed_upgrade += time;
        rocketSpeed = value;
        GetComponent<Rocket_cool_down>().cooldownCounter = 0;
        GetComponent<Rocket_cool_down>().backup_cooldownCounter = 0;

    }

    public void enable_piercing_upgrade(float time)
    {
        Timer_piercing_upgrade += time;
        PiercingUpgrade_on = true;
    }

    public void enable_multishot_upgrade(float time)
    {

        Timer_multishot_upgrade += time;


        left_Rocket.SetActive(true);
        left_Rocket_Stand.SetActive(true);
        right_Rocket.SetActive(true);
        right_Rocket_Stand.SetActive(true);
        multishot_on = true;
    }
}




