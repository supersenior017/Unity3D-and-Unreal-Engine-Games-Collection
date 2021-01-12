using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pool : MonoBehaviour
{
    //For Pause
    public GameObject PauseObject;

    //for Cooldown
    public Transform Pool_Cooldown_Image;
    public bool Cooldown_Activated = false;


    private Vector3 offset;

    GameObject target;
    GameObject cloudy;

    public GameObject pool_particel;

    Vector3 pool_transform;

    public bool isPool = false;
    private bool is_active = false;

    static int i = 0;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        cloudy = GameObject.FindGameObjectWithTag("Cloudy");


        offset = pool_particel.transform.position - target.transform.position;



    }

    // Update is called once per frame
    void Update()
    {

    

        if (Pool_Cooldown_Image.GetComponent<Image>().fillAmount == 1 && PauseObject.GetComponent<Pause>().pause == false) //Cooldown fertig? && Pause deaktiviert?
        {

            is_active = true;


        }

        if(pool_particel.active == true && Cooldown_Activated == true)
        {
           pool_particel.transform.Translate(Vector2.up *15 * Time.deltaTime);

        }

        if (Vector2.Distance(pool_particel.transform.position, cloudy.transform.position) < 5)
        {

            pool_particel.active = false;


            Debug.Log(i);

            cloudy.GetComponent<Cloudy_Energy>().energy = cloudy.GetComponent<Cloudy_Energy>().energy + i;
            if (cloudy.GetComponent<Cloudy_Energy>().energy > 100)
            {

                cloudy.GetComponent<Cloudy_Energy>().energy = 100;
            }
            i = 0;

            pool_particel.transform.position = target.transform.position + offset;
        }



    }


    private void OnTriggerStay2D(Collider2D collision)
    {

           if (collision.gameObject.tag == "Enemy" && is_active == true && Cooldown_Activated == false && target.GetComponent<CharacterController2D>().m_Grounded == true && target.GetComponent<PlayerMovement>().crouch == true)
           {

            isPool = true;
            pool_particel.active = true;

          
            for (; i < 20; i++)
            {

               
                pool_particel.GetComponent<ParticleSystem>().maxParticles = i;
                if (collision.GetComponent<Enemy_Health>().health > 0)
                {

                    collision.GetComponent<Enemy_Health>().getHit(-1);

                   
                }

                else
                {
                    break;
                }


            }

        }

     
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
    
        if (collision.gameObject.tag == "Enemy" && is_active == true  && target.GetComponent<CharacterController2D>().m_Grounded == true && target.GetComponent<PlayerMovement>().crouch == true)
        {
            //cloudy.GetComponent<Cloudy_Energy>().energy = cloudy.GetComponent<Cloudy_Energy>().energy + i;

            Cooldown_Activated = true;
           // i = 0;

        }

        if(i > 0)
        {

            Cooldown_Activated = true;

        }

    }


}
