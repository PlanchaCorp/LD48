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

    SoundManager soundManager;

    public Transform hook;
    void Awake(){
      Animator animator = GetComponent<Animator>();
      playerSimulation = new PlayerSimulation(transform, playerData, animator);
      groundCheck = transform.Find("GroundCheck");
      playerLogic = new PlayerLogic(playerData, playerSimulation, groundCheck);
      soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
      playerLogic.AttachSoundManager(soundManager);
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
    public void OnCollisionEnter2D(Collision2D collision){
      if(collision.gameObject.tag == "Trap"){
        Kill();
      }
    }
    public void Kill(){
        soundManager.Death();
        GameObject.Find("GameManager").GetComponent<gameManager>().OnclickReset();
    }
}
