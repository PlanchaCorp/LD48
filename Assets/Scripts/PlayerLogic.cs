class PlayerLogic{
  private PlayerData playerData;
  private PlayerSimulation playerSimulation;

  private float currentSpeed = 0;

  public PlayerLogic(PlayerData playerData, PlayerSimulation playerSimulation){
    this.playerData = playerData;
    this.playerSimulation = playerSimulation;
  }

  public void Move(float movement){
    currentSpeed =+ movement * playerData.moveSpeed;
  }
  public void FixedUpdate(float deltaTime){
      playerSimulation.MovePlayer(currentSpeed * deltaTime);
  }
}
