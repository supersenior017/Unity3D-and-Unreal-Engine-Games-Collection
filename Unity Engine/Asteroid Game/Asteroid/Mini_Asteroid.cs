using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Asteroid : MonoBehaviour
{

    public GameObject Explosion;

    private GameObject Stat_mini_Asteroids_destroyed;



    // Start is called before the first frame update
    void Start()
    {
        // child von parent trennen
        transform.parent = null;
        Stat_mini_Asteroids_destroyed = GameObject.FindWithTag("Stats");

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.gameObject.tag == "Ground")
        {

            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }


        if (collision.gameObject.tag == "Rocket" || collision.gameObject.tag == "Rocket_Side")
        {

            Stat_mini_Asteroids_destroyed.GetComponent<Statistics_this_level>().Mini_Asteroids++;

            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mini_Rocket")
        {

            Stat_mini_Asteroids_destroyed.GetComponent<Statistics_this_level>().Mini_Asteroids++;

            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }

    }
    void OnBecameInvisible()
        {
            Destroy(this.gameObject);
        }
}
