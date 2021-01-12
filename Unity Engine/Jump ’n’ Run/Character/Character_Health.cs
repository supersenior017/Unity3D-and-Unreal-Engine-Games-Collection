using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Health : MonoBehaviour
{
    public float health;

    private float health_for_bar;   // for scaling

    public RectTransform healthBar;


    // Start is called before the first frame update
    void Start()
    {
       

        health_for_bar = health;

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.localScale = new Vector3(health/health_for_bar, 1, 1);
    }

    public float getHealth()
    {
        return health;
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
    private void death()
    {

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

    public bool getHit(int damage)
    {
        health += damage;

        if (health <= 0)
        {
            health = 0;
            death();
        }
        return false;
    }
}
