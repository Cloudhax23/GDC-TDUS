/**** 
 * Created by: Qadeem Qureshi
 * Date Created: Mar 03, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Mar 07, 2022
 * 
 * Description: Gun shooting and handling
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun settings:")]
    public static int max_shots = 30; //How many bullets we have
    public GameObject bulletPrefab; //The bullet prefab
    public Transform fire_point; //Our bullet origin position relative to the gun
    public Transform player_camera; //The camera of our player
    public float time_to_reload = 1.5f; //How long to reload
    public float fire_rate = 0.1f; //Time between shots
    public bool can_fire = true; //Can fire by default
    public float fire_time = 0; //Track shots
    public static int available_shots; //Track availble ammo
    public float bullet_life = 3.5f; //How long a bullet stays alive in scene before destroyed

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        available_shots = max_shots;
    }

    // Update is called once per frame
    //Handles firing, reload, etc
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireWeapon();
            can_fire = false;
        }
        if (Input.GetMouseButtonUp(0) && available_shots > 0)
            can_fire = true;

        if (Input.GetKeyUp(KeyCode.R))
            ReloadWeapon();
        
    }
    
    //Called when fire is click on mouse.
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
    //Called when charachter reloads
    private void ReloadWeapon()
    {
        if(available_shots != max_shots)
        {
            can_fire = false;
            StartCoroutine("ReloadInProgress");
            Invoke("ReloadInProgress", time_to_reload);
        }
    }
    //Set our shots to max shots after reload cycle is done
    private void ReloadInProgress()
    {
        available_shots = max_shots;
        can_fire = true;
    }
}
