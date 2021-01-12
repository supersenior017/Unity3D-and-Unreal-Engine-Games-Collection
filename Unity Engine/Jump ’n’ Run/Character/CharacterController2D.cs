using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.      
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
   

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;



    //private Animator myAnimator;


    private ParticleSystem particlesys_white;
    private ParticleSystem particlesys_black;
    private ParticleSystem particlesys_yellow;



    CapsuleCollider2D Ghosty_Collider;


    private float jumpTimeCounter;
    public float jumptime;


    public bool isPooling = false;


    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }


    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        // myAnimator = GetComponent<Animator>();

        particlesys_white = transform.Find("White_Particle").gameObject.GetComponent<ParticleSystem>();
        particlesys_black = transform.Find("Black_Particle").gameObject.GetComponent<ParticleSystem>();
        particlesys_yellow = transform.Find("Yellow_Particle").gameObject.GetComponent<ParticleSystem>();

        Ghosty_Collider = GetComponent<CapsuleCollider2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    private void FixedUpdate()
    {

        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool crouch, bool jump)
    {

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }


        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.

            m_Rigidbody2D.velocity = new Vector2(0, 0);
            jumpTimeCounter = jumptime;
         
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));  //240 ist gut ohne velocity 0 zu setzen

        
            GetComponent<PlayerMovement>().isJumping = true;
            // m_Velocity = Vector2.up * m_JumpForce;
        }

        if (jump && GetComponent<PlayerMovement>().isJumping)
        {

        
            if (jumpTimeCounter > 0)
            {
               
                 m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

             
                jumpTimeCounter -= Time.deltaTime;
               // m_Rigidbody2D.transform.localScale = new Vector2(transform.localScale.x , 0.85f);

            }

            else
            {

                GetComponent<PlayerMovement>().isJumping = false;




            }
        }


        if(!m_Grounded && m_Rigidbody2D.velocity.y <0 && !crouch)     // Ghosty goes down faster
        {

            m_Rigidbody2D.AddForce(new Vector2(0, -50));
            m_Rigidbody2D.transform.localScale = new Vector2(transform.localScale.x, 1);



        }


        if (m_Grounded && crouch)
        {
            Ghosty_Collider.enabled = false;

            particlesys_white.startLifetime = 20;
            particlesys_white.gravityModifier = 100;
            particlesys_white.loop = false;
            particlesys_white.emissionRate = 0;
            particlesys_white.playbackSpeed = 0.2f;

            particlesys_black.startLifetime = 20;
            particlesys_black.gravityModifier = 100;
            particlesys_black.loop = false;
            particlesys_black.emissionRate = 0;
            particlesys_black.playbackSpeed = 0.2f;

            particlesys_yellow.gravityModifier = 100;
            particlesys_yellow.startLifetime = 20;
            particlesys_yellow.loop = false;
            particlesys_yellow.emissionRate = 0;
            particlesys_yellow.playbackSpeed = 0.2f;

            /*

            myAnimator.SetBool("crouch_active", crouch);
            myAnimator.SetBool("crouch_end", crouch);



            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ghosty_pool_animation") &&
            myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                myAnimator.SetBool("crouch_active_stay", crouch);
                m_Rigidbody2D.transform.localScale = new Vector2(transform.localScale.x, 1);
            }



            if (myAnimator.GetBool("crouch_active")== true)
            {

               Ghosty_Collider.enabled = false;
            }

*/




        }


        if (m_Grounded && !crouch)
        {
            Ghosty_Collider.enabled = true;

            Debug.Log(particlesys_white.particleCount);


           if(particlesys_white.particleCount < 3000)
            {
                particlesys_white.playbackSpeed = 10;
                    particlesys_black.playbackSpeed = 10;
                particlesys_yellow.playbackSpeed = 10;


            }

            else
            {

                particlesys_white.playbackSpeed = 1;
                particlesys_black.playbackSpeed = 1;
                particlesys_yellow.playbackSpeed = 1;

            }



            particlesys_white.startLifetime = 3;
            particlesys_white.gravityModifier = 0;
            particlesys_white.loop = true;
            particlesys_white.emissionRate = 1000;
            //particlesys_white.playbackSpeed = 1;


            particlesys_black.startLifetime = 3;
            particlesys_black.gravityModifier = 0;
            particlesys_black.loop = true;
            particlesys_black.emissionRate = 1000;
          // particlesys_black.playbackSpeed = 1;


            particlesys_yellow.startLifetime = 1;
            particlesys_yellow.gravityModifier = 0;
            particlesys_yellow.loop = true;
            particlesys_yellow.emissionRate = 100;
           // particlesys_yellow.playbackSpeed = 1;


            /*
            m_Rigidbody2D.transform.localScale = new Vector2(transform.localScale.x, 1);

            Ghosty_Collider.enabled = true;


            myAnimator.SetBool("crouch_active", crouch);

            myAnimator.SetBool("crouch_active_stay", crouch);

            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ghosty_pool_animation_invert") &&
           myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                myAnimator.SetBool("crouch_end", crouch);
            }

           */

        }


        // experimental *****************
        if (!m_Grounded && crouch)
        {



           // m_Rigidbody2D.transform.localScale = new Vector2(transform.localScale.x, 0.6f);
           


        }

    }

  


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }




}




