using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunderbolt_Destroy : MonoBehaviour
{
    //time for destroying thunderbolt
    public float time;

    public int damage = 20;
    public float force = 2000;
    //public AudioSource AttackSound;
    public int max_enemies = 1;   // Standard geht es nur durch ein Monster durch. Durch Upgrade erhöhbar

    private int actual_enemies = 0;

    GameObject ghosty;


    // Start is called before the first frame update
    void Start()
    {
        ghosty = GameObject.FindGameObjectWithTag("Player");

        Destroy(gameObject, time);
    }

    void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.gameObject.tag == "Enemy" && actual_enemies < max_enemies + 1 && collider2d.isTrigger == false)
        {

            actual_enemies++;

            if (actual_enemies == max_enemies + 1)
            {

                Destroy(gameObject);
            }


            // target.GetComponent<Enemy_Health>().getHit(-damage);


            if (actual_enemies <= max_enemies)
            {

                collider2d.GetComponent<Enemy_Health>().getHit(-damage);



                // AttackSound.Play();


                if (collider2d.GetComponent<Transform>().position.x > ghosty.transform.position.x)
                {
                   // collider2d.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    collider2d.GetComponent<Rigidbody2D>().AddForce(transform.right * force);
                }
                else
                {
                    //collider2d.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    collider2d.GetComponent<Rigidbody2D>().AddForce(transform.right * -force);
                }

            }

        }
    }
}
