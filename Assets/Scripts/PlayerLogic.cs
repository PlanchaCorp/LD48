using UnityEngine;
class PlayerLogic{
  private PlayerData playerData;
  private PlayerSimulation playerSimulation;

  private Vector2 vel;
  private Vector2 acc;
  private Vector2 pos;

  private Vector2 currentMovement;

  public PlayerLogic(PlayerData playerData, PlayerSimulation playerSimulation){
    this.playerData = playerData;
    this.playerSimulation = playerSimulation;
  }
  private void addForce(Vector2 force){
    this.acc += force;
  }

  public void Move(float movement){
    currentMovement = new Vector2(movement,0);
  }
  public void FixedUpdate(float deltaTime){
      this.addForce(currentMovement*playerData.accMultiplier);
      this.addForce(-this.vel* playerData.deceleration);
      this.pos += this.vel;
      this.acc = Vector2.ClampMagnitude(this.acc,playerData.maxAcc);
      this.vel += this.acc;
      this.vel = Vector2.ClampMagnitude(this.vel,playerData.maxSpeed);
      this.acc *= 0;
      playerSimulation.MovePlayer(vel);
  }
}
