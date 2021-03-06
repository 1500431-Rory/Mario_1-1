﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    // variables taken from CharacterController.Move example script
    // https://docs.unity3d.com/ScriptReference/CharacterController.Move.html
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public int stage = 1;
    private bool firePower = false;
    private bool invincible = false;
    private Vector3 moveDirection = Vector3.zero;

    public int Lives = 3; // number of lives the player hs


    Vector3 start_position; // start position of the player


    void Start()
    {
        // record the start position of the player
        start_position = transform.position;
        updateModel();
    }

    public void Reset()
    {
        // reset the player position to the start position
        transform.position = start_position;
        stage = 2;
        updateModel();
    }

    void Update()
    {
        // get the character controller attached to the player game object
        CharacterController controller = GetComponent<CharacterController>();


        if (Input.GetButton("Run"))
        {
            speed = 9.0F;
        }
        else
        {
            speed = 6.0F;
        }

        // moveVelocity += moveDirection.x;
        //moveVelocity %= speed;

        //moveDirection.x = moveVelocity;
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection.x *= speed;


        // check to see if the player is on the ground
        if (controller.isGrounded)
        {
            //Debug.Log("true");
            // check to see if the player should jump
            moveDirection.y = 0;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        else {
            //Debug.Log("false");
            // apply gravity to movement direction
            moveDirection.y -= gravity * Time.deltaTime;
        }
        // make the call to move the character controller
        controller.Move(moveDirection * Time.deltaTime);
        //Debug.Log(moveDirection.y);

        if (Input.GetButtonDown("Run") && firePower)
        {
            shoot();
        }

    }

    public Vector3 getMoveDir()
    {
        return moveDirection;
    }

    public void setVelY(float newVel)
    {
        moveDirection.y = newVel;
    }

    public void enemyHit()
    {
        if (!invincible)
        {
            StartCoroutine(damage());
        }
    }

    public void collect_powerup(int powerUp)
    {
        // 1up 0, mush 1, flower 2, star 3

        switch (powerUp)
        {
            case 0  : oneUp(); break;
            case 1  : mushroom(); break;
            case 2  : fireFlower(); break;
            case 3  : StartCoroutine(starPower()); break;
        }

    }

    void oneUp()
    {
        Lives++;
        return;
    }

    void mushroom()
    {
        stage = 2;
        updateModel();
        return;
    }

    void fireFlower()
    {
        stage = 3;
        firePower = true;
        updateModel();
        return;
    }

    void shoot()
    {
        //Instantiate(Resources.Load("koopaShell"));
    }

    IEnumerator damage()
    {
        stage--;
        if (stage == 2)
        {
            firePower = false;
        }
        else
        {
            transform.localPosition -= new Vector3(0, 0.5f, 0);
        }
        
        if (stage <= 0)
        {
            // player die
            Lives--;
            Reset();
        }
        updateModel();
        invincible = true;
        yield return new WaitForSeconds(0.5F);
        invincible = false;
    }

    IEnumerator starPower()
    {
        invincible = true;
        yield return new WaitForSeconds(5F);
        invincible = false;
    }

    void updateModel()
    {
        if (stage < 3)
        {
            gameObject.GetComponent<CharacterController>().height = stage;
            gameObject.GetComponent<CapsuleCollider>().height = stage;
            transform.localScale = new Vector3(1, stage * 0.5F, 1);
        }
        else if ( stage == 3)
        {
            gameObject.GetComponent<CharacterController>().height = 2;
            gameObject.GetComponent<CapsuleCollider>().height = 2;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

}