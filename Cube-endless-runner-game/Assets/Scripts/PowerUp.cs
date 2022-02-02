using UnityEngine;

public class PowerUp : MonoBehaviour {

    public AudioManager audioManager;
    public GameObject pickupEffect;
    public float powerUpSpeed;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickUp();
        }

    }

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        //if PowerUp is passed player, destroy it.
        if (transform.position.z < -7)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        //*-1 makes vector3.forward going backwards.
        transform.Translate(-1*(Vector3.forward * Time.deltaTime * powerUpSpeed));
    }

    void pickUp()
    {
        //play pickup sound
        audioManager.Play("PowerUpPickup");

        //spawn effect.
        Instantiate(pickupEffect, transform.position, transform.rotation);

        //apply effect to the player.
        FindObjectOfType<GameManager>().doPowerUp(); // TODO make reference instead.

        Destroy(gameObject);

    }

}
