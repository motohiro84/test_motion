using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMotion : MonoBehaviour
{

  private Animator animator;
  public static string state;
  string prevState;

  AnimatorStateInfo animStateInfo;

  void Start()
  {
    animator = this.gameObject.GetComponent<Animator>();
    state = "Walk";
  }

  void Update()
  {

  }

  void Animation()
  {
    if (prevState != state)
    {
      animator.CrossFadeInFixedTime(state, 0);
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
  public void AttackMotion()
  {
    state = "Attack" + BossController.attackNum;
    Animation();
    prevState = state;
  }


}
