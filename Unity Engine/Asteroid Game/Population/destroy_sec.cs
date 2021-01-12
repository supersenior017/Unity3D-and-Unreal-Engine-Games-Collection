using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy_sec : MonoBehaviour
{

    public float Seconds = 10;

    void Start()
    {
       
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
       
        yield return new WaitForSeconds(Seconds);

    }

}
