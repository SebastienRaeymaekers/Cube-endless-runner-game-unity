using UnityEngine;

public class Coin : MonoBehaviour {

    public GameObject pickupEffect;
    public float coinSpeed;
    public AudioManager audioManager;

    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pickUp();
        }

    }

    void Update()
    {
        //if coin is passed player, destroy it.
        if (transform.position.z < -7)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        //*-1 makes vector3.forward going backwards.
        transform.Translate(-1 * (Vector3.forward * Time.deltaTime * coinSpeed));
    }

    void pickUp()
    {
        //play pickup sound
        audioManager.Play("CoinPickup");

        //spawn Pickup effect.
        Instantiate(pickupEffect, transform.position, transform.rotation);

        //apply effect to the player.
        FindObjectOfType<GameManager>().increaseCoinCount(1); // TODO make reference instead.

        Destroy(gameObject);

    }
}
