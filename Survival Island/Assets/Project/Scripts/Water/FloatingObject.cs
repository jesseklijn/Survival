using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour {

    public Rigidbody rigidBody;
    public WorldSettings worldSettings;
    public bool anchored = false;
    public bool inWater;
    
    //Calculated data
    private float massCalculation = 0;
    private float gravityCalculation = 0;

    private void Start()
    {
        rigidBody.mass = Mathf.Clamp(rigidBody.mass, 15, 40);
        massCalculation = (rigidBody.mass / (rigidBody.mass / 15F));
        gravityCalculation = (-Physics.gravity.y);
    }
    // Update is called once per frame
    private void FixedUpdate()
    {

        float modifier = -(transform.position.y - worldSettings.waterLevelPosition) * 3;
        rigidBody.AddForce(0, massCalculation * (gravityCalculation + modifier), 0,ForceMode.Force);

        if(inWater == true)
        {
            rigidBody.AddForce((worldSettings.WaterVelocity / rigidBody.mass)/2,ForceMode.Force);
        }


        //constantForce.force = new Vector3(0, 9.81F, 0);
        


    }
}
