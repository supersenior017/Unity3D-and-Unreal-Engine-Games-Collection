using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Changer : MonoBehaviour
{

    public float Height = 16.9f;

    private GameObject target;
    private GameObject ghosty;
    private GameObject camera;

    private Quaternion rotation;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Cloudy");
        ghosty = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        rotation = transform.rotation;
        rotation.z = rotation.z * -500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            target.GetComponent<Cloudy_Controller>().offsety = Height;

        }

    }


    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

           ghosty.GetComponent<CharacterController2D>().m_Grounded = true;
           //camera.transform.rotation = rotation;
            ghosty.transform.rotation = rotation;


        }


    }
}
