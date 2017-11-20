using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grounded : MonoBehaviour {

    public Underwater underWater;
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Terrain")
        {
            underWater.isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            underWater.isGrounded = false;
        }
    }
}
