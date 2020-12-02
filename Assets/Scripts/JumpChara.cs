using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpChara : MonoBehaviour
{
  public GameObject Boss;
  BossMotion bossMotion;
  BossController bossController;

  void Start()
  {
    bossMotion = Boss.GetComponent<BossMotion>();
    bossController = Boss.GetComponent<BossController>();
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Player")
    {
      bossController.Jump();
      bossMotion.JumpMotion();
    }
  }

}
