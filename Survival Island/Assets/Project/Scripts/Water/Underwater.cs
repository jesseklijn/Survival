using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityStandardAssets.Characters.FirstPerson;

public class Underwater : MonoBehaviour
{

    public WorldSettings worldSettings;
    public bool isUnderwater = false;


    public WeatherController weatherController;
 
    public Rigidbody fpsRigidbody;

    public Vector3 desiredVelocity = Vector3.zero;
    public bool isGrounded = false;
    public bool isTouchingWaterSurface = false;

    // Use this for initialization
    void Start()
    {
        CharacterController controller = gameObject.transform.parent.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Timescaling for fast forward (Frametime testing)
        if (Input.GetKey(KeyCode.T))
        {
            Time.timeScale = 20;
        }
        else
        {
            Time.timeScale = 1;
        }

        //Checks if person is in the water or not
        if (transform.position.y < worldSettings.waterLevelPosition && isUnderwater == false)
        {
            isUnderwater = true;
            ChangeWatervision();
        }
        else if (transform.position.y > worldSettings.waterLevelPosition && isUnderwater == true)
        {
            isUnderwater = false;
            ChangeWatervision();
        }
       
        // is the controller on the ground?
        if (isUnderwater == false)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space))
            {
                if (isGrounded == true)
                {
                    Debug.Log("Not underwater and I am grounded.");
                   
                    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                    {
                        SurfaceWalk(false, 1);
                    }
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Jump();
                    }
                }
                else
                {
                   //IN THE AIR
                }
            }
        }
        else
        {
            if (isTouchingWaterSurface == true)
            {
                if (isGrounded == false)
                {
                    Debug.Log("Not underwater but floating and I am NOT grounded.");

                }

            }
            else if (isTouchingWaterSurface == false)
            {
                Debug.Log("I AM UNDERWATER.");
                WaterWalk();
                
            }
        }


    }
    void Jump()
    {
        
        if (isGrounded)
        {
            desiredVelocity = transform.parent.up * 125 * Time.deltaTime;
        }

        fpsRigidbody.velocity = new Vector3(fpsRigidbody.velocity.x, desiredVelocity.y, fpsRigidbody.velocity.z);
        desiredVelocity = Vector3.zero;

    }
    void SurfaceWalk(bool WaterSplash, int modifier)
    {

        desiredVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            (desiredVelocity += transform.parent.forward * (150 / modifier)).Normalize();
            Debug.Log("I Tried to move FORWARD");
        }
        if (Input.GetKey(KeyCode.A))
        {
            (desiredVelocity += -transform.parent.right * (100 / modifier)).Normalize();
        }
        if (Input.GetKey(KeyCode.S))
        {
            (desiredVelocity += -transform.parent.forward * (125 / modifier) ).Normalize();
        }
        if (Input.GetKey(KeyCode.D))
        {
            (desiredVelocity += transform.parent.right * (100 / modifier)).Normalize();
        }

        fpsRigidbody.velocity = desiredVelocity * Time.deltaTime;
        desiredVelocity = Vector3.zero;



    }
    void WaterWalk()
    {

        desiredVelocity = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            (desiredVelocity += transform.forward * 150 * Time.deltaTime).Normalize();
            Debug.Log("I Tried to move FORWARD");
        }
        if (Input.GetKey(KeyCode.A))
        {
            (desiredVelocity += -transform.parent.right * 100 * Time.deltaTime).Normalize();
        }
        if (Input.GetKey(KeyCode.S))
        {
            (desiredVelocity += -transform.forward * 125 * Time.deltaTime).Normalize();
        }
        if (Input.GetKey(KeyCode.D))
        {
            (desiredVelocity += transform.parent.right * 100* Time.deltaTime).Normalize();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            (desiredVelocity += transform.parent.up * 125 * Time.deltaTime).Normalize();
        }

        fpsRigidbody.velocity = desiredVelocity;
        desiredVelocity = Vector3.zero;



    }
    void ChangeWatervision()
    {
        weatherController.WeatherUpdate();
    }

}


