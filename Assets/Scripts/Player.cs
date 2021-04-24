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
    void Awake(){
      playerSimulation = new PlayerSimulation(transform);
      playerLogic = new PlayerLogic(playerData,playerSimulation);
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
}
