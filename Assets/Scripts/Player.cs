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

    public void OnMove(InputAction.CallbackContext context) {
        float movement = context.ReadValue<float>();
        if(context.performed){
        Debug.Log("perform" + movement);
        playerLogic.Move(movement);
        }
        if(context.canceled) {
          Debug.Log("cancel" + movement);
          playerLogic.Move(movement);
        }
    }
}
