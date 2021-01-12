using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Destroy : MonoBehaviour
{
    private float counter = 0.0f;

    void Update()
    {
        counter += Time.deltaTime;

        if(counter > 5)
        {
            Destroy(gameObject);
        }
    }
}
