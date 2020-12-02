using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseChara : MonoBehaviour
{

  public static bool Key;
  void Start()
  {
    Key = false;
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Key = true;
    }
  }

  void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Key = false;
    }
  }


}
