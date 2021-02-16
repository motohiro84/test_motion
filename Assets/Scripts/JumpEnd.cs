using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnd : MonoBehaviour
{
  public GameObject Boss;
  public GameObject Player;
  Rigidbody rigid;
  BossMotion bossMotion;
  public float forcePower;


  void Start()
  {
    rigid = Player.GetComponent<Rigidbody>();
    bossMotion = Boss.GetComponent<BossMotion>();
  }
  void OnTriggerEnter(Collider other)
  {
    if (BossController.jumpKey)
    {
      if (other.CompareTag("Ground"))
      {
        bossMotion.JumpEndMotion();
      }

      if (other.CompareTag("Player"))
      {
        Vector3 toVec = GetAngleVec(Boss, Player);

        rigid.AddForce(toVec * forcePower, ForceMode.Impulse);
      }
    }
  }

  Vector3 GetAngleVec(GameObject _from, GameObject _to)
  {
    Vector3 fromVec = new Vector3(_from.transform.position.x, 0, _from.transform.position.z);
    Vector3 toVec = new Vector3(_to.transform.position.x, 0.03f, _to.transform.position.z);
    return Vector3.Normalize(toVec - fromVec);
  }
}