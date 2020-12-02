using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnd : MonoBehaviour
{
  public GameObject Boss;
  BossMotion bossMotion;

  void Start()
  {
    bossMotion = Boss.GetComponent<BossMotion>();
  }
  void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Ground"))
    {
      bossMotion.JumpEndMotion();
    }
  }
}
