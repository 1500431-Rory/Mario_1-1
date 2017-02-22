using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_collider_script : MonoBehaviour {

    GameObject colliding_enemy;
    public bool is_top_collider;
    public float hit_vel = -5;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public GameObject get_colliding_enemy()
    {
        return colliding_enemy;
    }

    void OnTriggerEnter(Collider other)
    {
        if (is_top_collider)
        {
            if (other.gameObject.tag == "enemy")
            {
                // determine if an enemy is on the box
                colliding_enemy = other.gameObject;
                transform.parent.GetComponent<box_script>().set_colliding_enemy(true);
            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                // execute player collision function when the player collides with the bottom collider of the box
                transform.parent.GetComponent<box_script>().box_collision();
                // makes the player fall down instantly after colliding with the box - without this line the collision for the box will be messed up
                other.GetComponent<Player>().y_set_vel(hit_vel);
            }
//    public void y_set_vel(float new_vel)
//    {
//        moveDirection.y = new_vel;
//    } ..add this function to player
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(is_top_collider)
        {
            transform.parent.GetComponent<box_script>().set_colliding_enemy(false);
        }
    }
}
