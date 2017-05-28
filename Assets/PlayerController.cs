using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    // TODO : Still not okay that the movement is extremely smooth.
    
    Rigidbody rb;

    [SerializeField]
    private float speed;    
    [SerializeField]
    private float jumpForce;
    
    bool grounded = true;    
    bool canDoubleJump;

    
    
    // Use this for initialization
    void Start () {        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;       
    }
	
	// Update is called once per frame
	void Update ()
    {
        // we need some axis derived from camera but aligned with floor plane
        Vector3 forward = Camera.main.transform.TransformDirection(Vector3.forward);

        forward.y = 0f;
        forward = forward.normalized;
        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 walkDirection = (h * right + v * forward);



        // if you are not moving you will come to a halt.
        rb.velocity = new Vector3(0, rb.velocity.y, 0);
        
        rb.angularVelocity = Vector3.zero;

        if (Input.GetKey("q"))
        {  
            rb.AddForce(walkDirection * speed * Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {    
            rb.AddForce(walkDirection * speed * Time.deltaTime);
        
        }

        if (Input.GetKey("z"))
        {   
            rb.AddForce(walkDirection * speed * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {         
            rb.AddForce(walkDirection * speed * Time.deltaTime);
        }
 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(grounded == true)
            {
                //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.velocity = new Vector3(1, 0, 1);
                rb.AddForce(0, jumpForce, 0);
                
                canDoubleJump = true;
                grounded = false;
               
            }
            else
            {
                if(canDoubleJump == true)
                {
                    canDoubleJump = false;
                    //rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    rb.velocity = new Vector3(1, 0, 1);
                    rb.AddForce(0, jumpForce / 1, 0);
                    

                }

            }
        }

        if (Input.GetKey("e"))
        {
           rb.transform.Rotate(Vector3.up * 5);
        }
        if (Input.GetKey("a"))
        {
            rb.transform.Rotate(-Vector3.up * 5);
        }
    }

    
     void OnCollisionEnter(Collision collide)
    {
        
        if (rb.velocity.y < 1)
        {

            if (collide.gameObject.tag == "Ground")
            {
                grounded = true;
            }

        }
        
    }

    void OnCollisionExit(Collision collide)
    {
        
    }
}
