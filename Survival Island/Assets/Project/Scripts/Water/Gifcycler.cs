using UnityEngine;
using System.Collections;

public class Gifcycler : MonoBehaviour
{

    public Texture2D[] GifImages;
    public Material toChange;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(RotateTowardsClosestWayPoint(0));
    }

    IEnumerator RotateTowardsClosestWayPoint(int i)
    {
        if (i == GifImages.Length)
        {
            i = 0;
        }
        toChange.SetTexture("_ShadowTex", GifImages[i]);
        i++;
        yield return 0;
        StartCoroutine(RotateTowardsClosestWayPoint(i));
    }
}