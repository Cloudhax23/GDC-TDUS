/**** 
 * Created by: Qadeem Qureshi
 * Date Created: Mar 03, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Mar 07, 2022
 * 
 * Description: Spawning of targets and platforms
****/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [Header("Target/Platform spawner settings:")]
    private List<GameObject> floatingCubes; //List of the platforms availble
    private GameObject endZone; //The portal
    private int listIndex = 0; //The counter for which platform we are on
    public GameObject pinPrefab; //The bowling pin prefab
    public Vector2 distanceToPins = new Vector2(30, 100); //Min/max distance for spawning a bowling pin from you.

    // Start is called before the first frame update
    //Handles the intiation of objects as well as sorting cubes by distance. Also disables the visibility of platforms on init.
    void Start()
    {
        floatingCubes = GameObject.FindGameObjectsWithTag("FloatingCube").OrderBy(x => (x.transform.position - Camera.main.transform.position).magnitude).ToList();
        endZone = GameObject.FindGameObjectWithTag("Endzone");
        floatingCubes.ForEach(x => x.GetComponent<Renderer>().enabled = false);
        endZone.GetComponent<Renderer>().enabled = false;
        SpawnNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Invoking this from this script or others will result in a new bowling pin to be spawned
    public void SpawnNewTarget()
    {
        Vector2 randomInCircle = Random.insideUnitCircle * Random.Range(distanceToPins.x, distanceToPins.y);
        Instantiate(pinPrefab, new Vector3(Camera.main.transform.position.x + randomInCircle.x, Camera.main.transform.position.y, Camera.main.transform.position.z + randomInCircle.y), Camera.main.transform.rotation);
    }
    //Invoking this from this script or another will cause the platform to become visible and active.
    public void SpawnNextFloor()
    {
        if (floatingCubes.Count > listIndex)
        {
            floatingCubes[listIndex].GetComponent<Renderer>().enabled = true;
            listIndex++;
        }
        else
        {
            endZone.GetComponent<Renderer>().enabled = true;
        }
    }
}
