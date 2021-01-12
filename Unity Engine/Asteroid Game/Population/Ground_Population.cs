using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ground_Population : MonoBehaviour
{
    public Text canvas_pop;
    public float population_float;

    public float Asteroid_30x30_damage;
    public float Asteroid_40x40_damage;
    public float Asteroid_60x60_damage;
    public float Asteroid_mini_damage;

    public GameObject lose_menu;


    public float Ufo_damage;

    string population_string;

    private bool ufo_active = false;



    // Start is called before the first frame update
    void Start()
    {

        population_string = population_float.ToString("n0");
        canvas_pop.text = population_string;

    }

    // Update is called once per frame
    void Update()
    {
        if(ufo_active == true && GameObject.FindGameObjectsWithTag("Ufo").Length >0)
        {

            population_float = population_float - Ufo_damage;

            if (population_float <= 0)
            {
                The_End();
            }


            population_string = population_float.ToString("n0");

            canvas_pop.text = population_string;


        }

        else
        {
            ufo_active = false;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            population_float = population_float - Asteroid_30x30_damage;

            if(population_float <= 0)
            {
                The_End();
            }

            population_string = population_float.ToString("n0");

            canvas_pop.text = population_string;
        }


        if (collision.gameObject.tag == "Asteroid2")
        {

            population_float = population_float - Asteroid_40x40_damage;

            if (population_float <= 0)
            {
                The_End();
            }

            population_string = population_float.ToString("n0");

            canvas_pop.text = population_string;
        }


        if (collision.gameObject.tag == "Asteroid3")
        {

            population_float = population_float - Asteroid_60x60_damage;

            if (population_float <= 0)
            {
                The_End();
            }

            population_string = population_float.ToString("n0");

            canvas_pop.text = population_string;
        }



        if (collision.gameObject.tag == "Mini_Asteroid")
        {

            population_float = population_float - Asteroid_30x30_damage;

            if (population_float <= 0)
            {
                The_End();
            }

            population_string = population_float.ToString("n0");

            canvas_pop.text = population_string;

        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Ufo_Ray")
        {

            ufo_active = true;

        }
    }

   

    void The_End()
    {
        population_float = 0;
        population_string = population_float.ToString("n0");
        canvas_pop.text = population_string;

        Time.timeScale = 0.0f;
        lose_menu.SetActive(true);
        lose_menu.GetComponentInChildren<Text>().text = "Apocalypse Now";
    }
}
