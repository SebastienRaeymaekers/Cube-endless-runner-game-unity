using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public AudioManager audioManager;

    public int score;
    public int highScore;
    public int CoinCounter;

    public int level;
    public Slider levelProgressSlider;
    public int prevLevelBound = 0;
    public int numberOfLevelChanges = 0;

    public Color[] colorPatternLevels;

    private int levelBound = 30;
    public bool justIncreasedLevel = false;

    public BlockSpawner blockSpawner;
    public GameObject blockPrefab;
    public GameObject powerUpPrefab;

    public bool gameHasEnded = false;
    bool gameStoppedBool = false;
    float restartDelay = 3f;

    public void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        audioManager.Play("BackgroundMusic");
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            //Destroy(blockPrefab); doesn't work.
            Invoke("restartGame", restartDelay);
        }
    }

    public void resetAllCanvasValues()
    {
        score = 0;
        level = 1;
        prevLevelBound = 0;
        levelBound = 30;
        levelProgressSlider.value = 0;
    }

    public bool gameStopped()
    {
        return gameStoppedBool || gameHasEnded;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void completeLevel()
    {

    }



    //SCORE

    public void resetScore()
    {
        score = 0;
    }

    public void increaseScore(int number)
    {
        audioManager.Play("ObstaclePassing");

        if (gameHasEnded == false)
        { 
            score += number;

            if (score >= levelBound /*&& justIncreasedLevel == false*/)
            {
                //set the previous levelbound to the current one.
                prevLevelBound = levelBound;

                //increase levelbound using function: f(x) = 1.5x.
                levelBound = (int)(levelBound * 1.5f);

                //decrease time between waves using function: f(x) = x/6.
                blockSpawner.obstacleWaveWait -= blockSpawner.obstacleWaveWait / 6;

                //increase speed of obstacle approaching using function: f(x) = 1.3x.        
                float obstacleSpeed = blockPrefab.GetComponent<ObstacleMovement>().obstacleSpeed;
                obstacleSpeed = (1.3f * obstacleSpeed);

                //increase speed of powerup approaching using function: f(x) = 1.3x.        
                float powerUpSpeed = powerUpPrefab.GetComponent<PowerUp>().powerUpSpeed;
                powerUpSpeed = (1.3f * powerUpSpeed);

                //set boolean so that it only runs once when levelbound is breached.
                //justIncreasedLevel = true;

                //go to next level
                increaseLevelCount(1);

                //every 3 levels a portal will be spawned and numberOfLevelChanges will be incremented.
                if (level % 3 == 0)
                {
                    FindObjectOfType<BlockSpawner>().spawnNextLevelPortal();
                    numberOfLevelChanges++;
                }

            }

            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("HighScore", score);
            }

            updateLevelProgressBar();
        }
    }


    public void updateLevelProgressBar()
    {
        float scoresInLevelToComplete = levelBound - prevLevelBound;
        float scoreInLevel = score - prevLevelBound;
        float levelProgressFloat = scoreInLevel / scoresInLevelToComplete;

        levelProgressSlider.value = levelProgressFloat;
    }

    //LEVEL

    public void increaseLevelCount(int number)
    {
        level += number;
    }


    //POWER UP
    //EXAMPLE: Dubble Points for 5 secs.
    public void doPowerUp()
    {
        increaseScore(50);
    }


    //COINS
    public void increaseCoinCount(int number)
    {
        CoinCounter += number;
    }

}
