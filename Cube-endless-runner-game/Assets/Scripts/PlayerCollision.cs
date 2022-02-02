using UnityEngine;

public class PlayerCollision : MonoBehaviour {

    public PlayerMovement playerMovement;
    public ObstacleMovement obstacleMovement;
    public GameManager gameManager;
    public GameObject collisionEffect;


    void OnCollisionEnter(UnityEngine.Collision collisionInfo)
    { 

        if(collisionInfo.gameObject.tag == "Obstacle")
        {
            AudioManager audioManager = FindObjectOfType<AudioManager>();

            //stop backgroundmusic
            audioManager.Stop("BackgroundMusic");

            //play death sound
            audioManager.Play("PlayerDeath");

            //spawn effect.
            Instantiate(collisionEffect, transform.position, transform.rotation);

            playerMovement.enabled = false;
            
            GetComponent<MeshRenderer>().enabled = false; 
            
            gameManager.EndGame();
        }

    }
}
