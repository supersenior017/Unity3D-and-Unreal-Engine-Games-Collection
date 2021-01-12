using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animation : MonoBehaviour
{
    public GameObject button1;
    public GameObject Canvas;
    public GameObject image1;

    Vector2 image1scale;

    // Start is called before the first frame update
    void Start()
    {
        image1scale = button1.transform.localScale;
        image1.transform.localScale = image1scale;        
    }

    // Update is called once per frame
    void Update()
    {
        float ScalingSpeed = Canvas.GetComponent<ButtonSizeChange>().ScalingSpeed;

        if(Canvas.GetComponent<ButtonSizeChange>().button1_activated == true)
        {
            image1.GetComponent<Image>().transform.localScale = Vector2.Lerp(image1.transform.localScale, new Vector2(image1scale.x, image1scale.y / 2), ScalingSpeed * Time.deltaTime);
        }
        if(Canvas.GetComponent<ButtonSizeChange>().button1_activated == false && Canvas.GetComponent<ButtonSizeChange>().button1_finished == false)
        {
            image1.GetComponent<Image>().transform.localScale = Vector2.Lerp(image1.transform.localScale, new Vector2(image1scale.x, image1scale.y), ScalingSpeed * Time.deltaTime);
        }
    }
}
