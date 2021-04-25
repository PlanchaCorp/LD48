using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 2)]
class PlayerData : ScriptableObject{
  public float maxSpeed;
  public float maxAcc;

  public float accMultiplier;
  public float groundedHorizontalFriction;
  public float aerialHorizontalFriction;


  public float feetRadius;
  public LayerMask whatIsGround;

  public float jumpForce;
}
