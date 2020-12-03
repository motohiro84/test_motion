﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]

public class BossController : MonoBehaviour
{
  public bool moveEnabled = true;
  public static string attackNum;

  public int maxHp = 20;
  public GameObject Player;

  [SerializeField]
  string targetTag = "Player";
  [SerializeField]
  // float deadTime = 3;
  bool attacking = false;
  int hp;
  float moveSpeed = 2f;
  Animator animator;
  Rigidbody rigidBody;
  Transform target;
  BossMotion bossMotion;
  AnimatorStateInfo animStateInfo;
  public static bool jumpKey;
  private float jumpForce = 1000.0f;

  private Vector3 destination;
  private Vector3 velocity;
  private Vector3 direction;
  public float forcePower;


  public int Hp
  {
    set
    {
      hp = Mathf.Clamp(value, 0, maxHp);
      if (hp <= 0)
      {
        // StartCoroutine(Dead());
      }
    }
    get
    {
      return hp;
    }
  }

  void Start()
  {
    jumpKey = false;
    animator = GetComponent<Animator>();
    rigidBody = this.gameObject.GetComponent<Rigidbody>();
    bossMotion = this.gameObject.GetComponent<BossMotion>();
    target = GameObject.FindGameObjectWithTag(targetTag).transform;
    InitCharacter();
  }

  void LateUpdate()
  {
    destination = target.position;
    if (moveEnabled)
    {
      Move();
    }
    else
    {
      Stop();
    }

    // if (BossMotion.state != "Attack1" && BossMotion.state != "Attack2" && BossMotion.state != "Attack3" && attacking)
    // {
    //   attacking = false;
    // }
    if (BossMotion.state == "Run")
    {
      if (AttackChara.Key && !attacking)
      {
        Attack();
      }
    }
  }

  void InitCharacter()
  {
    Hp = maxHp;
  }
  public void Jump()
  {
    rigidBody.AddForce(transform.up * jumpForce, ForceMode.Acceleration);
  }

  void Move()
  {
    direction = (destination - transform.position).normalized;
    transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
    if (jumpKey)
    {
      velocity = direction * moveSpeed * 5f;
    }
    else if (ChaseChara.Key && !jumpKey)
    {
      velocity = direction * moveSpeed * 3f;
      bossMotion.RunMotion();
    }
    else
    {
      velocity = direction * moveSpeed;
      bossMotion.WalkMotion();
    }
    this.transform.position += velocity * Time.deltaTime;
  }
  void Stop()
  {
    direction = (destination - transform.position).normalized;
    transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
  }

  void Attack()
  {
    attackNum = Random.Range(1, 4).ToString();
    attacking = true;
    moveEnabled = false;
    bossMotion.AttackMotion();
  }

  public void AttackTime()
  {
    attacking = false;
    moveEnabled = true;
    AttackChara.Key = false;
  }

}
