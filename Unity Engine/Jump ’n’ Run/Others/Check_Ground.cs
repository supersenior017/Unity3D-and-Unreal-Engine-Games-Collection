using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check_Ground : MonoBehaviour
{
    [SerializeField]
    public GameObject DustCloud;
    public GameObject WalkCloud;


    GameObject ghosty;
    private Rigidbody2D rb;

    bool coroutineAllowed;
    bool grounded;


    private void Start()
    {
        ghosty = GameObject.FindGameObjectWithTag("Player");


    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.tag.Equals("Groundwalk")){

            grounded = true;
            coroutineAllowed = true;
            Instantiate(DustCloud, transform.position, DustCloud.transform.rotation);
        }


    }

    /*

    private void OnTriggerStay2D(Collider2D col)
    {

        if (col.gameObject.tag.Equals("Groundwalk"))
        {

            Instantiate(WalkCloud, transform.position, WalkCloud.transform.rotation);


        }

    }

    */


    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.gameObject.tag.Equals("Groundwalk"))
        {
            grounded = false;

            coroutineAllowed = false;


        }
    }

    void Update()
    {

        if(coroutineAllowed && ghosty.GetComponent<Rigidbody2D>().velocity.x != 0 && grounded)
        {

            StartCoroutine("SpawnCloud");
            coroutineAllowed = false;

        }

        if (ghosty.GetComponent<Rigidbody2D>().velocity.x == 0 || !grounded)
        {

            StopCoroutine("SpawnCloud");
            coroutineAllowed = true;

        }


    }


    IEnumerator SpawnCloud()
    {

        while (grounded)
        {

            Instantiate(WalkCloud, transform.position, WalkCloud.transform.rotation);
            yield return new WaitForSeconds(0.25f);
        }

    }


}
