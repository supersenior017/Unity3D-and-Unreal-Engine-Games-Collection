using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;       //Referenz des Spielerobjekts

    private Vector3 offset;         //offset Differenz zwischen Spieler und Kamera

    public float smoothSpeed = 10f;

    //Initialization
    void Start()
    {
        //transform.position der Kamera wird abgezogen von player.position
        offset = transform.position - player.transform.position;
    }

    // LateUpdate nach update des Frames
    void FixedUpdate()
    {
        // Spieler und Kamera selbe Position. Nur dass ein offset dazu addiert wird
        Vector3 desiredPosition = player.transform.position + offset;

        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);  // Lerp ist eine Interpolation von einer Position zu anderen 

        transform.position = smoothPosition;

    }
}

