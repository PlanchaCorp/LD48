using UnityEngine;

class PlayerSimulation {
  private Vector2 acc;
  private Vector2 pos;
  private Transform player;

private void addForce(Vector2 force){
    this.acc += force;
  }

  public PlayerSimulation(Transform player){
    this.player = player;
  }
  public void MovePlayer(Vector2 movement, PlayerData playerData) {
    Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
    Vector2 vel = rb.velocity;
    this.addForce(movement * playerData.accMultiplier);
    this.addForce(new Vector2(-vel.x,0) * playerData.deceleration);
    this.pos += vel;
    this.acc = Vector2.ClampMagnitude(this.acc, playerData.maxAcc);
    vel += this.acc;
    //this.vel = Vector2.ClampMagnitude(this.vel, playerData.maxSpeed);
    rb.velocity = vel;
    resetAcc();
  }

  public void Jump(float force){
    Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
    rb.AddForce(new Vector2(0,force));
  }

  private void resetAcc() {
    this.acc *= 0;
  }
}
