using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rope : MonoBehaviour
{
  [SerializeField]
  private RopeData ropeData;
  [SerializeField]
  private GameObject linkPrefab;

  private RopeLogic ropeLogic;
  private RopeSimulation ropeSimulation;

  private void Awake() {
    List<Transform> links = new List<Transform>();
    foreach (Transform child in transform)
      links.Add(child);
    ropeSimulation = new RopeSimulation(linkPrefab, links, transform);
    ropeLogic = new RopeLogic(ropeData, ropeSimulation);
  }

  private void Update() {
    ropeLogic.Update(Time.deltaTime);
  }

  public void OnChangeLength(InputAction.CallbackContext context) {
    float change = context.ReadValue<float>();
    if (context.canceled || context.performed)
      ropeLogic.ChangeActionState(change);
  }

  public void Anchor(HingeJoint2D anchor) {
    ropeLogic.ConnectRope(anchor);
  }
}
