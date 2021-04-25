using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconReach : MonoBehaviour
{
  private bool playerInReach;

  private void Start() {
    playerInReach = false;
  }

  private void OnTriggerEnter2D(Collider2D collider) {
    if (collider.CompareTag("Player"))
      playerInReach = true;
  }
  private void OnTriggerExit2D(Collider2D collider) {
    if (collider.CompareTag("Player"))
      playerInReach = false;
  }

  public bool isPlayerInReach() {
    return playerInReach;
  }
}
