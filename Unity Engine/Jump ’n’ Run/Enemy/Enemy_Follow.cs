using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Follow : MonoBehaviour
{

    public float speed;
    public float distance_active;  // distance when enemys going to attack target
    public float distance_stop;     // stop distance when target is close to the enemy

    private Transform target;

    private Enemy_Health enemy_healther;

    private float saveHealth;

    public bool Is_Soul_Destroyer = false;  // Soul destroyer soll sein localscale nicht wechseln
    public bool is_Sould_hole_Destroyer = false;
    public bool Soul_Destroyer_follow = false;

    public AudioSource AttackSound;

    private bool isHunting = true;




    private float gravity;


    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        enemy_healther = GetComponent<Enemy_Health>();

        saveHealth = enemy_healther.health;

        if (Is_Soul_Destroyer == true)
        {
            gravity = GetComponent<Rigidbody2D>().gravityScale;
            GetComponent<Rigidbody2D>().gravityScale = 0;

        }
    }


    void Update()
    {

        if (enemy_healther.health < saveHealth) // if you attack an enemy from far behind, than it will attack you
        {

            distance_active = 200;

        }
        // public AudioSource AttackSound;

        if (Vector2.Distance(transform.position, target.position) > distance_stop && Vector2.Distance(transform.position, target.position) < distance_active)
        {

            if (isHunting)
            {
                StartCoroutine(FadeIn(AttackSound, 1f));



                isHunting = false;

            }

            if (Is_Soul_Destroyer)
            {
                GetComponent<Rigidbody2D>().gravityScale = gravity * 2;

                if (Soul_Destroyer_follow == true)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                }
            }

            else
            {

                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }

            if (is_Sould_hole_Destroyer)
            {

                Destroy(this.gameObject, 5);
            }
        }

        if (target.transform.position.x > transform.position.x && !Is_Soul_Destroyer)
        {


            transform.localScale = new Vector3(-1, 1, 1);

        }

        else if (target.transform.position.x < transform.position.x && !Is_Soul_Destroyer)
        {


            transform.localScale = new Vector3(1, 1, 1);

        }


    }

    static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }

    }

}
