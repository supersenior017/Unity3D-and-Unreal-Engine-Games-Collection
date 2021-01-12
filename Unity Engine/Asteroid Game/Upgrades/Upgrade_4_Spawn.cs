using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_4_Spawn : MonoBehaviour
{
    public GameObject Upgrade4;

    public int release_Upgrade_destroyed = 3;

    public int actual_destroyed_asteroids = 0;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if(actual_destroyed_asteroids >= release_Upgrade_destroyed)
        {
            SpawnUpgrade(6.0f);

        }
        
    }


    private void SpawnUpgrade(float SpawnY)
    {
        float SpawnX = Random.Range(-2.3f, 2.3f);
        float SpawnZ = 0.0f;

        actual_destroyed_asteroids = 0;

        Instantiate(Upgrade4, new Vector3(SpawnX, SpawnY, SpawnZ), Quaternion.identity);
    }



}
