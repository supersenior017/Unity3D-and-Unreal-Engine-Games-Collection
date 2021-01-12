using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class visible : MonoBehaviour
{

    public float time = 0f;



    void Start()
    {
        GetComponent<Text>().enabled =false;

       StartCoroutine(ActiveCoroutine());
    }





    IEnumerator ActiveCoroutine()
    {
       
        yield return new WaitForSeconds(time);
        GetComponent<Text>().enabled = true;

    }


   
}
