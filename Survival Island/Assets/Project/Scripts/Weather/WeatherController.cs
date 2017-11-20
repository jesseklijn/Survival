using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class WeatherController : MonoBehaviour
{

    public WorldSettings worldSettings;
    private WorldSettings.Weather weather;
    public Underwater underWater;
    public GlobalFog globalFog;
    public ColorCorrectionCurves colorCorrection;
    public BloomOptimized bloom;
    public BlurOptimized blur;
    public GameObject caustics;
    public Rigidbody fpsRigidbody;
    // Use this for initialization
    void Start()
    {
        weather = worldSettings.weather;
        WeatherUpdate();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void WeatherUpdate()
    {
        if (underWater.isUnderwater == true)
        {
            fpsRigidbody.useGravity = false;
            caustics.SetActive(true);
            RenderSettings.fogColor = new Color(0, 0, 0.4F, 0.5F);
            RenderSettings.fogDensity = 0.05F;
            globalFog.distanceFog = true;
        }
        else
        {
            caustics.SetActive(false);
            globalFog.distanceFog = true;
            fpsRigidbody.useGravity = true;
            blur.enabled = false;

            bloom.intensity = 0.25F;
            bloom.threshold = 0.17F;
            bloom.blurSize = 0.77F;
            RenderSettings.fogDensity = 0.00F;
            globalFog.Advanced.ScatteringSize = 0.8F;

            if (worldSettings.weather == WorldSettings.Weather.Rain)
            {
                //RenderSettings.fogDensity = 0.01F;
                //RenderSettings.fogColor = new Color(0.7F, 0.7F, 0.7F, 0.7F);
                bloom.intensity = 0.40F;
                bloom.threshold = 1.4F;
                bloom.blurSize = 2.70F;
                globalFog.Advanced.ScatteringSize = 0;


            }
        }

    }
}
