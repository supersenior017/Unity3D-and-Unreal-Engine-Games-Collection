using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowly_Active : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

       
    }


    void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.gameObject.tag == "Enemy")
        {

            //collider2d.GetComponent<Enemy_Follow>().speed = 
           

        }

    }


    void OnTriggerStay2D(Collider2D collider2d)
    {
        if (collider2d.gameObject.tag == "Enemy")
        {

            Time.timeScale = 0.7f;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F;
    }




}