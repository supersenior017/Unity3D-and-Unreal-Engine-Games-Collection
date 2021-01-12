using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_Rotation : MonoBehaviour
{
    public float rotationSpeed = 50.0f;

    private float randomDirection;

    void Start()
    {
        //Erstelle zufallszahl 0 oder 1, damit Asteroid sich zufällig im oder gegen Uhrzeigersinn dreht
        randomDirection = Mathf.RoundToInt(Random.Range(0, 2));
    }

    void Update()
    {
        //Wenn Zufallszahl = 0 rotiere den Asteroid im Uhrzeigersinn, ansonsten gegen den Uhrzeigersinn
        if(randomDirection == 0)
        {
            gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);
        }
        else
        {
            gameObject.transform.Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
        }
    }
}
