using UnityEngine;
using UnityEngine.UI;

public class CoinCounter : MonoBehaviour {

    public GameManager gameManager;
    public Text coinCounterText;

    // Update is called once per frame
    void Update()
    {
        coinCounterText.text = "Coins: " + gameManager.CoinCounter.ToString();
    }

    void increaseCoinCount(int number)
    {
        gameManager.increaseCoinCount(number);
    }

}
