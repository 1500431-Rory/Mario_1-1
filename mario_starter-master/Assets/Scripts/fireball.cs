using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public float speed = 8.0f;
    public float gravity = 20.0f;
    public float bounce_height = 20.0f;

    public Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);
    Vector3 start_position;
    Vector3 start_direction;
    Vector3 moveDirection = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        // record the start position
        start_position = transform.position;

        // record the start direction
        start_direction = direction;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // get the character controller attached to the enemy game object
        CharacterController controller = GetComponent<CharacterController>();

        // set character controller moveDirection to be the direction I want the enemy to move in
        moveDirection = direction;
        moveDirection *= speed;

        // check to see if the enemy is on the ground
        if (controller.isGrounded)
        {
            moveDirection.y = bounce_height;
        }

        // apply gravity to movement direction
        moveDirection.y -= gravity * Time.deltaTime;

        // make the call to move the character controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // find out what we've hit
        if (hit.collider.gameObject.CompareTag("Pipe"))
        {
            // we've hit the pipe

            // flip the direction of the enemy
            direction = -direction;
        }
    }
}
