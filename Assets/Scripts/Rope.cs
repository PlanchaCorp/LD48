using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
  [SerializeField]
  private RopeData ropeData;
  [SerializeField]
  private GameObject linkPrefab;

  private RopeLogic ropeLogic;
  private RopeSimulation ropeSimulation;

  [SerializeField]
  private float deltaLength;

  private void Awake() {
    List<Transform> links = new List<Transform>();
    foreach (Transform child in transform)
      links.Add(child);
    ropeSimulation = new RopeSimulation(linkPrefab, links, transform);
    ropeLogic = new RopeLogic(ropeData, ropeSimulation);

  }

  private void Update() {
    ropeLogic.changeLength(deltaLength);
    deltaLength = 0;
  }

  public void changeLength(float amount) {

  }
}
