using UnityEngine;

public class NewGameManager : MonoBehaviour
{

    public AudioManager audioManager;

    public BlockSpawner blockSpawner;
    public GameObject blockPrefab;
    public GameObject powerUpPrefab;

    public int levelReached;
    public int collectedCoins;

    public void Start()
    {
        audioManager.Play("BackgroundMusic");
        int collectedCoins = PlayerPrefs.GetInt("collectedCoins", 0); //1 is default value if game is played for first time for example.

    }

    public void EndGame()
    {

    }


    public void StorePlayerPref()
    {

    }

    //LEVEL

    public void increaseLevelCount(int number)
    {
        //level += number;
    }


}
