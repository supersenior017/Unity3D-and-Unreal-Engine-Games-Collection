using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    private GameObject target;

    public int damage = 20;

    public int damage_pooling = 50;
    public float force = 20000;

    public float bounce = 9000;


    private bool finishedCoroutine = true;
    private bool finishedDamage_Coroutine = true;
    private bool finishedDamageHead_Coroutine = true;


    public bool isFish;
    public float bouncing_Fish = 200;

    public AudioSource AttackSound;


    public bool is_Soul_Destroyer = false;

    
    private Enemy_Health enemy_health;
    private Rigidbody2D rb2d;

   

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        enemy_health = this.GetComponent<Enemy_Health>();
        rb2d = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {

    }


      // Bei Collision soll bitte -damage LP abgezogen werden. Bei Soul-Destroyer geht collider weg, wenn z.b auf spieler trifft oder Gegner

      private void OnCollisionEnter2D(Collision2D collision)    
       {

            if (finishedDamage_Coroutine && finishedDamageHead_Coroutine)
            {

                if (collision.gameObject.tag == "Player" && !is_Soul_Destroyer)
                {
                    // target.GetComponent<Character_Health>().getHit(-damage);

                    AttackSound.Play();

                    StartCoroutine("cool_down_damage");
                    finishedDamage_Coroutine = false;

                    if (target.GetComponent<Transform>().position.x > transform.position.x)
                    {
                        target.GetComponent<Rigidbody2D>().AddForce(transform.right * force);

                        Debug.Log("berührt - force_right");
                    }
                    else
                    {
                        target.GetComponent<Rigidbody2D>().AddForce(transform.right * -force);

                        Debug.Log("berührt - force_left");
                    }
                }

                else if (collision.gameObject.tag == "Player" && is_Soul_Destroyer)
                {
                    target.GetComponent<Character_Health>().getHit(-damage);
                    GetComponent<EdgeCollider2D>().enabled = false;

                    AttackSound.Play();
                    Destroy(this.gameObject, 4);

                }

                else if (collision.gameObject.tag == "Enemy" && is_Soul_Destroyer)
                {
                    // AttackSound.Play();

                    collision.gameObject.GetComponent<Enemy_Health>().getHit(-damage * 4);
                    GetComponent<EdgeCollider2D>().enabled = false;
                    Destroy(this.gameObject, 4);


                }

            }

            if (collision.gameObject.tag == "Groundwalk" && is_Soul_Destroyer)
            {


                GetComponent<EdgeCollider2D>().enabled = false;

                Destroy(this.gameObject, 4);



            }


            if (!finishedDamage_Coroutine || !finishedDamageHead_Coroutine)
            {

                if (target.GetComponent<Transform>().position.x < transform.position.x)
                {
                    GetComponent<Rigidbody2D>().AddForce(transform.right * 100);


                }

                else
                {
                    GetComponent<Rigidbody2D>().AddForce(transform.right * -100);

                }

            }

            if (collision.gameObject.tag == "Groundwalk" && isFish)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bouncing_Fish), ForceMode2D.Impulse);


            }


        

    }

   

    // Pooling Funktion ( Wenn man geduckt auf die Köpfe der Feinde springt. Über den Feinden gibt ein Collider der getriggert ist.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slowly_area")
        {

           // GetComponent<Enemy_Follow>().speed 

        }


        if (collision.gameObject.tag != "Slowly_area")
        {

            if (collision.gameObject.tag == "Player" && collision.GetComponent<PlayerMovement>().crouch == true && collision.GetComponent<CharacterController2D>().m_Grounded == false && finishedCoroutine)
            {

                AttackSound.Play();

                //enemy_health.getHit(-damage_pooling);

                StartCoroutine("doubleDamageBug");
                finishedCoroutine = false;

                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 15);



                if (target.GetComponent<Transform>().position.x < transform.position.x)            //target.GetComponent<Transform>().localScale.x == 1   
                {

                    //GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                    Debug.Log("Full LP - auf Kopf mit pooling force_right");


                    GetComponent<Rigidbody2D>().AddForce(transform.right * bounce);

                }

                else
                {

                    // GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

                    GetComponent<Rigidbody2D>().AddForce(transform.right * -bounce);

                    Debug.Log("Full LP - auf Kopf mit pooling force_left");


                }

            }


            else if (collision.gameObject.tag == "Player" && collision.GetComponent<PlayerMovement>().crouch == false && collision.GetComponent<CharacterController2D>().m_Grounded == false && finishedDamageHead_Coroutine)
            {



                StartCoroutine("cool_down_damage_head");
                finishedDamageHead_Coroutine = false;

                AttackSound.Play();

                if (target.GetComponent<Transform>().position.x > transform.position.x)
                {
                    target.GetComponent<Rigidbody2D>().AddForce(transform.right * force * 1.2f);

                    Debug.Log("1/2 auf Kopf ohne pooling force_right");
                }
                else
                {
                    target.GetComponent<Rigidbody2D>().AddForce(transform.right * -force * 1.2f);

                    Debug.Log("1/2 auf Kopf ohne pooling force_left");
                }

            }

            if (!finishedCoroutine || !finishedDamageHead_Coroutine)
            {




            }



        }

    }



    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Slowly_area")
        {


        }

        if (collision.gameObject.tag != "Slowly_area")
        {


            if (target.GetComponent<Transform>().position.x < transform.position.x)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.right * 100);



            }

            else
            {
                GetComponent<Rigidbody2D>().AddForce(transform.right * -100);

            }


        }

        
    }




        IEnumerator doubleDamageBug()
    {
        enemy_health.getHit(-damage_pooling);
        yield return new WaitForSeconds(0.2f);
        finishedCoroutine = true;
    

    }



    IEnumerator cool_down_damage()
    {
        target.GetComponent<Character_Health>().getHit(-damage);
        yield return new WaitForSeconds(0.7f);
        finishedDamage_Coroutine = true;


    }

    IEnumerator cool_down_damage_head()
    {
        target.GetComponent<Character_Health>().getHit(-(damage / 2));
        yield return new WaitForSeconds(0.7f);
        finishedDamageHead_Coroutine = true;


    }

}
















