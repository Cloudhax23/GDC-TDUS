/**** 
 * Created by: Qadeem Qureshi
 * Date Created: Mar 03, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Mar 07, 2022
 * 
 * Description: Endzone sphere collision handling
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endzone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Once the player enters the sphere, they are teleported to the next scene
    private void OnTriggerEnter(Collider other)
    {
        GameManager.GM.NextLevel();
    }
}
