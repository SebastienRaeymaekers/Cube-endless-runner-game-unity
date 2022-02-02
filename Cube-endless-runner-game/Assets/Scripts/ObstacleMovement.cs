using UnityEngine;
using UnityEngine.UI;

public class ObstacleMovement : MonoBehaviour {

    public float obstacleSpeed = 25;
    public Text score;

    public GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update ()
    {
        //if obstacle is passed player, increase score and destroy it.
        if (transform.position.z < -7)
        {
            gameManager.increaseScore(1);

            Destroy(gameObject);
        }
    }

	
	void FixedUpdate ()
    {
        transform.Translate(-1 * (Vector3.forward * Time.deltaTime * obstacleSpeed));
    }
}
