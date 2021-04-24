using UnityEngine;

class PlayerSimulation {
  private Transform player;

  public PlayerSimulation(Transform player){
    this.player = player;
  }
  public void MovePlayer(Vector2 movement){
    Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
    Vector3 velocity = rb.velocity;
    velocity = movement;
    rb.velocity= velocity;
  }
}
