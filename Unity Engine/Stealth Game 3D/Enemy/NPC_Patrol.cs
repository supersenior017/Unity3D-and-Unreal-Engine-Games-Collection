using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Patrol : MonoBehaviour
{
    public float speed;
    public Transform[] moveSpots;
    public float startWaitTime;

    private int moveToSpot;
    private float waitTime;

    private Vector3 direction;
    private bool directionCalculated = false;

    private void Start()
    {
        waitTime = startWaitTime;
        moveToSpot = 0;
    }

    private void Update()
    {        
        if(directionCalculated == false)
        {
            direction = (moveSpots[moveToSpot].position - this.transform.position);
            directionCalculated = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, moveSpots[moveToSpot].position, speed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, direction, Time.deltaTime * 10, 0.0f));

        if (Vector3.Distance(transform.position, moveSpots[moveToSpot].position) < 0.2f)
        {
            directionCalculated = false;
            if (waitTime <= 0)
            {
                if (moveToSpot == moveSpots.Length - 1)
                {
                    moveToSpot = 0;
                }
                else
                {
                    moveToSpot += 1;
                }
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
                          
        }
    }
}
