using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Background : MonoBehaviour
{
    public GameObject Menu_Background_1;
    public GameObject Menu_Background_2;
    public float Speed_Background_1;
    public float Speed_Background_2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Rotate Background1 clockwise
        Menu_Background_1.transform.Rotate(Vector3.back * Speed_Background_1 * Time.deltaTime);
        //Rotate Background2 clockwise
        Menu_Background_2.transform.Rotate(Vector3.back * Speed_Background_2 * Time.deltaTime);
    }
}



