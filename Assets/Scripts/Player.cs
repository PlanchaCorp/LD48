using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
     private PlayerData playerData;
    // Start is called before the first frame update

    PlayerSimulation playerSimulation;
    PlayerLogic playerLogic;

    Transform groundCheck;
    void Awake(){
      playerSimulation = new PlayerSimulation(transform);
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
}
