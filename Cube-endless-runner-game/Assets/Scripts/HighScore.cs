using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    public GameManager gameManager;
    public Text highScoreText;

    // Update is called once per frame
    void Update()
    {
        highScoreText.text = "High Score: " + gameManager.highScore.ToString();
    }


}
