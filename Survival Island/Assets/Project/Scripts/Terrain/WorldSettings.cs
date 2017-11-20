using UnityEngine;
using System.Collections;

public class WorldSettings : MonoBehaviour {

    public enum Setting {
        Day,
        Night,
        Dawn,
        Dusk

    }
    public enum Weather
    {
        ClearSky,
        Sunny,
        Cloudy,
        Misty,
        Rain,
        Storm,
        Snow
    }

    public Light directionalLighting;
    public Setting setting;
    public Weather weather;
    public float dayTime = 600, nightTime = 300,duskTime = 50,dawnTime = 50, currentTime = 500;
    public float timePerSecond = 1;
    public float waterLevelPosition = 12.3F;
    public ParticleSystem sky;
    public GameObject SunraysUnderwater;
    public GameObject SeaWater;
    public Vector3 WaterVelocity;

	// Use this for initialization
	void Start () {
       WaterVelocity = SeaWater.GetComponent<Renderer>().material.GetVector("WaveSpeed");
    }
    private void FixedUpdate()
    {
        //SeaWater.GetComponent<Renderer>().material.SetVector("WaveSpeed", new Vector4());
    }
}
