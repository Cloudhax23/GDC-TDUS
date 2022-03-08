/**** 
 * Created by: Qadeem Qureshi
 * Date Created: Mar 03, 2022
 * 
 * Last Edited by: NA
 * Last Edited: Mar 07, 2022
 * 
 * Description: Charachter controller and movement
****/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Player control settings:")]
    public float speed = 7.5f; //How fast we move
    public float jumpSpeed = 8.0f; //The speed of our jumps
    public float gravity = 20.0f; //Gravity
    public Camera playerCamera; //Main camera from scene
    public float lookSpeed = 2.0f; //Camera sensitivity
    public float lookXLimit = 45.0f; //No neck breaking

    private CharacterController characterController; //Charachter controller unity component
    private Vector3 moveDirection; //Variable to check direction
    private Vector2 rotation; //Variable to check rotation

    [HideInInspector]
    public bool canMove = true;

    //Initialize by storing prevalent charachter control info
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;
    }

    //Based from: https://unity.com/fps-sample
    void Update()
    {
        if (characterController.isGrounded)
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            float curSpeedX = canMove ? speed * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? speed * Input.GetAxis("Horizontal") : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
            rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
            transform.eulerAngles = new Vector2(0, rotation.y);
        }
        //What happens when we drop too far
        if(transform.position.y <= -15)
        {
            GameManager.GM.LostLife();
        }
    }

    //When we land on a cube, we want to spawn the next bowling pin
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "FloatingCube")
        {
            hit.transform.gameObject.tag = "Untagged";
            TargetSpawner ts = Camera.main.GetComponent<TargetSpawner>();
            ts.SpawnNewTarget();
        }
    }
}
