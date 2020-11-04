using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
  public void OnTriggerEnter(Collider other) {
      print("Hit "+ other.name + "!");
      Destroy(gameObject);
  }
}
