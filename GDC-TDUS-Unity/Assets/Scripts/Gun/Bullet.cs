/**** 
 * Created by: Qadeem Qureshi
 * Date Created: Mar 03, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Mar 07, 2022
 * 
 * Description: Bullet collision and handling
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet settings:")]
    private Vector3 current_pos; //For raycasting
    private Vector3 new_pos; //For raycasting
    public float bullet_speed = 345; //How fast our projectile is
    
    // Start is called before the first frame update
    void Start()
    {
        new_pos = transform.position;
        current_pos = transform.position;
    }

    // Update is called once per frame
    //Raycast from current pos to lerp'd next pos based on the speed of the bullet. Check if it collides with something. Delete target if so and spawn platform.
    void FixedUpdate()
    {
        Vector3 velocity = transform.forward * bullet_speed;
        new_pos += velocity * Time.deltaTime;
        Vector3 direction = new_pos - current_pos;
        float distance = direction.magnitude;
        RaycastHit hit;

        if (Physics.Raycast(new_pos, direction, out hit, distance))
        {
            if(hit.collider != null && hit.transform.tag == "Pins")
            {
                Destroy(hit.transform.gameObject);
                TargetSpawner ts = Camera.main.GetComponent<TargetSpawner>();
                ts.SpawnNextFloor();
                GameManager.GM.UpdateScore(100);
            }
            current_pos = hit.point;
        }

        transform.position = new_pos;
        current_pos = new_pos;
    }
}
