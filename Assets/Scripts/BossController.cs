using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public class BossController : MonoBehaviour
{
  public bool moveEnabled = true;
  public static string attackNum;

  public int maxHp = 20;

  [SerializeField]
  string targetTag = "Player";
  [SerializeField]
  // float deadTime = 3;
  bool attacking = false;
  int hp;
  float moveSpeed;
  Animator animator;
  Rigidbody rigidBody;
  NavMeshAgent agent;
  Transform target;
  BossMotion bossMotion;
  AnimatorStateInfo animStateInfo;
  public static bool jumpKey;
  private float jumpForce = 3000.0f;


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
    agent = GetComponent<NavMeshAgent>();
    bossMotion = this.gameObject.GetComponent<BossMotion>();
    target = GameObject.FindGameObjectWithTag(targetTag).transform;
    InitCharacter();
  }

  void Update()
  {
    if (moveEnabled)
    {
      if (!jumpKey)
      {
        Move();
      }
      else
      {
        JumpMove();
      }
    }
    else
    {
      Stop();
    }

    if (!jumpKey)
    {
      if (AttackChara.Key && !attacking)
      {
        attackNum = Random.Range(1, 4).ToString();
        Attack();
      }
      if (AttackChara.Key)
      {
        AttackTime();
      }
    }

  }


  void InitCharacter()
  {
    Hp = maxHp;
    moveSpeed = agent.speed;
  }

  public void Jump()
  {
    rigidBody.AddForce(transform.up * jumpForce);
    jumpKey = true;
  }

  void JumpMove()
  {
    agent.speed = moveSpeed * 2;
    agent.SetDestination(target.position);
  }

  void Attack()
  {
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

  void Move()
  {
    if (ChaseChara.Key)
    {
      agent.speed = moveSpeed * 2;
      bossMotion.RunMotion();
    }
    else
    {
      agent.speed = moveSpeed;
      bossMotion.WalkMotion();
    }
    agent.SetDestination(target.position);
  }
  void Stop()
  {
    agent.speed = 0;
  }


}
