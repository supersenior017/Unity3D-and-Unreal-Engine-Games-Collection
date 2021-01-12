using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_sec : MonoBehaviour
{
    // Start is called before the first frame update




    public float destroy_time;




    void Start()
    {

        Destroy(gameObject, destroy_time);
    }

}
