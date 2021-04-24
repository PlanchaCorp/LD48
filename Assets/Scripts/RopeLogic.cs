using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLogic
{
  private RopeData ropeData;

  private RopeSimulation ropeSimulation;

  private int linkCount;
  private float lastLinkLength;
  private float ropeAction;

  public RopeLogic(RopeData ropeData, RopeSimulation ropeSimulation) {
    this.ropeData = ropeData;
    this.ropeSimulation = ropeSimulation;
    linkCount = 0;
    lastLinkLength = 0;
    ropeAction = 0;
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
      float newLinkLength = deltaLength <= ropeData.linkLength ? deltaLength : ropeData.linkLength;
      ropeSimulation.AppendNode(newLinkLength);
      lastLinkLength = newLinkLength;
    } else if (lastLinkLength + deltaLength > ropeData.linkLength) {
      ropeSimulation.resizeLastLink(ropeData.linkLength);
      if (linkCount < ropeData.maxLength / ropeData.linkLength) {
        linkCount++;
        float newNodeLength = (lastLinkLength + deltaLength) - ropeData.linkLength;
        ropeSimulation.AppendNode(newNodeLength > ropeData.linkLength ? ropeData.linkLength : newNodeLength);
        lastLinkLength = newNodeLength;
      } else {
        lastLinkLength = ropeData.linkLength;
      }
    } else {
      lastLinkLength += deltaLength;
      ropeSimulation.resizeLastLink(lastLinkLength);
    }
  }

  private void removeLength(float deltaLength) {
    if (lastLinkLength + deltaLength < 0) {
      if (linkCount > 0) {
        linkCount--;
        ropeSimulation.removeNode();
        float nextNodeLength = ropeData.linkLength + deltaLength - lastLinkLength;
        if (linkCount > 0) {
          lastLinkLength = nextNodeLength > 0 ? nextNodeLength : 1;
          ropeSimulation.resizeLastLink(lastLinkLength);
        } else
          lastLinkLength = 0;
      }
    } else {
      lastLinkLength += deltaLength;
      ropeSimulation.resizeLastLink(lastLinkLength);
    }
  }

  public void ChangeActionState(float actionState) {
    this.ropeAction = actionState;
  }

  public void ConnectRope(HingeJoint2D anchor) {
    ropeSimulation.AppendNode(ropeData.linkLength);
    ropeSimulation.AnchorLastNode(anchor);
    ropeSimulation.FreezeNodes();
  }

  public void Update(float time) {
    if (ropeAction != 0) {
      if (ropeAction > 0)
        changeLength(-1 * ropeData.retractSpeed * time);
      else
        changeLength(1 * ropeData.expandSpeed * time);
    }
  }
}
