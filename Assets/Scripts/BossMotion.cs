using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMotion : MonoBehaviour
{

  private Animator animator;
  public static string state;
  string prevState;
  public GameObject Boss;
  BossController bossController;
  AnimatorStateInfo animStateInfo;
  SphereCollider SphereCollider;

  void Start()
  {
    bossController = Boss.GetComponent<BossController>();
    animator = this.gameObject.GetComponent<Animator>();
    state = "Walk";
    SphereCollider = bossController.AttackArea.GetComponent<SphereCollider>();
  }

  void Update()
  {
    animStateInfo = animator.GetCurrentAnimatorStateInfo(0);
    if (animStateInfo.fullPathHash == Animator.StringToHash("Base Layer.Attack" + BossController.attackNum))
    {
      if (animStateInfo.normalizedTime >= 1.0f)
      {
        bossController.AttackTime();
      }
      Debug.Log("Base Layer.Attack" + BossController.attackNum);
    }
    if (animStateInfo.fullPathHash == Animator.StringToHash("Base Layer.JumpEnd"))
    {
      bossController.moveEnabled = false;
      if (animStateInfo.normalizedTime >= 1.0f)
      {
        BossController.jumpKey = false;
        bossController.moveEnabled = true;
      }
    }
  }

  void Animation()
  {
    if (prevState != state)
    {
      animator.CrossFade(state, 0.2f);
    }
  }
  public void WalkMotion()
  {
    state = "Walk";
    Animation();
    prevState = state;
  }
  public void RunMotion()
  {
    state = "Run";
    Animation();
    prevState = state;
  }
  public void JumpMotion()
  {
    state = "Jump";
    Animation();
    prevState = state;
  }
  public void JumpEndMotion()
  {
    state = "JumpEnd";
    Animation();
    prevState = state;
  }
  public void AttackMotion()
  {
    state = "Attack" + BossController.attackNum;
    Animation();
    prevState = state;
  }


}
