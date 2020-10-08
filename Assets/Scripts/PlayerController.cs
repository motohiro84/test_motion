using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerController : MonoBehaviour {
    public GameObject Player;
    public GameObject Camera;
    public float speed = 3;
    private Vector3 playerPos;
    private Transform PlayerTransform;
    private Transform CameraTransform;
    private float ii;
    private Animator animator;
    private Rigidbody rb;
    private float jumpForce = 400.0f;
    private bool Ground = true;
    private bool key = false;

    string state;
    string prevState;

    // Use this for initialization
    void Start () {
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        PlayerTransform = GameObject.Find("camera").transform.parent;
        playerPos = GameObject.Find("Player").transform.position;
        animator = GameObject.Find("Player").GetComponent<Animator>();
        CameraTransform = GameObject.Find("camera").GetComponent<Transform>();
 
    }
	
	// Update is called once per frame
	void Update () {
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        PlayerTransform.transform.Rotate(0, X_Rotation, 0);
        
        ii = Camera.transform.localEulerAngles.x;
        if (ii > 334 && ii < 360 || ii > 0 && 20 > ii)
        {
            CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
            float kk = Y_Rotation;
        }
        else
        {
           
            if (ii > 300)
            {
 
                if (Input.GetAxis("Mouse Y") < 0)
                {
 
                    CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
 
                }
            }
            else
            {
                if (Input.GetAxis("Mouse Y") > 0)
                {
 
                    CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
 
                }
 
            }
        }

        // GetInputKey();
        ChangeState();
        ChangeAnimation();
        Move();
    }

    // void GetInputKey()
    // {
    //     float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
 
    //     float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
 
    //     key = true;
    // }

        void ChangeState()
    {
        if (Ground == true )
        {
            if (key == true)
            {
                state = "WALK";
            }
            else
            {
                state = "IDLE";
            }
        }
        else
        {
            state = "JUMP";
        }
    }

        void ChangeAnimation()
    {
        if (prevState != state)
        {
            switch (state)
            {
                case "JUMP":
                    animator.SetBool("Jump", true);
                    animator.SetBool("Walk", false);
                    animator.SetBool("Idle", false);
                    break;
                case "WALK":
                    animator.SetBool("Jump", false);
                    animator.SetBool("Walk", true);
                    animator.SetBool("Idle", false);
                    break;
                default:
                    animator.SetBool("Jump", false);
                    animator.SetBool("Walk", false);
                    animator.SetBool("Idle", true);
                    break;
            }
            prevState = state;
        }
    }

    void Move()
    {
        if (Ground == true)
        {
            if (Input.GetButton("Jump"))
            {
                rb.AddForce(transform.up * jumpForce);
                Ground = false;
            }
 
        }
 


        float angleDir = PlayerTransform.transform.eulerAngles.y * (Mathf.PI / 180.0f);
        Vector3 dir1 = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir));
        Vector3 dir2 = new Vector3(-Mathf.Cos(angleDir), 0, Mathf.Sin(angleDir));

        if ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) )
        {
            if (Input.GetKey(KeyCode.W))
            {
                PlayerTransform.transform.position += dir1 * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.A))
            {
                PlayerTransform.transform.position += dir2 * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                PlayerTransform.transform.position += -dir2 * speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.S))
            {
                PlayerTransform.transform.position += -dir1 * speed * Time.deltaTime;
            }
            key = true;
        }
        else
        {
            key = false;
        }
 
    }

    void OnTriggerEnter(Collider col)
    {
       if (col.gameObject.tag == "Ground")
        {
            if (Ground == false)
                Ground = true;
        }
    }

}