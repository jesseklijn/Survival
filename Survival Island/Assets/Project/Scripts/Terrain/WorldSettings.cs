using UnityEngine;
using System.Collections;

public class WorldSettings : MonoBehaviour {

    public enum Setting {
        Day,
        Night,
        Dawn,
        Dusk

    }
    public Light directionalLighting;
    public Setting setting;
    public float dayTime = 600, nightTime = 300,duskTime = 50,dawnTime = 50, currentTime = 500;
    public float timePerSecond = 1;
    public float waterLevelPosition = 3;

	// Use this for initialization
	void Start () {
	
	}
}
