using System.Collections;
using System.Collections.Generic;
using UnityEngine;


  public class Enemy_Health : MonoBehaviour
{

    public float health;

    private float health_for_bar;   // for scaling

    public RectTransform healthBar;   // healthbar ist rot

 
    Vector3 original;  // originale healthbar.parent localscale Werte.

    private bool isdead;

    private AudioSource AttackSound;

    void Start()
    {


        health_for_bar = health;

        original = healthBar.parent.localScale;   // original scale von healthBars parent. Also localscale von parent wird in orignal gespeichert

        AttackSound = GetComponent<Enemy_Follow>().AttackSound;
      
     
    }

   
    void Update()
    {

        healthBar.localScale = new Vector3(health/health_for_bar, 1, 1);


        if (transform.localScale.x == -1)
        {
        
            healthBar.parent.localScale = -original;  //parent wird gespiegelt, sodass healbar von rechts nach links leer geht, statt dass healthbar sich mit Gegnern dreht, wo sonst von links nach rechts auf Null gehen würde

        }

        else
        {

            healthBar.parent.localScale = original;
          
        }


        if (isdead)
        {

            Vector3 desiredPosition = new Vector3(0, 0, 0);

            Vector3 smoothPosition = Vector3.Lerp(transform.localScale, desiredPosition, 5 * Time.deltaTime);  // Lerp ist eine Interpolation von einer Position zu anderen 




            transform.localScale = smoothPosition;
        }

    }

    public float getHealth()
    {
        return health;
    }


    // ******************************************
    // death
    // ******************************************
    // Called, when players health is 0.
    // ******************************************
    // arguments:
    // - int damage
    // ******************************************
    // return value:
    //  - bool
    // ******************************************
    private void death()
    {

        isdead = true;

        GetComponent<Enemy_Follow>().enabled = false;
       // GetComponent<Collider2D>().enabled = false;
        // GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<Enemy_Attack>().enabled = false;
        //GetComponent<Enemy_Follow>().AttackSound.enabled = false;

        StartCoroutine(FadeOut(AttackSound, 1f));



        Destroy(gameObject, 2);
    }



   
        static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
        {
            float startVolume = audioSource.volume;
            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }
            audioSource.Stop();
        }

  

        // ******************************************
        // getHit
        // ******************************************
        // Called, when player gets hit by an enemy.
        // Edit the health value. Returns FALSE, when success
        // ******************************************
        // arguments:
        // - int damage
        // ******************************************
        // return value:
        //  - bool
        // ******************************************

        public bool getHit(int damage)
    {
        health += damage;

        if (health <= 0)
        {
            health = 0;
            death();
        }
        return false;
    }
}

