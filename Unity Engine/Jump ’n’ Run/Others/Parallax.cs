using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public bool scrolling, parallax;

    public float backgroundSize;

    public float parallaxSpeed;



    private Transform cameraTransform;
    private Transform[] Layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;


    private float lastCameraX;

    private void Start() {

       
        cameraTransform = Camera.main.transform;

        lastCameraX = cameraTransform.position.x;
        Layers = new Transform[transform.childCount];  // wie viele kinder? 
        for (int i = 0; i < transform.childCount;i++) {

            Layers[i] = transform.GetChild(i);

            leftIndex = 0;
            rightIndex = Layers.Length - 1;

        }

    }


    private void Update()
    {


        if (Input.GetKey("u"))
        {

           //Layers[leftIndex].position = new Vector3(1 * Layers[rightIndex].position.x + backgroundSize, Layers[rightIndex].position.y+10, 0);

        }




        if (parallax)
        {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * parallaxSpeed);
        }

        lastCameraX = cameraTransform.position.x;


        if (scrolling)
        {
            if (cameraTransform.position.x < (Layers[leftIndex].position.x + viewZone))
            {

                ScrollLeft();
            }

            if (cameraTransform.position.x > (Layers[rightIndex].position.x - viewZone))
            {

                ScrollRight();
            }
        }

    }

    private void ScrollLeft() {

        int lastRight = rightIndex;  
        Layers[rightIndex].position = new Vector3(1 * Layers[leftIndex].position.x - backgroundSize, Layers[leftIndex].position.y, 0);     //Vector3.right * (Layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if(rightIndex < 0) {

            rightIndex = Layers.Length - 1;
        }


    }

	
	private void ScrollRight() {


        int lastLeft = leftIndex;  
        Layers[leftIndex].position = new Vector3(1 * Layers[rightIndex].position.x + backgroundSize, Layers[rightIndex].position.y, 0);  //Vector3.right * (Layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == Layers.Length)
        {
            leftIndex = 0;
         
        }



    }
}
