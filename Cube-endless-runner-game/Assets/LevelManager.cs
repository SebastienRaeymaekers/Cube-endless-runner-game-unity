using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public int levelCoinCounter;

    public Slider levelProgressSlider;
    public AudioManager audioManager;

    public BlockSpawner blockSpawner;
    public GameObject blockPrefab;
    public GameObject powerUpPrefab;

    public bool gameHasEnded = false;
    bool gameStoppedBool = false;
    float restartDelay = 3f;


    public void Start()
    {
        audioManager.Play("BackgroundMusic");
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void EndLevel()
    {
        if (gameHasEnded == false)
        {
            //...
            gameHasEnded = true;
        }
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void completeLevel()
    {

    }

    //maybe do something with time, that level 1 last for 30 secs for example while spawning a certain type of obstacle. !!!
    public void updateLevelProgressBar()
    {

    }


    //POWER UP
    //EXAMPLE: Dubble Points for 5 secs.
    public void doPowerUp()
    {
        //increaseScore(50);
    }


    //COINS
    public void increaseCoinCount(int number)
    {
        levelCoinCounter += number;
    }




}
