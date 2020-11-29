using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
  public void OnTriggerEnter(Collider other) {
    if (other.tag != "Player") {
      print("Hit "+ other.name + "!");
      Destroy(gameObject);
    }
  }
}
