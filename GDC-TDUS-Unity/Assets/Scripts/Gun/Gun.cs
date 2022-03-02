using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int max_shots = 30;
    public GameObject bulletPrefab;
    public Transform fire_point;
    public Transform player_camera;
    public float time_to_reload = 1.5f;
    public float fire_rate = 0.1f;
    public bool can_fire = true;
    public float fire_time = 0;
    public int available_shots;
    public float bullet_life = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        available_shots = max_shots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
            FireWeapon();
    }
    
    private void FireWeapon()
    {
        if(can_fire && Time.time > fire_time && available_shots > 0)
        {
            fire_time = Time.time + fire_rate;
            available_shots--;
            GameObject bulletObject = Instantiate(bulletPrefab, fire_point.position, fire_point.rotation);
            Destroy(bulletObject, bullet_life);
        }
    }
    private void ReloadWeapon()
    {

    }
}
