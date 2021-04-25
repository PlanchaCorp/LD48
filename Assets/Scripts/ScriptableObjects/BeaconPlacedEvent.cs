using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
class BeaconPlacedEvent : ScriptableObject
{

  private List<BeaconPlacedListener> listeners = new List<BeaconPlacedListener>();

  public void Raise() {
    for(int i = listeners.Count -1; i >= 0; i--)
      listeners[i].OnEventRaised();
  }

  public void RegisterListener(BeaconPlacedListener listener)
  {
    listeners.Add(listener);
  }

  public void UnregisterListener(BeaconPlacedListener listener)
  {
    listeners.Remove(listener);
  }
}
