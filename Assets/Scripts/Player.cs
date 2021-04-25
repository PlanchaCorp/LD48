using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
     private PlayerData playerData;
     [SerializeField]
     private DistanceJoint2D grapplingHook;

    PlayerSimulation playerSimulation;
    PlayerLogic playerLogic;

    Transform groundCheck;
    void Awake(){
      playerSimulation = new PlayerSimulation(transform,playerData);
      groundCheck = transform.Find("GroundCheck");
      playerLogic = new PlayerLogic(playerData,playerSimulation,groundCheck);
    }

    void FixedUpdate(){
      playerLogic.FixedUpdate(Time.fixedDeltaTime);
      if(playerLogic.isGrounded()){
        GetComponent<SpriteRenderer>().color = Color.red;
      } else {
         GetComponent<SpriteRenderer>().color = Color.blue;
      }
    }

    public void OnMove(InputAction.CallbackContext context) {
        float movement = context.ReadValue<float>();
        if(context.performed || context.canceled){
          playerLogic.Move(movement);
        }
    }
    public void OnJump(InputAction.CallbackContext context) {
      if(context.performed){
          playerLogic.Jump();
        }
    }

    public void Hook(Rigidbody2D target) {
      playerSimulation.Hook(GetComponent<DistanceJoint2D>(), target);
    }
}
