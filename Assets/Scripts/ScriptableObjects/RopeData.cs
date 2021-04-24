using UnityEngine;

[CreateAssetMenu(fileName = "RopeData", menuName = "ScriptableObjects/RopeData", order = 1)]
public class RopeData : ScriptableObject
{
  public float maxLength;
  public float linkLength;
}
