using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_start_lite : MonoBehaviour
{

    private GameObject Rocket;


    // Start is called before the first frame update
    void Start()
    {
        Rocket = GameObject.FindWithTag("Rocket");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(Rocket.GetComponent<Rocket_start>().PiercingUpgrade_on == false){
            if (Rocket.GetComponent<Rocket_start>().go == true && (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "Asteroid2"))
            {

                gameObject.SetActive(false);
            
            }
        }
    }

    // ufo works with trigger. destroy ufo here
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Rocket.GetComponent<Rocket_start>().PiercingUpgrade_on == false)
        {
            if (collision.gameObject.tag == "Ufo")
            {

                gameObject.SetActive(false);
                Destroy(collision.gameObject);


            }
        }
    }
}
