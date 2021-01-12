using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Asteroid_Spawn : MonoBehaviour
{
    public GameObject Asteroid0Prefab;
    public GameObject Asteroid1Prefab;
    public GameObject Asteroid2Prefab;
    public GameObject Asteroid3Prefab;
    public GameObject UfoPrefab;

    public GameObject win_menu;

    private int currentLevelNumber;
    private string currentLevel;
    private bool LevelSucceeded;
    private bool AsteroidsInstantiated;

    //private bool Level_won = false;

    private Statistics_this_level GO;

    private Ground_Population GP;

    double population_diff;

    // Start is called before the first frame update
    void Start()
    {

        currentLevelNumber = 1;
        currentLevel = "Level1";
        LevelSucceeded = false;
        AsteroidsInstantiated = false;

        GO = GameObject.FindWithTag("Stats").GetComponent<Statistics_this_level>();
        GP = GameObject.FindWithTag("Ground").GetComponent<Ground_Population>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelSucceeded == false)
        {
            if(currentLevel == "Level1")
            {
                if(AsteroidsInstantiated == false)
                {
                    Level1();
                }                
                AsteroidsInstantiated = true;
                if(GameObject.FindGameObjectsWithTag("Asteroid").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Asteroid2").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Asteroid3").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Ufo").Length == 0)
                {
                    LevelSucceeded = true;
                    AsteroidsInstantiated = false;
                }
            }
            if (currentLevel == "Level2")
            {
                if (AsteroidsInstantiated == false)
                {
                    Level2();
                }
                AsteroidsInstantiated = true;
                if (GameObject.FindGameObjectsWithTag("Asteroid").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Asteroid2").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Asteroid3").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Ufo").Length == 0)
                {
                    LevelSucceeded = true;
                    AsteroidsInstantiated = false;

                }
            }
            if (currentLevel == "Level3")
            {
                if (AsteroidsInstantiated == false)
                {
                    Level3();
                }
                AsteroidsInstantiated = true;
                if (GameObject.FindGameObjectsWithTag("Asteroid").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Asteroid2").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Asteroid3").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Ufo").Length == 0)
                {
                    LevelSucceeded = true;
                    AsteroidsInstantiated = false;
                }
            }
            if (currentLevel == "Level4")
            {
                if (AsteroidsInstantiated == false)
                {
                    Level4();
                }
                AsteroidsInstantiated = true;
                if (GameObject.FindGameObjectsWithTag("Asteroid").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Asteroid2").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Asteroid3").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Ufo").Length == 0)
                {
                    LevelSucceeded = true;
                    AsteroidsInstantiated = false;
                }
            }
            if (currentLevel == "Level5")
            {
                if (AsteroidsInstantiated == false)
                {
                    Level5();
                }
                AsteroidsInstantiated = true;
                if (GameObject.FindGameObjectsWithTag("Asteroid").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Asteroid2").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Asteroid3").Length == 0 &&
                   GameObject.FindGameObjectsWithTag("Ufo").Length == 0)
                {
                    LevelSucceeded = true;
                    AsteroidsInstantiated = false;

                    // Win Menü. Dabei wird Zeit angehalten. Es kommt nur dazu wenn wirklich alle Sachen zerstört sind. Auch UFO
                    Time.timeScale = 0.0f;



                    //save Asteroid destroyed and open win_menu

                    Game_Save backup = Save_System.loadStats();

                    GO.Asteroids_destroyed = GO.Asteroids_destroyed + backup.asteroid_destroy;
                    GO.Mini_Asteroids = GO.Mini_Asteroids + backup.mini_asteroid_destroy;
                    GO.Ufos = GO.Ufos + backup.Ufo_destroyed;


                    //go.level am besten das L
                    if(backup.World_1_level <= (SceneManager.GetActiveScene().buildIndex - 6))
                    {

                        GO.Level = (SceneManager.GetActiveScene().buildIndex - 5);
                    }
                    else
                    {
                        GO.Level = backup.World_1_level;

                    }

                    population_diff = (7750000000 - GP.population_float);
                    
                    GO.population = population_diff + backup.Population;


                    /*
                     * 
                    if(GP.population_float > GO.Highscore[SceneManager.GetActiveScene().buildIndex - 6])
                    {
                        GO.Highscore[SceneManager.GetActiveScene().buildIndex - 6] = GP.population_float;

                    }

                    */

                    Save_System.saveAsteroid_stat(GO);

                    win_menu.SetActive(true);

                }
            }
        }
        else
        {
            //Erhöhe das Level und speichere da Ergebnis in einem String, der den Namen der entsprechenden Funktion entspricht (Level1(), Level2(), ...)
            currentLevelNumber += 1;
            currentLevel = "Level" + currentLevelNumber.ToString();
            LevelSucceeded = false;
        }
            
    }

    private void Level1()
    {
        //Kleiner Asteroid: y= 6
        SpawnAstroidSmall(6.0f);
        SpawnAsteroidMega(7.0f);

        //Spawn Ufo
        //SpawnUfo();
    }

    private void Level2()
    {
        //Kleiner Asteroid: y= 6
        //Kleiner Asteroid: y= 6
        SpawnAstroidSmall(6.0f);
        SpawnAstroidSmall(6.0f);
    }

    private void Level3()
    {
        //Kleiner Asteroid: y= 7
        //Kleiner Asteroid: y= 6
        //Kleiner Asteroid: y = 6
        SpawnAstroidSmall(7.0f);
        SpawnAstroidSmall(6.0f);
        SpawnAstroidSmall(6.0f);
    }

    private void Level4()
    {
        //Kleiner Asteroid: y= 6
        //Kleiner Asteroid: y= 7
        //Kleiner Asteroid: y= 8
        //Kleiner Asteroid: y= 9
        //Ufo
        SpawnAstroidSmall(6.0f);
        SpawnAstroidSmall(7.0f);
        SpawnAstroidSmall(8.0f);
        SpawnAstroidSmall(9.0f);
        SpawnUfo();
    }

    private void Level5()
    {
        //Mittlerer Asteroid: y= 7
        //Kleiner Asteroid: y= 6
        //Kleiner Asteroid: y= 6
        SpawnAstroidSmall(7.0f);
        SpawnAstroidSmall(6.0f);
        SpawnAstroidSmall(6.0f);
    }

    private void SpawnUfo()
    {
        //Spawne Ufo Prefab an zufälliger Position
        int RandomSpawnLeftRight = Random.Range(1, 3);
        float SpawnX;
        float SpawnY = Random.Range(-0.5f, 3.5f);
        float SpawnZ = 0.0f;

        if(RandomSpawnLeftRight == 1)
        {
            SpawnX = -4.5f;
        }
        else
        {
            SpawnX = 4.5f;
        }

        Instantiate(UfoPrefab, new Vector3(SpawnX, SpawnY, SpawnZ), Quaternion.identity);
    }

    //Normaler Asteroid ohne besondere Merkmale
    private void SpawnAstroidSmall(float SpawnY)
    {
        float SpawnX = Random.Range(-2.3f, 2.3f);
        float SpawnZ = 0.0f;

        Instantiate(Asteroid0Prefab, new Vector3(SpawnX, SpawnY, SpawnZ), Quaternion.identity);
    }

    //Mittlerer Asteroid Splittert in kleinere Teile
    private void SpawnAsteroidMedium(float SpawnY)
    {
        float SpawnX = Random.Range(-2.0f, 2.0f);
        float SpawnZ = 0.0f;

        Instantiate(Asteroid1Prefab, new Vector3(SpawnX, SpawnY, SpawnZ), Quaternion.identity);
    }

    //Großer Asteroid teilt sich in 2 Mittlere auf
    private void SpawnAsteroidBig(float SpawnY)
    {
        float SpawnX = Random.Range(-1.5f, 1.5f);
        float SpawnZ = 0.0f;

        Instantiate(Asteroid2Prefab, new Vector3(SpawnX, SpawnY, SpawnZ), Quaternion.identity);
    }

    //Hält mehrere Schüsse aus bis er zerstört wird
    private void SpawnAsteroidMega(float SpawnY)
    {
        float SpawnX = 0.0f;
        float SpawnZ = 0.0f;

        Instantiate(Asteroid3Prefab, new Vector3(SpawnX, SpawnY, SpawnZ), Quaternion.identity);
    }
}
