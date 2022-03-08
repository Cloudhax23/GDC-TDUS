/**** 
 * Created by: Qadeem Qureshi
 * Date Created: Mar 03, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Mar 07, 2022
 * 
 * Description: Movement of cubes on second scene
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCubes : MonoBehaviour
{
    [Header("Cube movement settings:")]
    public int max_distance = 10; //Max distance before switching direction
    private float current_distance = 0; //Current distance counter
    public float velocity = .125f; //The speed at which the cubes move aat
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // This function determines the directionality of the cube as well as the distance it can travel
    void FixedUpdate()
    {
        Vector3 position = gameObject.transform.position;
        gameObject.transform.position = new Vector3(position.x, position.y + velocity, position.z);
        current_distance += velocity;
        if (current_distance == max_distance || current_distance == -max_distance)
        {
            velocity = -velocity;
        }
    }

    // Platforms tend to clip the player object. Set the parent of the player object to the platform to ensure correct collision
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    //Once the player jumps off the platform, remove from the platform they were on originally.
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
