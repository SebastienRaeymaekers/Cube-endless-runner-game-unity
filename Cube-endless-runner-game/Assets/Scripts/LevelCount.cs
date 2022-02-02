using UnityEngine;
using UnityEngine.UI;

public class LevelCount : MonoBehaviour {

    public GameManager gameManager;
    public Text levelText;

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Level: " + gameManager.level.ToString();
    }

    void increaseLevelCount(int number)
    {
        gameManager.increaseLevelCount(number);
    }
}
