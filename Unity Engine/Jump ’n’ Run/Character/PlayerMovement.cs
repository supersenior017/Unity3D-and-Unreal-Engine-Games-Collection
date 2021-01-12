
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    public bool isJumping = false;
    public bool crouch = false;

    

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
           
        }

        if (Input.GetButtonUp("Jump"))
        {

          
            jump = false;
            isJumping = false;
        }


        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }


    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);

       
    }
}







/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerMovement : MonoBehaviour {


	public CharacterController2D controller;


   
	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
    bool slideRight = false;
    bool slideLeft = false;


    bool throwing = false;

    public bool knife = false;


    // doppel Klick Variablen
    bool one_click = false;
    float dclick_threshold = 0.25f;
    double timerdclick = 0;


    // Update is called once per frame
    void Update () {


        // Doppel Klick Mechanismus
        if (one_click && ((Time.time - timerdclick) > dclick_threshold))
        {
       
            one_click = false;
        }
       
        if (Input.GetKeyDown("right") || Input.GetKeyDown("left"))  //rechte oder linke taste erst in Funktion rein
        {
            
            if (!one_click)
            {
                dclick_threshold = 0.15f;
                timerdclick = Time.time;
                one_click = true;

            //    Debug.Log(timerdclick);

              
            }

            else if (one_click && ((Time.time - timerdclick) < dclick_threshold))   //Doppelclick in der Zeit ??
            {
               // Debug.Log("double click");

                if (Input.GetKeyDown("left"))
                {
                    slideLeft = true;
                }
                else {
                    slideRight = true;
                }

                one_click = false;
            }

        }


        // Alle Eingaben hier

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;

		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

        if (Input.GetButtonDown("Fire1"))
        {
            knife = true;

        }
        else if (Input.GetButtonUp("Fire1"))
        {
            knife = false;
        }

        if(Input.GetButtonDown("Fire2")) 
        {

            throwing = true;
        }



        /*
          //kann weg. Durch doppel klick ersetzt
        if (Input.GetKey("k"))
        {
           slideRight = true;


        }

        if (Input.GetKey("j"))
        {
            slideLeft = true;

        }
        */


//}
    /*

    void FixedUpdate ()
	{
		// hier wird das umgesetzt. man greift auf controller zu und dessen Funktion Move. Danach Parameter off setzen. sonst springt und slidet es die ganze Zeit
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, slideRight, slideLeft, knife, throwing );
        jump = false;
        slideRight = false;
        slideLeft = false;
        throwing = false;


	}
}
*/