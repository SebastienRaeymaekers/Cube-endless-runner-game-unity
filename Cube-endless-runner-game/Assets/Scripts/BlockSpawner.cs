using System.Collections;
using UnityEngine;

public class BlockSpawner : MonoBehaviour {

    public Transform[] spawnPoints;
    public Transform[] powerUpSpawnPoints;
    public Transform[] coinSpawnPoints;

    public Transform portalSpawnPoint;
    public GameObject obstaclePrefab;
    public GameObject powerUpPrefab;
    public GameObject portalPrefab;
    public GameObject coinPrefab;
    public GameManager gameManager;

    //is array with int options for now, later it is better to work with chances, where the chance of getting a more difficult obstacle is increases when your score rises.
    public int[] allObstacles;
    public int lengthObst;

    //boolean so we can hold the obstacles that are spawning.
    public bool portalActivated;
    public float portalSpawnDelay;


    bool justSpawnedObst;

    public float obstacleWaveWait;
    public float firstPowerUpWait;
    public float powerUpWaveWait;

    void Start()
    {
        StartCoroutine(SpawnObstacleWaves());
        StartCoroutine(SpawnCoins());
        StartCoroutine(SpawnPowerUps());
    }

    IEnumerator SpawnObstacleWaves()
    {
        while (true) //acts like the update-loop
        {
            while (portalActivated == false)//don't spawn anything if portal is activated
            {
                int randomIndex = Random.Range(1, /*lengthObst+1*/ lengthObst + 1);
                SpawnObstacle(gameManager.level, randomIndex);
                justSpawnedObst = true;

                yield return new WaitForSeconds(obstacleWaveWait);//wait amount of seconds before recalling spawnObstacle function
            }

            if (portalActivated == true)
            {
                justSpawnedObst = false;
            }

            yield return new WaitForSeconds(obstacleWaveWait);//is wait for restarting the spawnloop. This is to counter rapid branching of the while loop when there is a portal active.
        }
    }

    IEnumerator SpawnPowerUps()
    {
        while (true)
        {
            yield return new WaitForSeconds(firstPowerUpWait);

            while (portalActivated == false)
            {
                int randomIndex = Random.Range(1, powerUpSpawnPoints.Length);
                spawnPowerUpAtPos(0, randomIndex);

                yield return new WaitForSeconds(powerUpWaveWait);
            }

            yield return new WaitForSeconds(obstacleWaveWait);
        }
    }

    IEnumerator SpawnCoins()
    {
        while (true)
        {
            while (portalActivated == false)
            {
                //50 % chance that random coins get spawned 0.5 seconds after just spawned obstacles.
                int randomChance = Random.Range(0, 2);
                if (justSpawnedObst && randomChance > 0.5)
                {
                    Invoke("spawnCoinsRandom", 0.5f);
                }

                yield return new WaitForSeconds(obstacleWaveWait);
            }

            yield return new WaitForSeconds(obstacleWaveWait);
        }
    }


    // Update is called once per frame
 /*   void Update()
    {

        //if portal is actived, hold spawning blocks so we can go to next level. TODO time will be messed up if we just resume this process after a given time!!!
        if (portalActivated == false)
        {
            bool justSpawnedObst = false;

            //check if its time to spawn obstacle.
            if (Time.time >= timeToSpawn)
            {
                int randomIndex = Random.Range(1, /*lengthObst+1*/// lengthObst + 1);
/*              SpawnObstacle(gameManager.level, randomIndex);
                timeToSpawn = Time.time + timeBetweenWaves;
                justSpawnedObst = true;
            }

            //check if its time to spawn powerup.
            if (Time.time >= timeToFirstPowerUp)
            {
                int randomIndex = Random.Range(1, powerUpSpawnPoints.Length);
                spawnPowerUpAtPos(0, randomIndex);
                timeToFirstPowerUp = Time.time + timeBetweenPowerUps;
            }

            //50 % chance that random coins get spawned 0.5 seconds after just spawned obstacles.
            int randomChance = Random.Range(0, 2);
            if (justSpawnedObst && randomChance > 0.5)
            {
                Invoke("spawnCoinsRandom", 0.5f);
            }

        }

    }*/

    //spawns a row of coins at column col.
    void SpawnCoinsAtCol(int col)
    {
        for (int i = col; i < spawnPoints.Length; i += 5)
        {
            spawnCoinAtPos(col, i);
        }
    }

    //spawn a row of coins at a random first-row spawnpoint.
    void spawnCoinsRandom()
    {
        int randomIndex = Random.Range(1, 6);
        for (int i = randomIndex; i < spawnPoints.Length; i += 5)
        {
            spawnCoinAtPos(randomIndex, i);
        }
    }


    void SpawnObstacle (int level, int randomIndex) //TODO come up with a dynamic solution
    {
        if(level < 3)
        {
            if (randomIndex == 1) spawnRow();
            if (randomIndex == 2) spawnDiagonal("RL");
            //if (randomIndex == 3) spawnDiagonal("LR");
            //if (randomIndex == 3) spawnThree("LEFT");
            if (randomIndex == 3) spawnHorMov();
            if (randomIndex == 4) spawnThree("RIGHT");

        }
        else if (level >=3)
        {
            if (randomIndex == 1) spawnVee("LEFT");
            if (randomIndex == 2) spawnVee("RIGHT");
            if (randomIndex == 3) spawnVee("MIDDLE");
            if (randomIndex == 4) spawnHorMov();
        }
    }


