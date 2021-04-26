using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;
public class RopeSystem : MonoBehaviour {
  //TODO MAKE PARAMETER

  public float climbSpeedBase;
  public GameObject ropeHingeAnchor;
  public DistanceJoint2D ropeJoint;
  public Player player;



  private float climbSpeed;
  private Dictionary<Vector2, int> wrapPointsLookup = new Dictionary<Vector2, int>();

  public bool canClimb = true;

  private bool isColliding;



  [SerializeField]
  private float ROPE_MAX_DISTANCE;
  [SerializeField]
  private Transform ropeOrigin;
  [SerializeField]
  private LayerMask collisionLayers;

  private Rigidbody2D ropeHingeAnchorRb;
  private SpriteRenderer ropeHingeAnchorSprite;
  [SerializeField] private List<Vector2> ropePositions;
  [SerializeField] private LineRenderer ropeRenderer;
  private bool distanceSet; // Last rope segment length is not 0


  void Start() {
    ropeHingeAnchorRb = ropeHingeAnchor.GetComponent<Rigidbody2D>();
    ropeHingeAnchorSprite = ropeHingeAnchor.GetComponent<SpriteRenderer>();
    ropePositions = new List<Vector2>();
    hook(ropeOrigin.position);
  }

  private void hook(Vector2 point) {
    ropePositions.Add(point);
  }

/// Evaluate last rope segment possible collision
  private void HandleRopeContact() {
    Vector2 playerPosition = transform.position;
    Vector2 lastRopePoint = ropePositions.Last();
    float raycastLength = Vector2.Distance(playerPosition, lastRopePoint) - 0.1f;
    Debug.DrawRay(playerPosition, (lastRopePoint - playerPosition).normalized * raycastLength, Color.blue);
    RaycastHit2D playerToCurrentNextHit = Physics2D.Raycast(playerPosition, (lastRopePoint - playerPosition).normalized, raycastLength, collisionLayers);
    if (playerToCurrentNextHit) {
      var colliderWithVertices = playerToCurrentNextHit.collider as PolygonCollider2D;
        if (colliderWithVertices != null)
        {
            var closestPointToHit = GetClosestColliderPointFromRaycastHit(playerToCurrentNextHit, colliderWithVertices);

            // 4
            if (wrapPointsLookup.ContainsKey(closestPointToHit))
            {
                ResetRope();
                return;
            }

            // 5
            ropePositions.Add(closestPointToHit);
            wrapPointsLookup.Add(closestPointToHit, 0);
            distanceSet = false;
        }
    }
  }

  private Vector2 GetClosestColliderPointFromRaycastHit(RaycastHit2D hit, PolygonCollider2D polyCollider)
{
    // 2
    var distanceDictionary = polyCollider.points.ToDictionary<Vector2, float, Vector2>(
        position => Vector2.Distance(hit.point, polyCollider.transform.TransformPoint(position)),
        position => polyCollider.transform.TransformPoint(position));

    // 3
    var orderedDictionary = distanceDictionary.OrderBy(e => e.Key);
    return orderedDictionary.Any() ? orderedDictionary.First().Value : Vector2.zero;
}

/// Reset the rope as a zero length segment
  private void ResetRope() {
    // ropeJoint.enabled = false;
    //playerMovement.isSwinging = false;
    ropeRenderer.positionCount = 2;
    ropeRenderer.SetPosition(0, ropeOrigin.position);
    ropeRenderer.SetPosition(1, ropeOrigin.position);
    ropeJoint.distance = 0;
    ropePositions.Clear();
    wrapPointsLookup.Clear();
    ropeHingeAnchorSprite.enabled = false;
  }

  void Update() {

    if (ropePositions.Count > 0) {
      HandleRopeContact();


      HandleRopeUnwrap();
      UpdateRopePositions();
      if (canClimb) {
          ropeJoint.distance += Time.deltaTime * climbSpeed;
      }
    }
    if (ropeJoint.distance + SumUsedRopeDistance()  > ROPE_MAX_DISTANCE ) {
      ropeJoint.distance = ROPE_MAX_DISTANCE - SumUsedRopeDistance();
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
    var playerDir = new Vector2(transform.position.x, transform.position.y) - anchorPosition;
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

/// Calculate total rope segment length used (since last anchor)
  private float SumUsedRopeDistance(){
    float sum = 0;
    for(int i =0 ; i<= ropePositions.Count -2;i++){
      sum += Vector2.Distance(ropePositions[i],ropePositions[i+1]);
    }
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

  void OnCollisionEnter2D(Collision2D colliderStay) {
    isColliding = true;
  }

  private void OnCollisionExit2D(Collision2D colliderOnExit) {
    isColliding = false;
  }
  public void OnBeaconPlaced(GameObject beacon){
    LineRenderer attachedRope = Instantiate(ropeRenderer, beacon.transform);
    Transform attachedPoint = beacon.transform.Find("AttachedPoint");
    attachedRope.SetPosition(ropeRenderer.positionCount - 1, attachedPoint != null ? attachedPoint.position : beacon.transform.position);
    ropeOrigin = attachedPoint != null ? attachedPoint : beacon.transform;
    ResetRope();
    distanceSet = true;
    hook(ropeOrigin.position);
  }
}
