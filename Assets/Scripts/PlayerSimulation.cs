using UnityEngine;

class PlayerSimulation {
  private Transform player;

  public PlayerSimulation(Transform player){
    this.player = player;
  }
  public void MovePlayer(float movement){
    Rigidbody2D rb = player.gameObject.GetComponent<Rigidbody2D>();
    Vector3 pos = player.position;
    pos.x += movement;
    rb.MovePosition(pos);
  }
}
