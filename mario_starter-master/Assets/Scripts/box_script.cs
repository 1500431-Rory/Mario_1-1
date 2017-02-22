using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box_script : MonoBehaviour {

    public enum box_type { empty, coin, one_up, mushroom, flower, star };

    GameObject[] enemies;
    GameObject game_logic;

    public GameObject obj_to_spawn;

    public box_type my_box_type;

    Transform spawn_position;
    Transform box_collider;

    float timer;

    bool enemy_is_colliding;
    bool moving_up;
    bool moving_down;
    bool is_interacting;
    bool has_been_collected = false;

    public float movement_dist;
    public float movement_speed;

	// Use this for initialization
	void Start ()
    {
        // initialise some variables that are children of the main prefab
        spawn_position = this.gameObject.transform.GetChild(0);
        box_collider = this.gameObject.transform.GetChild(1);
        // find game logic script
        game_logic = GameObject.FindGameObjectWithTag("game_logic");
    }
	
	// Update is called once per frame
	void Update ()
    {
        update_box_position();
	}

    void update_box_position()
    {
        // check if box is moving up
        if(moving_up)
        {
            // move the box up
            timer++;
            transform.position = new Vector3(transform.position.x, (transform.position.y + movement_speed), transform.position.z);
            if (timer >= movement_dist)
            {
                // if the box reached its destination, set it to move down
                moving_up = false;
                moving_down = true;
            }
        }
        if(moving_down)
        {
            // move box down again
            timer--;
            transform.position = new Vector3(transform.position.x, (transform.position.y - movement_speed), transform.position.z);
            if(timer <= 0)
            {
                // stop moving the box
                moving_down = false;
            }
        }
    }

    public void set_colliding_enemy(bool is_colliding)
    {
        enemy_is_colliding = is_colliding;
    }

    public void box_collision()
    {
       Debug.Log("player collided with the box");
       // check if an enemy is on the box
       if (enemy_is_colliding)
       {
           // get all enemies in the scene, find the one colliding right now
           enemies = GameObject.FindGameObjectsWithTag("enemy");
           foreach (GameObject my_enemy in enemies)
           {
              if (my_enemy == box_collider.GetComponent<box_collider_script>().get_colliding_enemy())
              {
              // destroy the enemy
              Destroy(my_enemy);
              }
           }
        }
        // check if box is not empty
        if (!has_been_collected)
        {
            // check if the box contains an object that needs to be spawned
            if (my_box_type != box_type.coin && my_box_type != box_type.empty)
            {
                // instantiate object if there is something the box needs to spawn
                Instantiate(obj_to_spawn, spawn_position.position, Quaternion.identity);
            }
            // if the box is a coin box...
            if (my_box_type == box_type.coin)
            {
                // ...add to player score
                game_logic.GetComponent<GameLogic>().add_score(100);
            }
            // make sure player cannot collect stuff from the same box more than once
            has_been_collected = true;
         }
         // reset animation
         moving_up = true;
    }
}
