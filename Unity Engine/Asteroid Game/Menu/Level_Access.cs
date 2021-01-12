using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Access : MonoBehaviour
{

    public Load_Statistics_Scene actual_level;



    public Button[] buttons;
    // Start is called before the first frame update

    void Start()
    {

        actual_level.GetComponent<Load_Statistics_Scene>();

 

       
        
    }

    // Update is called once per frame
    void Update()
    {

        if(actual_level.World_1_level_actual == 0)
        {

            buttons[0].image.color = Color.cyan;
            buttons[0].interactable = true;

        }


        if (actual_level.World_1_level_actual> 0 && actual_level.World_1_level_actual < 24)
        {
            for (int i = 0; i < actual_level.World_1_level_actual; i++)
            {

                buttons[i].image.color = Color.cyan;
                buttons[i].interactable = true;

            }
        }
    }
}
