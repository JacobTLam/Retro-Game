using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerControl : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private bool canMove;
    private Rigidbody2D theRB2D;
    public float dashForce;
    public float flip;

    public bool grounded;
    public LayerMask whatIsGrd;
    public Transform grdChecker;
    public float grdCheckerRad;

    public float airTime;
    public float airTimeCounter;

    // Start is called before the first frame update 
    void Start()
    {
        theRB2D = GetComponent<Rigidbody2D>();
        dashForce = 100;
        flip = -1;
        

        airTimeCounter = airTime;
    }
    // Update is called once per frame 
    void Update()
    {
        
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            canMove = true;
        }
        Debug.Log(theRB2D.position.y);
    }
    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(grdChecker.position, grdCheckerRad, whatIsGrd);

        MovePlayer();
        //Dash();
        //Teleport();
        Jump();

    }
    void MovePlayer()
    {
        if (canMove)
        {
            theRB2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, theRB2D.velocity.y);
        }
    }

    //void Dash()
    //{
        //if (Input.GetKeyDown(KeyCode.R))
        //{
            //theRB2D.velocity = new Vector2(dashForce, theRB2D.velocity.y);
        //}
    //}
    
    //void Teleport()
    //{
        //if (Input.GetKeyDown(KeyCode.T))
        //{
            //theRB2D.MovePosition(theRB2D.position * -1); 
        //}
    //}
    
    void Jump()
    {
        if (grounded == true)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce);
            }
        }

        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            if(airTimeCounter > 0)
            {
                theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce);
                airTimeCounter -= Time.deltaTime;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            airTimeCounter = 0;
        }

        if (grounded)
        {
            airTimeCounter = airTime;
        }
    }
}
