using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Rocket_shoot : MonoBehaviour
{

    public float moveSpeed = 10f;

    Rigidbody2D rb;

    //do not change in Inspector
    public Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
       
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);

    }

    // Update is called once per frame
    void Update()
    {

        //rotation
        var dir = (Vector3)moveDirection;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = angle - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Mini_Asteroid")
        {
            Destroy(this.gameObject);
        }
    }


    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
