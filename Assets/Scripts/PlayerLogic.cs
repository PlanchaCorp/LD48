using UnityEngine;
class PlayerLogic{
  private PlayerData playerData;
  private PlayerSimulation playerSimulation;

  private Vector2 currentMovement;

  public PlayerLogic(PlayerData playerData, PlayerSimulation playerSimulation){
    this.playerData = playerData;
    this.playerSimulation = playerSimulation;
  }

  public void Move(float movement){
    currentMovement = new Vector2(movement,0);
  }
  public void FixedUpdate(float deltaTime){
      playerSimulation.MovePlayer(currentMovement * deltaTime, this.playerData);
  }
}
