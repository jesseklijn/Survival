using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Underwater : MonoBehaviour
{

    public WorldSettings worldSettings;
    public bool isUnderwater = false;
    public BlurOptimized blur;
    public ColorCorrectionCurves colorCorrection;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < worldSettings.waterLevelPosition)
        {
            isUnderwater = true;
            changeWatervision();
        }
        else
        {
            isUnderwater = false;
            changeWatervision();
        }
    }

    void changeWatervision()
    {
        if (isUnderwater == true)
        {
            RenderSettings.fogColor = new Color(0.2F, 0.65F, 0.77F, 0.5F);
            RenderSettings.fogDensity = 0.03F;
            blur.enabled = true;
            //colorCorrection.blueChannel
        }
        else
        {
            RenderSettings.fogDensity = 0.002F;
            RenderSettings.fogColor = new Color(0.61F,0.51F,0.4F,0.5F);
            blur.enabled = false;
            colorCorrection.enabled = false;
        }
    }
}


