using System;
using UnityEngine;


public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;

    public GameManager gameManager;
    public AudioManager audioManager;

    //public float forwardForce = 2000f; 
    //public float sidewaysForce = 4000f;

    private Vector3 targetPos;
    private float leftAndRightIncrement = 2.0f;
    public float speed;

    public bool fallingOfPlainForceApplied;

    //set reference to the gamemanager right before instance is made.
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();

    }

	// Update is called once per frame
	void FixedUpdate () {

        //checks if player is still wihtin bounderies of the plain, if not than it should fall.
        if(transform.position.x < 5.5 && transform.position.x > -5.5)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        } else //else add forces to replicate falling movement
        {
            if (transform.position.x > 5.5 && !fallingOfPlainForceApplied)
            {
                rb.AddForce(2000 * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                rb.AddForce(0, 0, 2000 * Time.deltaTime, ForceMode.VelocityChange);
            }
            else if (transform.position.x < -5.5 && !fallingOfPlainForceApplied)
            {
                rb.AddForce(-2000 * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
                rb.AddForce(0, 0, 2000 * Time.deltaTime, ForceMode.VelocityChange);
            }
            fallingOfPlainForceApplied = true; //to make sure the forces only get applied once.
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //rounds the location on the x-axis to an int that is even.
            float newPos = transform.position.x - leftAndRightIncrement;
            float roundedNewPos = (float)Math.Round(newPos / 2, MidpointRounding.AwayFromZero) * 2;

            targetPos = new Vector3(roundedNewPos, transform.position.y, transform.position.z);
            //rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            
            //play move sound
            audioManager.Play("PlayerMovement");
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //rounds the location on the x-axis to an int that is even.
            float newPos = transform.position.x + leftAndRightIncrement;
            float roundedNewPos = (float)Math.Round(newPos / 2, MidpointRounding.AwayFromZero) * 2;
                
            targetPos = new Vector3(roundedNewPos, transform.position.y, transform.position.z);
            //rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

            //play move sound
            audioManager.Play("PlayerMovement");
        }


        //endgame if player falls of plain
        if (transform.position.y < 0)
        {
            FindObjectOfType<GameManager>().EndGame();
        }
 

    }
}
