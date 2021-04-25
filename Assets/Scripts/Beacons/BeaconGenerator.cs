using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BeaconGenerator : MonoBehaviour
{
  [SerializeField]
  private GameObject beaconPrefab;
  [SerializeField]
  private BeaconPlacedEvent beaconPlacedEvent;

  private List<BeaconReach> beaconReachs;

  private Transform _player;
  private Transform player {
    get {
      if (_player == null)
        _player = GameObject.FindGameObjectWithTag("Player").transform;
      return _player;
    }
  }

  private BeaconEnd _endBeacon;
  private BeaconEnd endBeacon {
    get {
      if (_endBeacon == null)
        _endBeacon = GetComponentInChildren<BeaconEnd>();
      return _endBeacon;
    }
  }

  public BeaconGenerator() {
    beaconReachs = new List<BeaconReach>();
  }

  public void GenerateBeacon(InputAction.CallbackContext context) {
    if (context.performed) {
      GameObject newBeacon = PlaceBeacon(player.transform.position);
      if (newBeacon != null)
        beaconReachs.Add(newBeacon.GetComponentInChildren<BeaconReach>());
    }
  }

  private GameObject PlaceBeacon(Vector2 position) {
    if (endBeacon != null && endBeacon.isPlayerInReach()) {
      Debug.Log("End");
      return null;
    }
    foreach (BeaconReach reach in beaconReachs) {
      if (reach != null && reach.isPlayerInReach())
        return null;
    }
    beaconPlacedEvent.Raise();
    return Instantiate(beaconPrefab, position, transform.rotation, transform);
  }
}
