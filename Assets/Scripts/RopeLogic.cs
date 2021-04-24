using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLogic
{
  private RopeData ropeData;

  private RopeSimulation ropeSimulation;

  private int linkCount;
  private float lastLinkLength;

  public RopeLogic(RopeData ropeData, RopeSimulation ropeSimulation) {
    this.ropeData = ropeData;
    this.ropeSimulation = ropeSimulation;
    linkCount = 0;
    lastLinkLength = 0;
  }

  public void changeLength(float deltaLength) {
    if (deltaLength > 0)
      this.addLength(deltaLength);
    else if (deltaLength < 0)
      this.removeLength(deltaLength);
  }

  private void addLength(float deltaLength) {
    if (linkCount == 0) {
      linkCount++;
      ropeSimulation.appendNode(deltaLength);
      lastLinkLength = deltaLength;
    } else if (lastLinkLength + deltaLength > ropeData.linkLength) {
      ropeSimulation.resizeLastLink(lastLinkLength);
      if (linkCount < ropeData.maxLength / ropeData.linkLength) {
        linkCount++;
        float newNodeLength = (lastLinkLength + deltaLength) - ropeData.linkLength;
        ropeSimulation.appendNode(newNodeLength > ropeData.linkLength ? ropeData.linkLength : newNodeLength);
        lastLinkLength = newNodeLength;
      } else {
        lastLinkLength = ropeData.linkLength;
      }
    } else {
      lastLinkLength += deltaLength;
      ropeSimulation.resizeLastLink(lastLinkLength);
    }
    Debug.Log(lastLinkLength);
    Debug.Log(linkCount);
  }

  private void removeLength(float deltaLength) {
    if (lastLinkLength + deltaLength < 0) {
      if (linkCount > 0) {
        linkCount--;
        ropeSimulation.removeNode();
        float nextNodeLength = ropeData.linkLength + deltaLength - lastLinkLength;
        if (linkCount > 0) {
          lastLinkLength = nextNodeLength > 0 ? nextNodeLength : 0;
          ropeSimulation.resizeLastLink(lastLinkLength);
        } else
          lastLinkLength = 0;
      }
    } else {
      lastLinkLength += deltaLength;
      ropeSimulation.resizeLastLink(lastLinkLength);
    }
    Debug.Log("Blog");
    Debug.Log(lastLinkLength);
    Debug.Log(linkCount);
  }
}