/*
 * 
 * 
 * 
 * using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    private GameObject target;


    public int damage = 20;

    public int damage_pooling = 50;
    public float force = 20000;

    public float bounce = 30000;
  // public AudioSource AttackSound;


   public bool is_Soul_Destroyer = false;

    
    private Enemy_Health enemy_health;
    private Rigidbody2D rb2d;



    bool TimerStarted = false;
    private float _timer = 0f;
    public float TimeIWantInSeconds = 1f;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");

        enemy_health = this.GetComponent<Enemy_Health>();
        rb2d = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {

        if (TimerStarted)
        {
            _timer += Time.deltaTime;



            if (_timer >= (TimeIWantInSeconds + 3))
            {

                TimerStarted = false;
                _timer = 0;
            }

        }

    }


        // Bei Collision soll bitte -damage LP abgezogen werden. Bei Soul-Destroyer geht collider weg, wenn z.b auf spieler trifft oder Gegner

      private void OnCollisionEnter2D(Collision2D collision)
       {

        if (_timer <= TimeIWantInSeconds)
        {

            if (collision.gameObject.tag == "Player" && !is_Soul_Destroyer)
            {
                target.GetComponent<Character_Health>().getHit(-damage);

                //AttackSound.Play();

                if (target.GetComponent<Transform>().position.x > transform.position.x)
                {
                    target.GetComponent<Rigidbody2D>().AddForce(transform.right * force);
                }
                else
                {
                    target.GetComponent<Rigidbody2D>().AddForce(transform.right * -force);
                }
            }

            else if (collision.gameObject.tag == "Player" && is_Soul_Destroyer)
            {
                target.GetComponent<Character_Health>().getHit(-damage);
                GetComponent<EdgeCollider2D>().enabled = false;

                // AttackSound.Play();
                Destroy(this.gameObject, 4);

            }

            else if (collision.gameObject.tag == "Enemy" && is_Soul_Destroyer)
            {
                // AttackSound.Play();

                collision.gameObject.GetComponent<Enemy_Health>().getHit(-damage * 4);
                GetComponent<EdgeCollider2D>().enabled = false;
                Destroy(this.gameObject, 4);


            }

            else if (collision.gameObject.tag == "Groundwalk" && is_Soul_Destroyer)
            {
                GetComponent<EdgeCollider2D>().enabled = false;

                Destroy(this.gameObject, 4);

            }

        }

        if (_timer >= TimeIWantInSeconds)
        {

            if (collision.gameObject.tag == "Player" && !is_Soul_Destroyer)
            {

                GetComponent<Rigidbody2D>().velocity = new Vector2(40, 10);
            }
        }



    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!TimerStarted) TimerStarted = true;


        if (_timer <= TimeIWantInSeconds)
        {
            if (collision.gameObject.tag == "Player" && collision.GetComponent<PlayerMovement>().crouch == true)
            {
                Debug.Log(_timer);
                enemy_health.getHit(-damage_pooling);

                // collision.GetComponent<Rigidbody2D>().AddForce(transform.up * bounce/15);
                collision.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 20);


                if (target.GetComponent<Transform>().position.x > transform.position.x)
                {
                    //collision.GetComponent<Rigidbody2D>().velocity = new Vector2(bounce, 12);

                    collision.GetComponent<Rigidbody2D>().AddForce(transform.right * bounce);

                }

                else
                {
                    //  collision.GetComponent<Rigidbody2D>().velocity = new Vector2(-bounce, 20);
                    collision.GetComponent<Rigidbody2D>().AddForce(transform.right * -bounce);

                }

            }

          
        }

        if(_timer > TimeIWantInSeconds)
        {

            if (collision.gameObject.tag == "Player" && collision.GetComponent<PlayerMovement>().crouch == true)
            {

                GetComponent<Rigidbody2D>().velocity = new Vector2(40, 10);

            }

        }







    }
}


*/  
