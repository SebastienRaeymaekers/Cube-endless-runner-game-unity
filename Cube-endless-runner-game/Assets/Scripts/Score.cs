using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public GameManager gameManager;
    public Text scoreText;
	
	// Update is called once per frame
	void Update ()
    {
        scoreText.text = gameManager.score.ToString();
    }

    void increaseScore(int number)
    {
        gameManager.increaseScore(number);
    }

}
