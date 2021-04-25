using UnityEngine;
class PlayerLogic{
  private PlayerData playerData;
  private PlayerSimulation playerSimulation;
  private Vector2 currentMovement;
private Transform groundcheck;
  public PlayerLogic(PlayerData playerData, PlayerSimulation playerSimulation,Transform groundCheck){
    this.playerData = playerData;
    this.playerSimulation = playerSimulation;
    this.groundcheck = groundCheck;
  }

  public void Move(float movement){
    currentMovement = new Vector2(movement,0);
  }
  public void FixedUpdate(float deltaTime){
      playerSimulation.MovePlayer(currentMovement * deltaTime);
      if(isGrounded()) {
        playerSimulation.groundFriction();
      } else {
        playerSimulation.aerialFriction();
      }
       playerSimulation.StepMovementSimuation();
  }

  public bool isGrounded(){
    Collider2D[] groundColliders = Physics2D.OverlapCircleAll(groundcheck.position, playerData.feetRadius, playerData.whatIsGround);
     return groundColliders.Length > 0;
  }
  public void Jump(){
    if(isGrounded()){
      float force = this.playerData.jumpForce;
      playerSimulation.Jump(force);
    }
  }
}
