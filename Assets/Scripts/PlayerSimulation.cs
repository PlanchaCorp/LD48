using UnityEngine;

class PlayerSimulation {
  private Vector2 acc;
  private Vector2 pos;
  private Rigidbody2D rb;
  private Transform player;
  PlayerData playerData;

private void addForce(Vector2 force){
    this.acc += force;
  }

  public PlayerSimulation(Transform player,PlayerData playerData){
    this.player = player;
    this.playerData = playerData;
    this.rb = player.gameObject.GetComponent<Rigidbody2D>();
  }

  public void StepMovementSimuation(){
    Vector2 vel = rb.velocity;
    this.pos += vel;
    this.acc = Vector2.ClampMagnitude(this.acc, playerData.maxAcc);
    vel += this.acc;
    //this.vel = Vector2.ClampMagnitude(this.vel, playerData.maxSpeed);
    rb.velocity = vel;
    resetAcc();
  }
  public void groundFriction(){
    this.addForce(new Vector2(-rb.velocity.x,0) * playerData.groundedHorizontalFriction);
  }
    public void aerialFriction(){
    this.addForce(new Vector2(-rb.velocity.x,0) * playerData.aerialHorizontalFriction);
  }
  public void MovePlayer(Vector2 movement) {
    this.addForce(movement * playerData.accMultiplier);
  }

  public void Jump(float force){
    Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
    rb.AddForce(new Vector2(0,force));
  }

  private void resetAcc() {
    this.acc *= 0;
  }

  public void Hook(DistanceJoint2D subject, Rigidbody2D target) {
    subject.connectedBody = target;
  }
}
