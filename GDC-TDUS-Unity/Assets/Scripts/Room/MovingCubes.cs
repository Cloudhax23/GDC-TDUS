using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCubes : MonoBehaviour
{
    private int max_distance = 10;
    private float current_distance = 0;
    private float velocity = .125f;
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.parent = null;
        }
    }
}
