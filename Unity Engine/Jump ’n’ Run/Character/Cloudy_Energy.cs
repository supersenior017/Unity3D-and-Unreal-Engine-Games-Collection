using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cloudy_Energy : MonoBehaviour
{
    public float energy;

    private float energy_for_bar;   // for scaling

    public RectTransform energyBar;


    private Vector3 thickness;


    // Start is called before the first frame update
    void Start()
    {
        energy_for_bar = energy;

        thickness = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        energyBar.localScale = new Vector3(energy/energy_for_bar, 1, 1);

        transform.localScale = new Vector3((thickness.x-0.1f) + (0.1f*(energy/100)), (thickness.x - 0.1f) + (0.1f * (energy / 100)), thickness.z);

    }

    public float getEnergy()
    {
        return energy;
    }


    // ******************************************
    // death
    // ******************************************
    // Called, when players health is 0.
    // ******************************************
    // arguments:
    // - int damage
    // ******************************************
    // return value:
    //  - bool
    // ******************************************
    private void out_of_energy()
    {
        print("not enough Energy for using Ability");
    }


    // ******************************************
    // getHit
    // ******************************************
    // Called, when player gets hit by an enemy.
    // Edit the health value. Returns FALSE, when success
    // ******************************************
    // arguments:
    // - int damage
    // ******************************************
    // return value:
    //  - bool
    // ******************************************

    public bool use_ability(int energy_amount)
    {
        energy += energy_amount;

        if (energy <= 0)
        {
            energy = 0;
            out_of_energy();
        }
        return false;
    }
}