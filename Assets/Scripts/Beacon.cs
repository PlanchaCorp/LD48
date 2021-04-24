using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;
public class Beacon : MonoBehaviour
{
  [SerializeField]
  private Light2D pointLight;
  [SerializeField]
  private HingeJoint2D anchor;

  private bool lighted;
  private bool reachable;

  private void Start() {
    lighted = false;
    reachable = false;
  }

  public void Connect() {
    lighted = true;
    if (pointLight != null)
      pointLight.enabled = true;
    Transform rope = GameObject.FindGameObjectWithTag("Rope").transform;
    rope.parent = transform;
    rope.GetComponent<Rope>().Anchor(anchor);
  }

  public void OnConnectRope(InputAction.CallbackContext context) {
    if (context.performed && reachable && !lighted)
      Connect();
  }

  private void OnTriggerEnter2D(Collider2D collider) {
    if (collider.CompareTag("Player"))
      reachable = true;
  }
  private void OnTriggerExit2D(Collider2D collider) {
    if (collider.CompareTag("Player"))
      reachable = false;
  }
}