    //obstacle = X_XXX with the free spot at a random place.  LEVELS: 01-03
    void spawnRow()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length/4);

        for (int i = 0; i < spawnPoints.Length/4; i++)
        {
            if (randomIndex != i)
            {
                Instantiate(obstaclePrefab, spawnPoints[i].position, Quaternion.identity);
            }
        }
        int randomChance = Random.Range(0, 2); //50/50 chance that missing block contains coinrow
        if (randomChance < 1) SpawnCoinsAtCol(randomIndex);
    }

    /*obstacle = _
                  X
                   X 
                    X
                     X          from right to left (players view) so spawnpoints: 4, 8, 12, 16. LEVELS: 01-03
    */
    public void spawnDiagonal(string direction) 
    {
        if (direction == "RL")
        {
            //spawn al but the last block
            for (int i = 4; i < spawnPoints.Length; i += 4)
            {
                //spawn block at pos i with the interval timeBetweenDraws
                spawnObstacleAtPos(0, i);
            }
        }

        else if (direction == "LR") // at pos 0, 6, 12
        {
            //spawn al but the last block
            for (int i = 0; i < spawnPoints.Length - 1; i += 6)
            {
                //spawn block at pos i with the interval timeBetweenDraws
                spawnObstacleAtPos(0, i);
            }
        }

    }

    //LEVELS: 01-03
    public void spawnBarier()
    {

        //spawn block at pos i with the interval timeBetweenDraws
        /*spawnObstacleAtPos(0, 1);
        spawnObstacleAtPos(1, 1);

        spawnObstacleAtPos(0, 3);
        spawnObstacleAtPos(1, 3);*/


    }

    /*pattern: 
            ... LEFT NORMAL: 1, 5, 7. RIGHT NORMAL: 3, 7, 9. LEFT REVERSE: 0, 2, 6. RIGHT REVERSE: 2, 4, 8.
    */
    public void spawnVee(string type)
    {
        if (type == "LEFT")
        {
            int randomChance = Random.Range(0, 2);
            if (randomChance < 1)
            {
                spawnObstacleAtPos(0, 1); spawnObstacleAtPos(0, 5); spawnObstacleAtPos(0, 7);
            }
            else
            {
                spawnObstacleAtPos(0, 3); spawnObstacleAtPos(0, 7); spawnObstacleAtPos(0, 9);
            }

        }
        else if (type == "RIGHT")
        {
            int randomChance = Random.Range(0, 2);
            if (randomChance < 1)
            {
                spawnObstacleAtPos(0, 0); spawnObstacleAtPos(0, 2); spawnObstacleAtPos(0, 6);
            }
            else
            {
                spawnObstacleAtPos(0, 2); spawnObstacleAtPos(0, 4); spawnObstacleAtPos(0, 8);
            }
        }
        else if (type == "MIDDLE") //2, 6, 8 = NORMAL. 1,3,7 = INVERSE  
        {
            int randomChance = Random.Range(0, 2);
            if (randomChance < 1)
            {
                spawnObstacleAtPos(0, 2); spawnObstacleAtPos(0, 6); spawnObstacleAtPos(0, 8);
            }
            else
            {
                spawnObstacleAtPos(0, 1); spawnObstacleAtPos(0, 3); spawnObstacleAtPos(0, 7);
            }
        }
    }


    /*pattern: 
                XXX-- or --XXX
    */
    public void spawnThree(string side)
    {
        if (side == "LEFT")
        {
            for (int i = 0; i < 3; i ++)
            {
                //spawn block at pos i with the interval timeBetweenDraws
                spawnObstacleAtPos(0, i);
            }
        }
        else if (side == "RIGHT")
        {
            for (int i = 2; i < 5; i++)
            {
                //spawn block at pos i with the interval timeBetweenDraws
                spawnObstacleAtPos(0, i);
            }
        }
    }

    void spawnHorMov()
    {
        GameObject movingObstacle = Instantiate(obstaclePrefab, spawnPoints[0].position, Quaternion.identity) as GameObject;

        movingObstacle.GetComponent<Lerper>().enabled = true;
    }

    void spawnObstacleAtPos(int layer, int spawnPoint)
    {
        Instantiate(obstaclePrefab, spawnPoints[spawnPoint].position, Quaternion.identity);
    }

    void spawnPowerUpAtPos(int layer, int spawnPoint)
    {
        Instantiate(powerUpPrefab, powerUpSpawnPoints[spawnPoint].position, Quaternion.identity);
    }

    void spawnCoinAtPos(int layer, int spawnPoint)
    {
        Instantiate(coinPrefab, spawnPoints[spawnPoint].position, Quaternion.identity);
    }

    void spawnPortal()
    {
        Instantiate(portalPrefab, portalSpawnPoint.position, Quaternion.identity);
    }

    public void spawnNextLevelPortal()
    {
        portalActivated = true;
        Invoke("spawnPortal", portalSpawnDelay);
    }

}
