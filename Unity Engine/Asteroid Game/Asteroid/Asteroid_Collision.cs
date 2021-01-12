using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Collision : MonoBehaviour
{
    public GameObject AsteroidPrefab;
    public GameObject Explosion;

    public GameObject Asteroid_Destroy;

    public GameObject Upgrade1AttackSpeed;
    public GameObject Upgrade2MultiShot;
    public GameObject Upgrade3Piercing;


    private GameObject Destroyed_Asteroids;

    private GameObject Stat_Asteroids_destroyed;

    private int HitCounter;

    private void Start()
    {
        GetComponent<CircleCollider2D>().enabled = false;

        Destroyed_Asteroids = GameObject.FindWithTag("Spawn");

        Stat_Asteroids_destroyed = GameObject.FindWithTag("Stats");

        HitCounter = 4;
    }

    // other object need Collider and a specific tag. erweitere hier
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rocket" || collision.gameObject.tag == "Rocket_Side")
        {

            Destroyed_Asteroids.GetComponent<Upgrade_4_Spawn>().actual_destroyed_asteroids++;
            Stat_Asteroids_destroyed.GetComponent<Statistics_this_level>().Asteroids_destroyed++;

            if(gameObject.name == "Asteroid_0(Clone)")
            {
                Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);

                SpawnUpgrade();

                Destroy(this.gameObject);

            }
            if(gameObject.name == "Asteroid_1(Clone)")
            {
                Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);

                Instantiate(Asteroid_Destroy, gameObject.transform.position, Quaternion.identity);

                SpawnUpgrade();

                Destroy(this.gameObject);
            }
            if(gameObject.name == "Asteroid_2(Clone)")
            {
                //wenn mittlerer Asteroid zerstört wurde, spawne links und rechts davon jeweils einen weiteren Asteroiden
                Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
                GameObject g = Instantiate(AsteroidPrefab, new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y, 0), Quaternion.identity);
                GameObject h = Instantiate(AsteroidPrefab, new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y, 0), Quaternion.identity);

                SpawnUpgrade();

                Destroy(this.gameObject);

                g.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
                h.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1);
            }
            if(gameObject.name == "Asteroid_3(Clone)")
            {
                HitCounter -= 1;
                if(HitCounter != 0)
                {
                    gameObject.GetComponent<AudioSource>().Play();
                }

                if(HitCounter == 0)
                {
                    Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);

                    SpawnUpgrade();

                    Destroy(this.gameObject);
                }                
            }
        }

        if (collision.gameObject.tag == "Ground")
        {
            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void SpawnUpgrade()
    {
        int RandomNumber = Random.Range(1, 100);
        
        if(RandomNumber <= 5)
        {
            Instantiate(Upgrade1AttackSpeed, gameObject.transform.position, Quaternion.identity);
        }
        if (RandomNumber > 5 && RandomNumber <= 10)
        {
            Instantiate(Upgrade2MultiShot, gameObject.transform.position, Quaternion.identity);
        }
        if (RandomNumber > 10 && RandomNumber <= 15)
        {
            Instantiate(Upgrade3Piercing, gameObject.transform.position, Quaternion.identity);
        }
    }


    private void OnBecameVisible()
    {
        GetComponent<CircleCollider2D>().enabled = true;
    }
}
