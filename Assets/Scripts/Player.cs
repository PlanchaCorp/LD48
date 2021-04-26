using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    PlayerSimulation playerSimulation;
    PlayerLogic playerLogic;

    Transform groundCheck;
    void Awake(){
      Animator animator = GetComponent<Animator>();
      playerSimulation = new PlayerSimulation(transform, playerData, animator);
      groundCheck = transform.Find("GroundCheck");
      playerLogic = new PlayerLogic(playerData,playerSimulation,groundCheck);
    }

    void FixedUpdate(){
      playerLogic.FixedUpdate(Time.fixedDeltaTime);
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
