using UnityEngine;
using System.Collections;

public class TerrainHeightMapGenerator : MonoBehaviour {

    public TerrainData terrainData;
    public Heightmapbuilder heightMapBuilder;
	// Use this for initialization
	void Start () {
        //int ewidth = terrainData.heightmapWidth;
        //int eheight = terrainData.heightmapHeight;
        //heightmap = terrainData.GetHeights(0, 0, ewidth, eheight);
        ////...create and modify height map as you like
        //// keep floats in 0 to 1 range
        //terrainData.SetHeights(0, 0, heightmap);
        //terrain.Flush();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
