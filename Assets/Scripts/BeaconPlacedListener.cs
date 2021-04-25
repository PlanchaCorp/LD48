using UnityEngine;
using UnityEngine.Events;

class BeaconPlacedListener : MonoBehaviour
{
  public BeaconPlacedEvent gameEvent;
  private RopeSystem _ropeSystem;
  private RopeSystem ropeSystem {
    get {
      if (_ropeSystem == null)
        _ropeSystem = GetComponent<RopeSystem>();
      return _ropeSystem;
    }
  }

  private void OnEnable()
  { gameEvent.RegisterListener(this); }

  private void OnDisable()
  { gameEvent.UnregisterListener(this); }

  public void OnEventRaised(GameObject beacon)
  {
    ropeSystem.OnBeaconPlaced(beacon);
  }
}
