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
  private SoundManager soundManager;

  public bool canPlace = true;

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

  private void Start() {
    soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
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
      soundManager.ConnectEnd();
      beaconPlacedEvent.Raise(endBeacon.gameObject);
      return endBeacon.gameObject;
    }
    foreach (BeaconReach reach in beaconReachs) {
      if (reach != null && reach.isPlayerInReach())
        return null;
    }
    if(!canPlace || position.y < endBeacon.transform.position.y)
    {
      soundManager.Fall();
      return null;
    }
    GameObject beacon = Instantiate(beaconPrefab, position, transform.rotation, transform);
    soundManager.PlaceBeacon();
    beaconPlacedEvent.Raise(beacon);

    return beacon;
  }
}
