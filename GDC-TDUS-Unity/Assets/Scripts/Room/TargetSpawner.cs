using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    private List<GameObject> floatingCubes;
    private GameObject endZone; 
    private int listIndex = 0;
    public GameObject pinPrefab;
    public Vector2 distanceToPins = new Vector2(30, 100);

    // Start is called before the first frame update
    void Start()
    {
        floatingCubes = GameObject.FindGameObjectsWithTag("FloatingCube").ToList();
        endZone = GameObject.FindGameObjectWithTag("Endzone");
        floatingCubes.ForEach(x => x.GetComponent<Renderer>().enabled = false);
        endZone.GetComponent<Renderer>().enabled = false;
        SpawnNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnNewTarget()
    {
        Vector2 randomInCircle = Random.insideUnitCircle * Random.Range(distanceToPins.x, distanceToPins.y);
        Instantiate(pinPrefab, new Vector3(Camera.main.transform.position.x + randomInCircle.x, Camera.main.transform.position.y, Camera.main.transform.position.z + randomInCircle.y), Camera.main.transform.rotation);
    }

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
