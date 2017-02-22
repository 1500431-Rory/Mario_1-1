using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class power_up : MonoBehaviour {

    public enum powerup_type { one_up, mushroom, flower, star };
    public powerup_type my_powerup_type;
    public float speed = 6.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;
    public Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f); // normalised direction the powerup will move in

    Vector3 start_position; // start position of the powerup

    Vector3 start_direction; // start direction of the powerup

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
        // get character controller on the powerup
        CharacterController controller = GetComponent<CharacterController>();

        // check to see if the powerup is on the ground
        if (controller.isGrounded)
        {
            // set character controller moveDirection to be the direction I want the powerup to move in
            moveDirection = direction;
            moveDirection *= speed;
        }


        // apply gravity to movement direction
        moveDirection.y -= gravity * Time.deltaTime;

        // make the call to move the character controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.tag == "Player")
        {
            // check if a player has collided with the powerup, allow them to collect it and destroy the powerup object
            other.collider.gameObject.GetComponent<Player>().collect_powerup((int)my_powerup_type);
            Destroy(this.gameObject);
        }
        if(other.gameObject.CompareTag("Pipe"))
        {
            // we've hit the pipe

            // flip the direction of the enemy
            direction = -direction;
        }
    }
}
