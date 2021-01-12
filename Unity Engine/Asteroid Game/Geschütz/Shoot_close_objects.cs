using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot_close_objects : MonoBehaviour
{

	[SerializeField]
	GameObject Mini_rocket;

	

	public float fireRate = 1;
    float nextFire;

	float distanceToEnemy;


    public float distanceToAttack = 60;
	GameObject closestEnemy = null;

	float backup_distanceToAttack;

	float Timer_geschütz_upgrade;

	void Start()
	{
		nextFire = Time.time;

		backup_distanceToAttack = distanceToAttack;
	}


	// Update is called once per frame
	void Update()
	{
		//Debug.Log(distanceToEnemy);

		FindClosestEnemy();

		if (distanceToEnemy < distanceToAttack)
		{
			CheckIfTimeToFire();
		}

		if (Timer_geschütz_upgrade > 0)
		{

			Timer_geschütz_upgrade -= Time.deltaTime;
		}

		if (Timer_geschütz_upgrade <= 0)
		{

			distanceToAttack = backup_distanceToAttack;
		}



	}

	void FindClosestEnemy()
	{
		float distanceToClosestEnemy = Mathf.Infinity;
		//GameObject closestEnemy = null;
		GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Mini_Asteroid");



		foreach (GameObject currentEnemy in allEnemies)
		{
			    distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;

			if (distanceToEnemy < distanceToClosestEnemy)
			{
				distanceToClosestEnemy = distanceToEnemy;
				closestEnemy = currentEnemy;

			}
			
		}

      // Debug.DrawLine(this.transform.position, closestEnemy.transform.position);
		//Debug.Log(distanceToClosestEnemy);

	}


    // else wird aufgerufen, wenn closestEnemy existiert.
	void CheckIfTimeToFire()
	{
		if (closestEnemy == null)
		{
			return;
		}

		else
		{
			if (Time.time > nextFire)
			{
				GameObject g = Instantiate(Mini_rocket, transform.position, Quaternion.identity);
				g.GetComponent<Mini_Rocket_shoot>().moveDirection = (closestEnemy.transform.position - transform.position).normalized;
				nextFire = Time.time + fireRate;
			}
		}
	}


	public void enable_geschütz_energy_upgrade(float value, float time)
	{
		Timer_geschütz_upgrade += time;
		distanceToAttack = value;

	}


}
