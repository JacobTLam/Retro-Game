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

    public bool sprung;
    public LayerMask whatIsSpr;

    public bool tele;
    public LayerMask whatIsTele;

    private Animator theAnimator;

    public GameManager theGM;

    //public bool ceiling;
    //public LayerMask whatIsCei;
    //public Transform ceiChecker;
    //public float ceiCheckerRad;

    private LivesManager theLM;


    // Start is called before the first frame update 
    void Start()
    {
        theLM = FindObjectOfType<LivesManager>();

        theRB2D = GetComponent<Rigidbody2D>();
        theAnimator = GetComponent<Animator>();
        dashForce = 100;
        flip = -1;
        

        airTimeCounter = airTime;
    }
    // Update is called once per frame 
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            airTimeCounter = 0;
        }

        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            canMove = true;
        }

        if (sprung == true)
        {
            theRB2D.velocity = new Vector2(theRB2D.velocity.x, 50);
        }

        if(tele == true)
        {
            Vector2 newpos = new Vector2(-8.5f, 3.5f);
            theRB2D.MovePosition(newpos);
        }

        //if (ceiling == true)
        //{
         //   transform.localScale = new Vector2(1f, -1f);
           // Physics2D.gravity = new Vector2(0, 0);
       // }
        


    }
    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(grdChecker.position, grdCheckerRad, whatIsGrd);
        sprung = Physics2D.OverlapCircle(grdChecker.position, grdCheckerRad, whatIsSpr);
        tele = Physics2D.OverlapCircle(grdChecker.position, grdCheckerRad, whatIsTele);
        //ceiling = Physics2D.OverlapCircle(ceiChecker.position, ceiCheckerRad, whatIsCei);
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

            theAnimator.SetFloat("Speed", Mathf.Abs(theRB2D.velocity.x));

            if (theRB2D.velocity.x > 0)
                transform.localScale = new Vector2(1f, 1f);
            else if (theRB2D.velocity.x < 0)
                transform.localScale = new Vector2(-1f, 1f);
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


        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            
            if (airTimeCounter > 0)
            {
                theRB2D.velocity = new Vector2(theRB2D.velocity.x, jumpForce);
                airTimeCounter -= Time.deltaTime;
            }

        }

        if (grounded)
        {
            airTimeCounter = airTime;
        }

        
        
        theAnimator.SetBool("Grounded", grounded);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Spike")
        {
            Debug.Log("Ouch!");
            //theGM.GameOver();
            theGM.Reset();
            theLM.TakeLife();
        }
    }
}
