﻿using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {

    const float DEAD_ZONE_HEIGHT = -5;
    public float maxSpeed = 30;
    private bool isDucking = false;
    public float jumpForce = 30;
    public Weapon currentWeapon;
    private Vector3 wayPtPos;
    

    private Vector3 startPosition;
    private new Rigidbody2D rigidbody2D;

    // Use this for initialization
    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	    if(transform.position.y < DEAD_ZONE_HEIGHT)
        {
            //FindObjectOfType<GM>().LifeLost();
            Die();
        }

        var x_force = Input.GetAxis("Horizontal");
        rigidbody2D.velocity += Vector2.right * x_force;
        rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity, maxSpeed);
        if(Input.GetButtonDown("Jump") && rigidbody2D.velocity.y == 0)
        {
            rigidbody2D.velocity += Vector2.up * jumpForce;
        }
        if(Input.GetButtonDown("Fire1") && currentWeapon != null)
        {
            currentWeapon.Attack();
        }
        //Duck if arrow down
        if(Input.GetAxis("Vertical") < 0 && !isDucking)
        {
            transform.localScale *= .5f;
            isDucking = true;
        }

        else if (Input.GetAxis("Vertical") >= 0 && isDucking)
        {
            transform.localScale *= 2f;
            isDucking = false;  
        }
        //Flip direction of character
        if(rigidbody2D.velocity.x > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else if(rigidbody2D.velocity.x < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
	}
    public void SetWayPt(Vector3 wayptSetter)
    {
        wayPtPos = wayptSetter;
    }
    public void Die()
    {
        if(wayPtPos != null)
            transform.position = wayPtPos;
        else
        {
            transform.position = startPosition;
        }
        rigidbody2D.velocity = new Vector2();
        FindObjectOfType<GM>().LifeLost();
    }
}
