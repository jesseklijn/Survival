using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSurface : MonoBehaviour
{

    public Underwater underWater;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
            underWater.isTouchingWaterSurface = true;
      
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            underWater.isTouchingWaterSurface = false;
        if (other.gameObject.tag == "WorldObject")
        {
            other.gameObject.GetComponent<FloatingObject>().inWater = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WorldObject")
        {
            other.gameObject.GetComponent<FloatingObject>().inWater = true;
        }
    }
}
