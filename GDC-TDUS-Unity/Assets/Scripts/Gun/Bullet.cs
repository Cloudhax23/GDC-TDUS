using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 current_pos;
    private Vector3 new_pos;
    public float bullet_speed = 345;
    public float hit_force = 50f;

    // Start is called before the first frame update
    void Start()
    {
        new_pos = transform.position;
        current_pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocity = transform.forward * bullet_speed;
        new_pos += velocity * Time.deltaTime;
        Vector3 direction = new_pos - current_pos;
        float distance = direction.magnitude;
        RaycastHit hit;

        if (Physics.Raycast(new_pos, direction, out hit, distance))
        {
            if(hit.rigidbody != null)
            {
                print("We hit the pins!");
                hit.rigidbody.AddForce(direction * hit_force);
            }
            current_pos = hit.point;
        }

        transform.position = new_pos;
        current_pos = new_pos;
    }
}
