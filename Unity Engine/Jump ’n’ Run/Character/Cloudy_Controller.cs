using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloudy_Controller : MonoBehaviour
{
    public float speed;
    public float distance_active;  // distance when enemys going to attack target
    public float distance_stop;     // stop distance when target is close to the enemy

    public float speed_up;          // distance zum beschleunigen

    public float speed_max;         // distance größer als speed_up (Blitz)

    private float speed_old;

    private Transform target;


    private Vector3 offset;

    public float offsety = 16.9f;

    void Start()
    {




        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        speed_old = speed;

        offset = new Vector3(0, offsety, 0);    // transform.position - target.transform.position;   //24.8 bei 16.9




    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (Vector2.Distance(transform.position, target.position) > distance_stop && Vector2.Distance(transform.position, target.position) < distance_active)
        {

            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(target.position.x, offsety), speed * Time.deltaTime);


            if (Vector2.Distance(transform.position, target.position) > speed_up && Vector2.Distance(transform.position, target.position) < speed_max)

            {
           
                speed = 20;
            }


            if (Vector2.Distance(transform.position, target.position) > speed_max)
            {

                speed = 100;

            }


            if (Vector2.Distance(transform.position, target.position) <speed_up)
            {
                speed = speed_old;

            }

            if (transform.position.y != offsety) {



                    speed = 100;
            }

        }

       

    }
}

