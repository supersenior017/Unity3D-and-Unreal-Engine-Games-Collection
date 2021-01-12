using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_to_scale_only : MonoBehaviour
{

    public bool scaling = false;
    float s;
    Vector3 scale_normal;
    public Vector3 scale_target;
    public float timeToReachScale;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scaling == true)
        {
            s += Time.deltaTime / timeToReachScale;
            transform.localScale = Vector3.Lerp(scale_normal, scale_target, s);
        }


    }
}
