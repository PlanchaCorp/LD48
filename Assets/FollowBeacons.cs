using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBeacons : MonoBehaviour
{
    // Start is called before the first frame update
  public void OnBeaconPlaced(GameObject beacon){
    GetComponent<Cinemachine.CinemachineVirtualCamera>().Follow = beacon.transform;
  }
}