/*

using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Kraft zum springen. 700 ist eine gute Zahl
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // geducktes laufen 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // Bewegung weich machen
    [SerializeField] private bool m_AirControl = false;                         // Luft bewegen?
    [SerializeField] private LayerMask m_WhatIsGround;                          // Maske für was alles Boden für den Character darstellt
    [SerializeField] private Transform m_GroundCheck;                           //Position an Beinen.
    [SerializeField] private Transform m_CeilingCheck;                          // Position über den Kopf
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // Welche Collider soll entfernt werden, wenn geduckt?

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;




    public GameObject boomer;

    public GameObject shield_back;
    public GameObject shield_slide;


    private Animator myAnimator;


    //for shield visibility

    public bool slide_on = false;  //more for animation

    public bool throw_on = true;

    public bool sliding_on = true;



    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;


    private void Awake()
    {
  

        m_Rigidbody2D = GetComponent<Rigidbody2D>();

      
        myAnimator = GetComponent<Animator>();  //Referenz

       

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }


    public void Move(float move, bool crouch, bool jump, bool slideRight, bool slideLeft, bool knife, bool throwing)
    {

       

        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
               crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {



            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                //move *= m_CrouchSpeed;  //testzwecken deaktiviert

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }



            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);



            // if Character is on ground and moving is + or - something. Than activate run animation
            if (m_Grounded == true)
            {

                myAnimator.SetFloat("positiv_speed", move);


                if (move < 0)
                {

                    myAnimator.SetFloat("positiv_speed", -move);
                }



                // Messer animation

                myAnimator.SetBool("positiv_knife", knife);

            }

            // disable run animation.

            else
            {

                myAnimator.SetFloat("positiv_speed", 0f);

            }



            // If the input is moving the player right and the player is facing left...
            if (move > 0 &&   !m_FacingRight ) 
            {
                // ... flip the player.
                Flip();



            }
            // the player is moving left and the player is facing right
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                  Flip();

               

            }
        }



        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));


        }

        if (!m_Grounded)
        {


            //jump animation

            myAnimator.SetTrigger("positiv_jump");

            myAnimator.SetLayerWeight(1, 1);

            myAnimator.SetBool("positiv_knife", knife);
        }

        else
        {

            myAnimator.SetBool("positiv_jump", jump);
            myAnimator.SetLayerWeight(1, 0);

        }


        //throwing shield

       

        if ((m_Grounded || !m_Grounded) && throwing && !slide_on && throw_on )
        {
            shield_back.GetComponent<Renderer>().enabled = false;

          
            //wenn kein shield_throw im scene ist

            GameObject clone;

            clone = Instantiate(boomer, new Vector2(transform.position.x, transform.position.y-1 ), transform.rotation) as GameObject;
            throw_on = false;



            Invoke("disUnlimited", 1.5f);
           

        }
        /*

        else {

            throw_on = false;
        }
      */

