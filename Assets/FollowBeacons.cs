using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowBeacons : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform lookAt;
    private Transform follow;

  public void OnBeaconPlaced(GameObject beacon){
    GetComponent<CinemachineVirtualCamera>().Follow = beacon.transform;
    follow = GetComponent<CinemachineVirtualCamera>().Follow;
  }
  void Start(){
      lookAt = GetComponent<CinemachineVirtualCamera>().LookAt;
      follow = GetComponent<CinemachineVirtualCamera>().Follow;
  }
  void Update(){

    Debug.Log(lookAt.position);
    CinemachineFramingTransposer comp = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
    comp.m_TrackedObjectOffset = follow.position - lookAt.position;
  }
}
