using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeaconGenerator : MonoBehaviour
{
  [SerializeField]
  private GameObject beaconPrefab;

  private Transform _player;
  private Transform player {
    get {
      if (_player == null)
        _player = GameObject.FindGameObjectWithTag("Player").transform;
      return _player;
    }
  }

  public void GenerateBeacon(InputAction.CallbackContext context) {
    if (context.performed)
      PlaceBeacon(player.transform.position);
  }

  private void PlaceBeacon(Vector2 position) {
    Instantiate(beaconPrefab, position, transform.rotation, transform);
  }
}
