using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_cool_down : MonoBehaviour
{

    public float cooldownCounter;
    Vector3 backup_transform;
    Quaternion backup_rotation;


    public float backup_cooldownCounter;


    // Start is called before the first frame update
    void Start()
    {
        backup_transform = GetComponent<Transform>().position;
        backup_rotation = GetComponent<Transform>().rotation;
        backup_cooldownCounter = cooldownCounter;
    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<Rocket_start>().cool_down_now == true)
        {
            cooldownCounter -= Time.deltaTime;
            if (cooldownCounter <= 0)
            {
                cooldownCounter = backup_cooldownCounter;
                transform.position = backup_transform;
                transform.rotation = backup_rotation;
                transform.GetComponent<Renderer>().enabled = true;
                transform.GetComponent<CapsuleCollider2D>().enabled = true;

                GetComponent<Rocket_start>().cool_down_now = false;
                GetComponent<Rocket_start>().go = false;
              
            }

        }
    }
}
