using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Ghostly startet links im Menü
 * Ghostly springt auf den Exit Button
 * Ghostly trifft auf Cloudy
 * Ghostly rennt vor Cloudy nach links weg
 * Ghostly rennt hinter den Exit Button
 * Ghostly schaut schräg hinter Exit Button hervor
 * 
 * 
 * 
 */

public class Menu_Ghosty : MonoBehaviour
{
    public GameObject Ghostly_Menu;

    float speed = 3;
    bool destination = true; //true = right, false = left
    bool will_jump = true; //Ghostly will jump at Exit-Button

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x_position = Ghostly_Menu.transform.position.x;
        float y_position = Ghostly_Menu.transform.position.y;

        //Position of Ghostly. If higher than 4 then turn left (false)
        if (x_position >= 4)
        {
            destination = false;
        }        
        //Position of Ghostly. If lower than -8 then turn right (true)
        if(x_position <= -8)
        {
            destination = true;
        }

        //Jump on the Exit-Button
        if (x_position >= -1.2 && x_position <= 0.9 && will_jump == true)
        {
            Ghostly_Menu.transform.Translate(Vector3.up * 7 * Time.deltaTime);
        }



        //Ghostly moves right
        if (destination == true)
        {
            Ghostly_Menu.transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        //Ghostly moves left
        if(destination == false)
        {
            Ghostly_Menu.transform.Translate(Vector3.left * speed * Time.deltaTime);
        }           
    }
}
