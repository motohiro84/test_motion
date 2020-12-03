using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpChara : MonoBehaviour
{
  public GameObject Boss;
  BossMotion bossMotion;
  BossController bossController;
  bool Key;
  float coolTime;

  void Start()
  {
    Key = true;
    bossMotion = Boss.GetComponent<BossMotion>();
    bossController = Boss.GetComponent<BossController>();
  }

  void OnTriggerStay(Collider other)
  {
    if (!BossController.jumpKey && !AttackChara.Key && Key)
    {
      if (other.CompareTag("Player"))
      {
        Key = false;
        BossController.jumpKey = true;
        bossController.Jump();
        bossMotion.JumpMotion();
        coolTime = Random.Range(10f, 20f);
        Invoke("JumpCool", coolTime);
      }
    }
  }


  void JumpCool()
  {
    Key = true;
  }

}
