using UnityEngine;
using UnityEngine.Events;

class BeaconPlacedListener : MonoBehaviour
{
  public BeaconPlacedEvent gameEvent;
  public UnityEvent response;

  private void OnEnable()
  { gameEvent.RegisterListener(this); }

  private void OnDisable()
  { gameEvent.UnregisterListener(this); }

  public void OnEventRaised()
  { response.Invoke(); }
}
