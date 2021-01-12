using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Game_Save 
{

    public int World_1_level;
    public int World_2_level;

    public float asteroid_destroy;
    public float mini_asteroid_destroy;
    public float Ufo_destroyed;

    public int debugging;


    public double Population;



    //public float[] Highscore;



    public void save_asteroid_destroy(Statistics_this_level stat)
    {

        asteroid_destroy = stat.Asteroids_destroyed;
        mini_asteroid_destroy = stat.Mini_Asteroids;
        Ufo_destroyed = stat.Ufos;

        World_1_level = stat.Level;

        Population = stat.population;

        /*

        for(int i = 0; i < World_1_level; i++)
        {

            Highscore[i] = stat.Highscore[i];
        }

        */


    }



    public void debug()
    {
        debugging = 0;
    }


}
