using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo_Collision : MonoBehaviour
{
    public GameObject UfoExplosion;

    public GameObject Upgrade1AttackSpeed;
    public GameObject Upgrade2MultiShot;
    public GameObject Upgrade3Piercing;


    private GameObject Stat_Ufo_destroyed;

    void Start()
    {
       
        Stat_Ufo_destroyed = GameObject.FindWithTag("Stats");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Rocket" || collision.gameObject.tag == "Rocket_Side")
        {
            if (gameObject.name == "Ufo(Clone)") 
            {

                Stat_Ufo_destroyed.GetComponent<Statistics_this_level>().Ufos++;


                Instantiate(UfoExplosion, gameObject.transform.position, Quaternion.identity);

                SpawnUpgrade();

                Destroy(this.gameObject);
            }
        }
       
    }

    void SpawnUpgrade()
    {
        int RandomNumber = Random.Range(1, 100);

        if (RandomNumber <= 5)
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
}
