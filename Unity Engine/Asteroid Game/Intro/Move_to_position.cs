using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_to_position : MonoBehaviour
{
    float t;
    Vector3 startPosition;
    public Vector3 target;
    public float timeToReachTarget;


    public bool scaling = false;
    float s;
    Vector3 scale_normal;
    public Vector3 scale_target;
    public float timeToReachScale;



    void Start()
    {
        startPosition = transform.position;

        scale_normal = transform.localScale;
    }
    void Update()
    {
        t += Time.deltaTime / timeToReachTarget;
        transform.position = Vector3.Lerp(startPosition, target, t);

        if (scaling == true)
        {
            s += Time.deltaTime / timeToReachScale;
            transform.localScale = Vector3.Lerp(scale_normal, scale_target, s);
        }




    }


}
