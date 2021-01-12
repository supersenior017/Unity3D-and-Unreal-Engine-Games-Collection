using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class canvas_text : MonoBehaviour
{

    public Load_Statistics_Scene population_value;
    public GameObject canvas;


    double population_double;
    string population_string;




    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
        population_value = GetComponent<Load_Statistics_Scene>();

        population_double = population_value.population_death;

        population_string = population_double.ToString("n0");



        canvas.GetComponentInChildren<Text>().text = population_string;
    }
}
