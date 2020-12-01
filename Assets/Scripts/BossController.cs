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
    animator = GetComponent<Animator>();
    rigidBody = GetComponent<Rigidbody>();
    agent = GetComponent<NavMeshAgent>();
    bossMotion = this.gameObject.GetComponent<BossMotion>();
    target = GameObject.FindGameObjectWithTag(targetTag).transform;
    InitCharacter();
  }

  void Update()
  {
    if (moveEnabled)
    {
      Move();
    }
    else
    {
      Stop();
    }


    if (AttackChara.Key && !attacking)
    {
      attackNum = Random.Range(1, 4).ToString();
      Attack();
    }

    animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
    if (animStateInfo.fullPathHash == Animator.StringToHash("Base Layer.Attack" + attackNum))
    {
      if (animStateInfo.normalizedTime > 1.0f)
      {
        attacking = false;
        moveEnabled = true;
        AttackChara.Key = false;
        Debug.Log("1");
      }
    }
  }


  void InitCharacter()
  {
    Hp = maxHp;
    moveSpeed = agent.speed;
  }

  void Attack()
  {
    attacking = true;
    moveEnabled = false;
    bossMotion.AttackMotion();
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
    rigidBody.velocity = agent.desiredVelocity;
  }
  void Stop()
  {
    agent.speed = 0;
  }


}
