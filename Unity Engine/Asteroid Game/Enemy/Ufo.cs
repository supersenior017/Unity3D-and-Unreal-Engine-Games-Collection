using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ufo : MonoBehaviour
{
    public float speed = 5f;
    public float timeToFade = 2.0f;
    public GameObject Particles;

    private float RandomStopHorizontal;
    private Vector3 targetPosition;
    private GameObject lightbeam;
    private Color alphaColor;
    private bool canFade;

    // Start is called before the first frame update
    void Start()
    {
        RandomStopHorizontal = Random.Range(-2.0f, 2.0f);
        targetPosition = new Vector3(RandomStopHorizontal, transform.position.y, transform.position.z);
        lightbeam = gameObject.transform.GetChild(0).gameObject;

        canFade = true;
        alphaColor = lightbeam.GetComponent<SpriteRenderer>().material.color;
        alphaColor.a = 150f;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if(transform.position == targetPosition && canFade == true)
        {
            lightbeam.GetComponent<SpriteRenderer>().material.color = Color.Lerp(lightbeam.GetComponent<SpriteRenderer>().material.color, alphaColor, timeToFade * Time.deltaTime * 0.1f);
            if(lightbeam.GetComponent<SpriteRenderer>().material.color.a >= 120f)
            {
                lightbeam.GetComponent<BoxCollider2D>().enabled = true;
                Particles.SetActive(true);
                lightbeam.GetComponent<BoxCollider2D>().isTrigger = true;
                
                canFade = false;
                
            }
        }
    }

}
