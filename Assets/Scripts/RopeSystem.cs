using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
public class RopeSystem : MonoBehaviour {
  //TODO MAKE PARAMETER
  public float ropeMaxCastDistance = 8f;

  public float climbSpeedBase;
  public GameObject ropeHingeAnchor;
  public DistanceJoint2D ropeJoint;
  public Player player;

  public LineRenderer ropeRenderer;
  public LayerMask ropeLayerMask;

  public List<Vector2> ropePositions = new List<Vector2>();

  private Vector2 playerPosition;
  private Rigidbody2D ropeHingeAnchorRb;
  private SpriteRenderer ropeHingeAnchorSprite;
  private bool distanceSet;
  private float climbSpeed;
  private Dictionary<Vector2, int> wrapPointsLookup = new Dictionary<Vector2, int>();

  public bool canClimb = true;

  private bool isColliding;


  void Awake() {
    // 2ropeJoint

    ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
    ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
  }
  void Start() {
    Vector2 point = GameObject.Find("Beacon").transform.position;
    hook(point);
  }
  private void hook(Vector2 point) {
    ropePositions.Add(point);
  }

  void Update() {
    playerPosition = transform.position;
    // 1

    if (ropePositions.Count > 0) {
      // 2
      var lastRopePoint = ropePositions.Last();
      Debug.DrawRay(playerPosition, (lastRopePoint - playerPosition).normalized, Color.blue);
      var playerToCurrentNextHit = Physics2D.Raycast(playerPosition, (lastRopePoint - playerPosition).normalized, Vector2.Distance(playerPosition, lastRopePoint) - 0.1f, ropeLayerMask);
      // 3
      if (playerToCurrentNextHit) {
        var colliderWithVertices = playerToCurrentNextHit.collider as PolygonCollider2D;
        if (colliderWithVertices != null) {
          var closestPointToHit = GetClosestColliderPointFromRaycastHit(playerToCurrentNextHit, colliderWithVertices);

          // 4
          if (wrapPointsLookup.ContainsKey(closestPointToHit)) {
            ResetRope();
            return;
          }

          // 5
          ropePositions.Add(closestPointToHit);
          //Ajouter la distance du secment
          wrapPointsLookup.Add(closestPointToHit, 0);
          distanceSet = false;
        }
      }


      HandleRopeUnwrap();
      UpdateRopePositions();
      if (canClimb) {
        if (!isColliding) {
          ropeJoint.distance += Time.deltaTime * climbSpeed;
        }
      }
    }
    if (ropeJoint.distance + SumUsedRopeDistance()  > ropeMaxCastDistance ) {
      ropeJoint.distance = ropeMaxCastDistance -SumUsedRopeDistance();
    }
  }
  private void HandleRopeUnwrap() {
    if (ropePositions.Count <= 1) {
      return;
    }
    // Hinge = next point up from the player position
    // Anchor = next point up from the Hinge
    // Hinge Angle = Angle between anchor and hinge
    // Player Angle = Angle between anchor and player

    // 1
    var anchorIndex = ropePositions.Count - 2;
    // 2
    var hingeIndex = ropePositions.Count - 1;
    // 3
    var anchorPosition = ropePositions[anchorIndex];
    // 4
    var hingePosition = ropePositions[hingeIndex];
    // 5
    var hingeDir = hingePosition - anchorPosition;
    // 6
    var hingeAngle = Vector2.Angle(anchorPosition, hingeDir);
    // 7
    var playerDir = playerPosition - anchorPosition;
    // 8
    var playerAngle = Vector2.Angle(anchorPosition, playerDir);

    if (playerAngle < hingeAngle) {
      // 1
      if (wrapPointsLookup[hingePosition] == 1) {
        UnwrapRopePosition(anchorIndex, hingeIndex);
        return;
      }

      // 2
      wrapPointsLookup[hingePosition] = -1;
    } else {
      // 3
      if (wrapPointsLookup[hingePosition] == -1) {
        UnwrapRopePosition(anchorIndex, hingeIndex);
        return;
      }

      // 4
      wrapPointsLookup[hingePosition] = 1;
    }

  }
  private float SumUsedRopeDistance(){
    float sum = 0;
    for(int i =0 ; i<= ropePositions.Count -2;i++){
      sum += Vector2.Distance(ropePositions[i],ropePositions[i+1]);
    }
    Debug.Log(sum);
    return sum;
  }

  private void UnwrapRopePosition(int anchorIndex, int hingeIndex) {
    // 1
    var newAnchorPosition = ropePositions[anchorIndex];
    wrapPointsLookup.Remove(ropePositions[hingeIndex]);
    ropePositions.RemoveAt(hingeIndex);
    //Supprimer la distance du segment

    // 2
    ropeHingeAnchorRb.transform.position = newAnchorPosition;
    ropeJoint.distance = Vector2.Distance(transform.position, newAnchorPosition);
    distanceSet = true;
  }

  private void UpdateRopePositions() {

    // 2
    ropeRenderer.positionCount = ropePositions.Count + 1;

    // 3
    for (var i = ropeRenderer.positionCount - 1; i >= 0; i--) {
      if (i != ropeRenderer.positionCount - 1) // if not the Last point of line renderer
      {
        ropeRenderer.SetPosition(i, ropePositions[i]);

        // 4
        if (i == ropePositions.Count - 1 || ropePositions.Count == 1) {
          var ropePosition = ropePositions[ropePositions.Count - 1];
          if (ropePositions.Count == 1) {
            ropeHingeAnchorRb.transform.position = ropePosition;
            if (!distanceSet) {
              ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
              distanceSet = true;
            }
          } else {
            ropeHingeAnchorRb.transform.position = ropePosition;
            if (!distanceSet) {
              ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
              distanceSet = true;
            }
          }
        }
        // 5
        else if (i - 1 == ropePositions.IndexOf(ropePositions.Last())) {
          var ropePosition = ropePositions.Last();
          ropeHingeAnchorRb.transform.position = ropePosition;
          if (!distanceSet) {
            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);
            distanceSet = true;
          }
        }
      } else {
        // 6
        ropeRenderer.SetPosition(i, transform.position);
      }
    }
  }
  public void OnRopeAdjust(InputAction.CallbackContext context) {
    if (context.performed || context.canceled) {
      float value = context.ReadValue<float>();
      climbSpeed = value * climbSpeedBase;
    }
  }
  public void OnRopeStop(InputAction.CallbackContext context) {

  }

  private Vector2 GetClosestColliderPointFromRaycastHit(RaycastHit2D hit, PolygonCollider2D polyCollider) {
    // 2
    var distanceDictionary = polyCollider.points.ToDictionary<Vector2, float, Vector2>(
        position => Vector2.Distance(hit.point, polyCollider.transform.TransformPoint(position)),
        position => polyCollider.transform.TransformPoint(position));

    // 3
    var orderedDictionary = distanceDictionary.OrderBy(e => e.Key);
    return orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero;
  }
  private void ResetRope() {
    ropeJoint.enabled = false;
    //playerMovement.isSwinging = false;
    ropeRenderer.positionCount = 2;
    ropeRenderer.SetPosition(0, transform.position);
    ropeRenderer.SetPosition(1, transform.position);
    ropePositions.Clear();
    ropeHingeAnchorSprite.enabled = false;
  }
  void OnCollisionEnter2D(Collision2D colliderStay) {
    isColliding = true;
  }

  private void OnCollisionExit2D(Collision2D colliderOnExit) {
    isColliding = false;
  }
}
