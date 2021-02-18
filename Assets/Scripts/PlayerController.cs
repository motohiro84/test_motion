using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

public class PlayerController : StrixBehaviour
{
  public GameObject Player;
  public GameObject Camera;
  public float speed;
  private Vector3 playerPos;
  private Transform PlayerTransform;
  private Transform CameraTransform;
  private float ii;
  private Animator animator;
  private Rigidbody rb;
  private float jumpForce = 300.0f;
  private bool Ground = true;
  private bool key = false;

  string state;
  string prevState;

  // Use this for initialization
  void Start()
  {
    rb = GameObject.Find("Player").GetComponent<Rigidbody>();
    PlayerTransform = GameObject.Find("camera").transform.parent;
    playerPos = GameObject.Find("Player").transform.position;
    animator = GameObject.Find("Player").GetComponent<Animator>();
    CameraTransform = GameObject.Find("camera").GetComponent<Transform>();
    CameraTransform.transform.Rotate(360f, 0, 0);

  }

  // Update is called once per frame
  void Update()
  {

    TPS();
    ChangeState();
    ChangeAnimation();
    Move();

    // var info = animator.GetCurrentAnimatorClipInfo(0);
    // Debug.Log(info[0].clip.name);
  }

  void ChangeState()
  {
    if (Ground == true)
    {
      if (key == true)
      {
        if (Input.GetKey(KeyCode.LeftShift))
        {
          state = "RUN";
        }
        else
        {
          state = "WALK";
        }
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
    if (Input.GetKeyDown(KeyCode.LeftControl))
    {
      state = "SPRINT";
    }
  }

  void ChangeAnimation()
  {
    if (prevState != state)
    {
      switch (state)
      {
        case "JUMP":
          animator.SetTrigger("Jump");
          animator.SetBool("Walk", false);
          animator.SetBool("Run", false);
          break;
        case "SPRINT":
          animator.SetTrigger("Sprint");
          animator.SetBool("Walk", false);
          animator.SetBool("Run", false);
          break;
        case "WALK":
          animator.SetBool("Walk", true);
          animator.SetBool("Run", false);
          animator.SetBool("Idle", false);
          break;
        case "RUN":
          animator.SetBool("Walk", false);
          animator.SetBool("Run", true);
          animator.SetBool("Idle", false);
          break;
        default:
          animator.SetBool("Walk", false);
          animator.SetBool("Run", false);
          animator.SetBool("Idle", true);
          break;
      }
      prevState = state;
    }
  }
  void TPS()
  {
    if (isLocal == false)
    {
      return;
    }
    float X_Rotation = Input.GetAxis("Mouse X");
    float Y_Rotation = Input.GetAxis("Mouse Y");
    PlayerTransform.transform.Rotate(0, X_Rotation, 0);

    //オイラー角と、マウスの変化量を足したものをラジアンに変換
    ii = (Camera.transform.localEulerAngles.x - Y_Rotation) * Mathf.Deg2Rad;
    //sin関数で-1~1の範囲に変換
    ii = Mathf.Sin(ii);

    //角度の制限をつけてやる
    if (ii > -0.6f && ii < 0.2f)
    {
      CameraTransform.transform.Rotate(-Y_Rotation, 0, 0);
      //float kk = Y_Rotation;
    }
  }

  void Move()
  {
    if (isLocal == false)
    {
      Camera.SetActive(false);
      return;
    }
    if (Ground == true)
    {
      if (Input.GetButtonDown("Jump"))
      {
        rb.AddForce(transform.up * jumpForce, ForceMode.Force);
        //rb.velocity += transform.up * jumpForce * time.deltatime / mass
        //rb.velocity += transform.up * jumpForce / mass
        Ground = false;
      }

    }


    float x = Input.GetAxisRaw("Horizontal");
    float z = Input.GetAxisRaw("Vertical");

    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
    {
      PlaySpeed();
      Vector3 dir = (transform.right * x) + (transform.forward * z);
      PlayerTransform.transform.position += dir * speed * Time.deltaTime;
      key = true;
    }
    else
    {
      key = false;
    }

    //float angleDir = PlayerTransform.transform.eulerAngles.y * (Mathf.PI / 180.0f);
    //Vector3 dir1 = new Vector3(Mathf.Sin(angleDir), 0, Mathf.Cos(angleDir));
    //Vector3 dir2 = new Vector3(-Mathf.Cos(angleDir), 0, Mathf.Sin(angleDir));

    //if ( Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) )
    //{
    //    PlaySpeed();

    //    if (Input.GetKey(KeyCode.W))
    //    {
    //        PlayerTransform.transform.position += dir1 * speed * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.A))
    //    {
    //        PlayerTransform.transform.position += dir2 * speed * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.D))
    //    {
    //        PlayerTransform.transform.position += -dir2 * speed * Time.deltaTime;
    //    }
    //    if (Input.GetKey(KeyCode.S))
    //    {
    //        PlayerTransform.transform.position += -dir1 * speed * Time.deltaTime;
    //    }
    //    key = true;
    //}
    //else
    //{
    //    key = false;
    //}

  }

  void PlaySpeed()
  {
    if (state == "RUN")
    {
      speed = 4;
    }
    else if (state == "SPRINT")
    {
      //speed = 400;
      float force = 4000;
      float x = Input.GetAxisRaw("Horizontal");
      float z = Input.GetAxisRaw("Vertical");
      Vector3 dir = (transform.right * x) + (transform.forward * z);
      GetComponent<Rigidbody>().AddForce(dir * force);
    }
    else
    {
      speed = 2;
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