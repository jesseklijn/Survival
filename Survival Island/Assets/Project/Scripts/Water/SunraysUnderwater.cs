using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunraysUnderwater : MonoBehaviour
{

    bool isInCycle;
    public WorldSettings worldSettings;
    public Transform player;

    public int rayRadius = 10; //spawn radius of the sunray through the water
    public int maxRange = 10;

    public int minInterval = 2, maxInterval = 4, minDuration = 6, maxDuration = 10;
    
    Light lightComponent;

    public bool revert = true;
    // Use this for initialization
    void Start()
    {
        lightComponent = transform.GetComponent<Light>();
        //SpawnRay();


    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnRay()
    {
        Vector3 pos = new Vector3(player.transform.position.x + Random.Range(-rayRadius, rayRadius), worldSettings.waterLevelPosition, player.position.z + Random.Range(-rayRadius, rayRadius));
        
        RaycastHit hit;
        if (Physics.Raycast(pos, Vector3.down, out hit, 12))
        {
            if (hit.distance > 4 && hit.distance < 12)
            {
                //Hit is sucessful
                Debug.Log("Hit is sucessful");
                transform.position = pos;
                lightComponent.range = 0;
                StartCoroutine(Shine(0, maxRange, Random.Range(minDuration,maxDuration)));
                Debug.Log("Distance is: " + hit.distance);
            }
            else
            {
                //Hit is either too close or too far away.
                Debug.Log("Hit is either too close or too far away.");
                //Specify:
                if (hit.distance <= 4)
                {
                    Debug.Log("Distance is too less. It should try again in a new positon. (" + hit.distance + ")");
                    StartCoroutine(Wait(maxInterval));
                    
                  
                    //Distance is too less. It should try again in a new positon.

                }
                else if (hit.distance >= 12)
                {
                    Debug.Log("Distance is too much. It will try again. in (" + maxInterval + " seconds) Distance:(" + hit.distance + ")");
                    StartCoroutine(Wait(maxInterval));
                    
                    //Distance is too much. It will not try again.
                }
            }

        }
        else
        {
            StartCoroutine(Wait(maxInterval));
            Debug.Log("Distance is too much. It will try again. in ("+maxInterval+" seconds) Distance:(" + hit.distance + ")");
            //Distance is too much. It will not try again.
        }

    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        SpawnRay();

    }
    IEnumerator Shine(float PointA, float PointB, float time)
    {


        //Check if character is already moving..

        if (isInCycle == false)
        {

            isInCycle = true;

            float t = 0;
            //Move while time is still below 1
            while (t < 1)
            {
                t += Time.deltaTime / time;
                float a = Mathf.Lerp(PointA, PointB, t);

                lightComponent.range = a;
                transform.Rotate(new Vector3(0, 0.1F, 0), Space.World);
                yield return 0;

            }

            //Reset Routine
            isInCycle = false;

            if (revert == true)
            {
                revert = false;
                yield return new WaitForSeconds(Random.Range(minInterval,maxInterval));
                StartCoroutine(Shine(maxRange, 0, Random.Range(minDuration,maxDuration)));
                
            }
            else
            {
                revert = true;
                yield return new WaitForSeconds(Random.Range((minInterval/2), (maxInterval/2)));
                SpawnRay();
                
            }
        }


    }


}