// Sliden nach rechts


/*
 * 
 * 

if ((m_Grounded || !m_Grounded) && slideRight && sliding_on && throw_on )
{

    shield_slide.GetComponent<Renderer>().enabled = true;
    shield_back.GetComponent<Renderer>().enabled = false;


    m_AirControl = false;
    myAnimator.SetBool("positiv_slide", true);

    slide_on = true;
    sliding_on = false;


    if (m_Grounded) {   // am Boden

        m_Rigidbody2D.gravityScale = 0;

        m_Rigidbody2D.velocity = Vector2.right * 10;

        // m_Rigidbody2D.AddForce(new Vector2(m_JumpForce*2, 0));


        Invoke("gravity", 0.5f);

        Invoke("disUnlimited_slide", 2f);


    }



    // m_Rigidbody2D.AddForce(new Vector2(m_JumpForce, 0)); // Kraft nach rechts


    m_Rigidbody2D.gravityScale = 0;
    m_Rigidbody2D.velocity = Vector2.right * 10;


    /*
     * superPlayer bei what is Ground entfernen
     * 
    if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround)) // falls es an böden trifft. bleibt es stehen.
    {
        m_Rigidbody2D.velocity = Vector2.zero;
    }

*/

/*

            Invoke("gravity", 0.5f); //warten Funktion bis Gravity aufgerufen wird
            Invoke("disUnlimited_slide", 2f);

            if (!m_FacingRight)
            {

                Flip();
            }


        }

        // Sliden nach links

        if ((m_Grounded || !m_Grounded) && slideLeft && sliding_on && throw_on)
        {
            shield_slide.GetComponent<Renderer>().enabled = true;
            shield_back.GetComponent<Renderer>().enabled = false;
            slide_on = true;
            sliding_on = false;


            m_AirControl = false;
            myAnimator.SetBool("positiv_slide", true);


            if (m_Grounded)  // am Boden
            {


                m_Rigidbody2D.gravityScale = 0;
                m_Rigidbody2D.velocity = Vector2.left * 10;


                //   m_Rigidbody2D.AddForce(new Vector2(-m_JumpForce * 2, 0));


                Invoke("gravity", 0.5f);

                Invoke("disUnlimited_slide", 2);


            }


            m_Rigidbody2D.gravityScale = 0;
            m_Rigidbody2D.velocity = Vector2.left * 10;



           // m_Rigidbody2D.AddForce(new Vector2(-m_JumpForce, 0)); // kraft nach links


            /*
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround)) // Falls mit Böden in Kontakt kommt. v = 0
            {
                m_Rigidbody2D.velocity = Vector2.zero;
            }
            */


/*
Invoke("gravity", 0.5f);
Invoke("disUnlimited_slide", 2f);



if (m_FacingRight)
{

    Flip();
}
}
}


// Flip den Character.
private void Flip()
{

m_FacingRight = !m_FacingRight;

//transform x scale mit minus 1
Vector3 theScale = transform.localScale;
theScale.x *= -1;
transform.localScale = theScale;
}


private void gravity()
{
shield_back.GetComponent<Renderer>().enabled = true;
shield_slide.GetComponent<Renderer>().enabled = false;
m_Rigidbody2D.velocity = Vector2.zero; // hier bleibt Rigidbody stehen

m_AirControl = true;

m_Rigidbody2D.gravityScale = 3;

//m_Rigidbody2D.AddForce(new Vector2(0, -m_JumpForce)); // nach unten mit m_jumpforce

slide_on = false;
myAnimator.SetBool("positiv_slide", false);


}


private void disUnlimited() {

shield_back.GetComponent<Renderer>().enabled = true;
throw_on = true;


}

private void disUnlimited_slide() {

sliding_on = true;

}

}



*/




