using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade4_Geschütz_Attack : MonoBehaviour
{

    public ParticleSystem ShockWaveSmall;

    Shoot_close_objects script;
    public float geschütz_time;
    public float geschütz_value;

    private GameObject Geschütz;


    void Start()
    {
        Geschütz = GameObject.FindWithTag("Geschütz");

    }


    void Update()
    {

        gameObject.transform.position += Vector3.down * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rocket")
        {
            Instantiate(ShockWaveSmall, gameObject.transform.position, Quaternion.identity);


            script = Geschütz.GetComponent<Shoot_close_objects>();
            script.enable_geschütz_energy_upgrade(geschütz_value, geschütz_time);

            Destroy(this.gameObject);


        }

        if (collision.gameObject.tag == "Rocket_Side")
        {
            Instantiate(ShockWaveSmall, gameObject.transform.position, Quaternion.identity);

            script = Geschütz.GetComponent<Shoot_close_objects>();

            script.enable_geschütz_energy_upgrade(geschütz_value, geschütz_time);

            Destroy(this.gameObject);

        }
    }





    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
