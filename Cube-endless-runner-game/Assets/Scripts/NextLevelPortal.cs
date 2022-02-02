using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class NextLevelPortal : MonoBehaviour
{ 
    public GameObject pickupEffect;
    public Vector3 pickupEffectPosition;
    public float nlPortalSpeed;
    public GameManager gameManager;
    public Camera mainCamera;
    //public Slider levelProgressSlider;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            goThroughPortal();
        }

    }

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        mainCamera = FindObjectOfType<Camera>();
        //levelProgressSlider = FindObjectOfType<Canvas>().GetComponent<Leve>();
    }


    void Start()
    {
        //make partical effect.
        //Invoke("destroyParticle", 1);
        //Instantiate(pickupEffect, transform.position, transform.rotation);

    }

    void FixedUpdate()
    {
        //Instantiate(pickupEffect, transform.position, transform.rotation);

        //-1 makes vector3.forward going backwards.
        transform.Translate(-1 * (Vector3.forward * Time.deltaTime * nlPortalSpeed));

        //pickupEffect.transform.Translate(-1 * (Vector3.forward * Time.deltaTime * nlPortalSpeed));

        /*pickupEffectPosition = transform.position;

        pickupEffect.transform.position = pickupEffectPosition;*/

        //Destroy(pickupEffect);


    }

    void destroyParticle()
    {

    }

    void goThroughPortal()
    {
        //make sound
        FindObjectOfType<AudioManager>().Play("GoThroughPortal");

        //spawn effect when going through.
        //Instantiate(pickupEffect, transform.position, transform.rotation);

        //apply effect to the player.
        //..


        //Change invironement
        Color newColor = gameManager.colorPatternLevels[gameManager.numberOfLevelChanges];
        mainCamera.backgroundColor = newColor;
        RenderSettings.fogColor = newColor;
            //FindObjectOfType<Camera>().backgroundColor = newCol;
            //FindObjectOfType<Canvas>().GetComponentInChildren<Leve> TODO change color of progressbar.

        //Close the portal
        FindObjectOfType<BlockSpawner>().portalActivated = false;
        //Destroy portal
        Destroy(gameObject);

    }

}
