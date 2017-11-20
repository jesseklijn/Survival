using UnityEngine;
using System.Collections;
using System;

public class WorldCycle : MonoBehaviour
{

    public bool isRotating = false;
    public WorldSettings worldSettings;
    ParticleSystem.MainModule settings;
    Light sunray;
    // Use this for initialization
    void Start()
    {

        sunray = worldSettings.SunraysUnderwater.GetComponent<Light>();
        settings = worldSettings.sky.main;
        StartCoroutine(Timer());
    }
    //DesiredIntensity between 0-1


    IEnumerator RotateTowardsClosestWayPoint(Vector3 PointA, Vector3 PointB, float time, GameObject toMove, Color currentWaterColor, Color desiredWaterColor, Light toBeEdited)
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
            StartCoroutine(RotateTowardsClosestWayPoint(worldSettings.directionalLighting.transform.eulerAngles, new Vector3(worldSettings.directionalLighting.transform.rotation.x + 360, worldSettings.directionalLighting.transform.rotation.y, worldSettings.directionalLighting.transform.rotation.z), radiusIncrement, worldSettings.directionalLighting.transform.parent.gameObject, currentWaterColor, desiredWaterColor,toBeEdited));
        }

    }


    public IEnumerator setDesiredIntensity(float timeFrame, float desiredIntensity, float currentIntensity, Color currentSkyColor, Color desiredSkyColor, Color currentWaterColor, Color desiredWaterColor, Light toBeEdited)
    {
        float radiusIncrement = worldSettings.dawnTime + worldSettings.dayTime + worldSettings.duskTime + worldSettings.nightTime;

        StartCoroutine(RotateTowardsClosestWayPoint(worldSettings.directionalLighting.transform.eulerAngles, new Vector3(worldSettings.directionalLighting.transform.rotation.x + 360, worldSettings.directionalLighting.transform.rotation.y, worldSettings.directionalLighting.transform.rotation.z), radiusIncrement, worldSettings.directionalLighting.transform.parent.gameObject, currentWaterColor, desiredWaterColor, toBeEdited));

        float t = 0;
        //Move while time is still below 1
        while (t < 1)
        {
            t += Time.deltaTime / timeFrame;
           worldSettings.directionalLighting.intensity = Mathf.Lerp(currentIntensity, desiredIntensity, t);
            settings.startColor = new Color(Mathf.Lerp(currentSkyColor.r, desiredSkyColor.r, t), Mathf.Lerp(currentSkyColor.g, desiredSkyColor.g, t),Mathf.Lerp(currentSkyColor.b, desiredSkyColor.b, t));
           toBeEdited.color = new Color(Mathf.Lerp(currentWaterColor.r, desiredWaterColor.r, t), Mathf.Lerp(currentWaterColor.g, desiredWaterColor.g, t), Mathf.Lerp(currentWaterColor.b, desiredWaterColor.b, t));
            yield return 0;

        }
    }
   
    public IEnumerator Timer()
    {
       
        //Add something
        worldSettings.currentTime++;
        //Debug.Log(worldSettings.currentTime);

        yield return new WaitForSeconds(worldSettings.timePerSecond);

        //if time is between 4  && 6
        if (worldSettings.currentTime >= worldSettings.dayTime * 0.90F && worldSettings.currentTime < (worldSettings.dayTime + worldSettings.duskTime))
        {
            if (worldSettings.setting != WorldSettings.Setting.Dusk)
            {
                worldSettings.setting = WorldSettings.Setting.Dusk;
                Color desiredSkyColor = new Color(0.30F, 0.44F, 0.5F);
                Color desiredWaterColor = new Color(0.15F, 0.20F, 0.20F);
                float desiredIntensityRAW = 0.2F;
                StartCoroutine(setDesiredIntensity(worldSettings.dayTime * 0.40F, desiredIntensityRAW, worldSettings.directionalLighting.intensity,settings.startColor.color, desiredSkyColor, sunray.GetComponent<Light>().color,desiredWaterColor, sunray.GetComponent<Light>()));
            }


        }
        //if time is between 6 && 8
        else if (worldSettings.currentTime >= (worldSettings.dayTime + (worldSettings.duskTime * 0.75F)) && worldSettings.currentTime < (worldSettings.dayTime + worldSettings.duskTime + worldSettings.nightTime))
        {
            if (worldSettings.setting != WorldSettings.Setting.Night)
            {

                worldSettings.setting = WorldSettings.Setting.Night;

                //Assign night sky
                
             Color desiredColor = new Color(0.121F, 0.121F, 0.121F);
                Color desiredWaterColor = new Color(0.075F, 0.10F, 0.10F);
                float desiredIntensityRAW = 0.0F;
                StartCoroutine(setDesiredIntensity(worldSettings.duskTime * 0.25F, desiredIntensityRAW, worldSettings.directionalLighting.intensity, settings.startColor.color, desiredColor,sunray.GetComponent<Light>().color, desiredWaterColor, sunray.GetComponent<Light>()));
            }

        }
        else if (worldSettings.currentTime >= (worldSettings.dayTime + worldSettings.duskTime + (worldSettings.nightTime * 0.85F)) && worldSettings.currentTime < (worldSettings.dayTime + worldSettings.duskTime + worldSettings.nightTime + worldSettings.dawnTime))
        {
            if (worldSettings.setting != WorldSettings.Setting.Dawn)
            {

                Color desiredColor = new Color(0.251F, 0.251F, 0.251F);
                Color desiredWaterColor = new Color(0.10F, 0.20F, 0.15F);
                worldSettings.setting = WorldSettings.Setting.Dawn;
                float desiredIntensityRAW = 1F;
                StartCoroutine(setDesiredIntensity(worldSettings.nightTime * 0.15F, desiredIntensityRAW, worldSettings.directionalLighting.intensity, settings.startColor.color, desiredColor, sunray.GetComponent<Light>().color, desiredWaterColor, sunray.GetComponent<Light>()));
            }

        }
        else if (worldSettings.currentTime >= (worldSettings.dayTime + worldSettings.duskTime + worldSettings.nightTime + (worldSettings.dawnTime * 0.75F)))
        {
            if (worldSettings.setting != WorldSettings.Setting.Day)
            {
                Color desiredColor = Color.white;
                Color desiredWaterColor = new Color(0.15F, 0.35F, 0.30F);
                worldSettings.setting = WorldSettings.Setting.Day;
                float desiredIntensityRAW = 1.5F;
                StartCoroutine(setDesiredIntensity(worldSettings.dawnTime * 0.25F, desiredIntensityRAW, worldSettings.directionalLighting.intensity, settings.startColor.color, desiredColor, sunray.GetComponent<Light>().color, desiredWaterColor, sunray.GetComponent<Light>()));
                worldSettings.currentTime = 0;
            }


        }
        StartCoroutine(Timer());
    }
}
