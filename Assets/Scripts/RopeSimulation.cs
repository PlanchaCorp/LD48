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

  public void appendNode(float size) {
    GameObject newNode = GameObject.Instantiate(linkPrefab, parent);
    newNode.transform.localScale = new Vector3(1, newNode.transform.localScale.y, 1);
    if (links.Count == 0) {
      newNode.GetComponent<HingeJoint2D>().connectedBody = parent.gameObject.GetComponent<Rigidbody2D>();
    } else {
      Transform lastLink = links[links.Count - 1];
      Vector2 lastPosition = lastLink.Find("Extremity").transform.position;
      newNode.transform.position = lastPosition;
      newNode.GetComponent<HingeJoint2D>().connectedBody = lastLink.GetComponent<Rigidbody2D>();
    }
    links.Add(newNode.transform);
  }

  public void removeNode() {
    if (links.Count > 0) {
      GameObject.Destroy(links[links.Count - 1].gameObject);
      links.RemoveAt(links.Count - 1);
    }
  }

}
