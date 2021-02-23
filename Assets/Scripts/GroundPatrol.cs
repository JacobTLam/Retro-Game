﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPatrol : MonoBehaviour
{
    public float moveSpeed;

    public float rayLength;

    private bool moveLeft;

    public Transform contactChecker;
    
    void Start()
    {
        moveLeft = true;
    }

    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        RaycastHit2D contactCheck = Physics2D.Raycast(contactChecker.position, Vector2.left, rayLength);

        if(contactCheck == true)
        {
            if(moveLeft == true)
            {
                transform.eulerAngles = new Vector2(0, -180);
                moveLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 0);
                moveLeft = true;
            }
        }
    }
}
