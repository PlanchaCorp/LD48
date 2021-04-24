using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSimulation
{
  private GameObject linkPrefab;
  private Transform parent;
  private List<Transform> links;

  public RopeSimulation (GameObject linkPrefab, List<Transform> children, Transform parent) {
    this.linkPrefab = linkPrefab;
    this.parent = parent;
    links = new List<Transform>(children);
  }

  public void resizeLastLink(float size) {
    if (links.Count == 0)
      return;
    Transform lastLink = links[links.Count - 1];
    lastLink.localScale = new Vector3(size, lastLink.localScale.y, lastLink.localScale.z);
  }

  public Rigidbody2D AppendNode(float size, Rigidbody2D anchor) {
    GameObject newNode = GameObject.Instantiate(linkPrefab, parent);
    newNode.transform.localScale = new Vector3(size, newNode.transform.localScale.y, 1);
    DistanceJoint2D joint = newNode.GetComponent<DistanceJoint2D>();
    joint.distance = size;
    if (links.Count == 0) {
      joint.connectedBody = anchor;
      joint.autoConfigureDistance = false;
      joint.distance = 0;
    } else {
      Transform lastLink = links[links.Count - 1];
      Vector2 lastPosition = lastLink.Find("Extremity").transform.position;
      newNode.transform.position = lastPosition;
      joint.connectedBody = lastLink.GetComponent<Rigidbody2D>();
    }
    links.Add(newNode.transform);
    return newNode.GetComponent<Rigidbody2D>();
  }

  public void removeNode() {
    if (links.Count > 0) {
      GameObject.Destroy(links[links.Count - 1].gameObject);
      links.RemoveAt(links.Count - 1);
    }
  }

  public void AnchorLastNode(DistanceJoint2D anchor) {
    if (links.Count > 0) {
      Rigidbody2D lastLink = links[links.Count - 1].GetComponent<Rigidbody2D>();
      anchor.connectedBody = lastLink;
    }
  }

  public void FreezeNodes(Transform newParent) {
    foreach (Transform node in links) {
      Rigidbody2D rb = node.GetComponent<Rigidbody2D>();
      rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
      node.parent = newParent;
      rb.freezeRotation = true;
    }
    links = new List<Transform>();
  }

}
