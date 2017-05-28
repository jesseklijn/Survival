using UnityEngine;
using System.Collections;
using System;

public class WorldCycle : MonoBehaviour
{

    public bool isRotating = false;
    public WorldSettings worldSettings;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Timer());
    }
    //DesiredIntensity between 0-1

    IEnumerator RotateTowardsClosestWayPoint(Vector3 PointA, Vector3 PointB, float time, GameObject toMove)
    {
        //Check if character is already moving..
        //Debug.Log(PointA + " # " + PointB);
        if (isRotating == false)
        {

            isRotating = true;

            float t = 0;
            //Move while time is still below 1
            while (t < 1)
            {
                t += Time.deltaTime / time;
                toMove.gameObject.transform.eulerAngles = Vector3.Lerp(PointA, PointB, t);


                yield return 0;

            }

            //Reset Routine
            isRotating = false;
            float radiusIncrement = worldSettings.dawnTime + worldSettings.dayTime + worldSettings.duskTime + worldSettings.nightTime;
            StartCoroutine(RotateTowardsClosestWayPoint(worldSettings.directionalLighting.transform.eulerAngles, new Vector3(worldSettings.directionalLighting.transform.rotation.x + 360, worldSettings.directionalLighting.transform.rotation.y, worldSettings.directionalLighting.transform.rotation.z), radiusIncrement, worldSettings.directionalLighting.transform.parent.gameObject));
        }

    }


    public IEnumerator setDesiredIntensity(float timeFrame, float desiredIntensity, float increment, float currentIntensity)
    {
        float radiusIncrement = worldSettings.dawnTime + worldSettings.dayTime + worldSettings.duskTime + worldSettings.nightTime;

        StartCoroutine(RotateTowardsClosestWayPoint(worldSettings.directionalLighting.transform.eulerAngles, new Vector3(worldSettings.directionalLighting.transform.rotation.x + 360, worldSettings.directionalLighting.transform.rotation.y, worldSettings.directionalLighting.transform.rotation.z), radiusIncrement, worldSettings.directionalLighting.transform.parent.gameObject));
        //StartCoroutine(RotateTowardsClosestWayPoint(radiusIncrement, worldSettings.directionalLighting.gameObject, 360));
   
        //Debug.Log("rotates 1,0,1..");
        worldSettings.directionalLighting.intensity += increment / timeFrame;
        yield return new WaitForSeconds(1);
        if (currentIntensity != desiredIntensity)
        {
            StartCoroutine(setDesiredIntensity(worldSettings.duskTime, desiredIntensity, increment, worldSettings.directionalLighting.intensity));
        }

    }

    public IEnumerator Timer()
    {

        //Add something
        worldSettings.currentTime++;
        //Debug.Log(worldSettings.currentTime);

        yield return new WaitForSeconds(worldSettings.timePerSecond);

        //if time is between 4  && 6
        if (worldSettings.currentTime >= worldSettings.dayTime && worldSettings.currentTime < (worldSettings.dayTime + worldSettings.duskTime))
        {
            if (worldSettings.setting != WorldSettings.Setting.Dusk)
            {

                worldSettings.setting = WorldSettings.Setting.Dusk;
                float desiredIntensityRAW = 0.5F;
                float subtractedIntensity = desiredIntensityRAW - worldSettings.directionalLighting.intensity;
                StartCoroutine(setDesiredIntensity(worldSettings.duskTime, desiredIntensityRAW, subtractedIntensity, worldSettings.directionalLighting.intensity));
            }


        }
        //if time is between 6 && 8
        else if (worldSettings.currentTime >= (worldSettings.dayTime + worldSettings.duskTime) && worldSettings.currentTime < (worldSettings.dayTime + worldSettings.duskTime + worldSettings.nightTime))
        {
            if (worldSettings.setting != WorldSettings.Setting.Night)
            {
                worldSettings.setting = WorldSettings.Setting.Night;
                float desiredIntensityRAW = 0.25F;
                float subtractedIntensity = desiredIntensityRAW - worldSettings.directionalLighting.intensity;
                StartCoroutine(setDesiredIntensity(worldSettings.nightTime, desiredIntensityRAW, subtractedIntensity, worldSettings.directionalLighting.intensity));
            }

        }
        else if (worldSettings.currentTime >= (worldSettings.dayTime + worldSettings.duskTime + worldSettings.nightTime) && worldSettings.currentTime < (worldSettings.dayTime + worldSettings.duskTime + worldSettings.nightTime + worldSettings.dawnTime))
        {
            if (worldSettings.setting != WorldSettings.Setting.Dawn)
            {
                worldSettings.setting = WorldSettings.Setting.Dawn;
                float desiredIntensityRAW = 1F;
                float subtractedIntensity = desiredIntensityRAW - worldSettings.directionalLighting.intensity;
                StartCoroutine(setDesiredIntensity(worldSettings.dawnTime, desiredIntensityRAW, subtractedIntensity, worldSettings.directionalLighting.intensity));
            }

        }
        else if (worldSettings.currentTime >= (worldSettings.dayTime + worldSettings.duskTime + worldSettings.nightTime + worldSettings.dawnTime))
        {
            if (worldSettings.setting != WorldSettings.Setting.Day)
            {
                worldSettings.setting = WorldSettings.Setting.Day;
                float desiredIntensityRAW = 1.5F;
                float subtractedIntensity = desiredIntensityRAW - worldSettings.directionalLighting.intensity;
                StartCoroutine(setDesiredIntensity(worldSettings.dayTime, desiredIntensityRAW, subtractedIntensity, worldSettings.directionalLighting.intensity));
                worldSettings.currentTime = 0;
            }


        }
        StartCoroutine(Timer());
    }
}
