using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowBeacons : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    private Transform beacon;
    private bool first = true;

  public void OnBeaconPlaced(GameObject beacon){
    GetComponent<CinemachineVirtualCamera>().Follow = beacon.transform;
    this.beacon = GetComponent<CinemachineVirtualCamera>().Follow;
    first = false;
  }

  public void OnCut(){
    player = null;
  }

  void Start(){
      player = GetComponent<CinemachineVirtualCamera>().LookAt;
      beacon = GetComponent<CinemachineVirtualCamera>().Follow;

  }
  void Update(){
   if(player != null){
    CinemachineFramingTransposer comp = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
    if(first){
    comp.m_TrackedObjectOffset =   beacon.position - player.position;
    } else {
    comp.m_TrackedObjectOffset =  player.position - beacon.position;
    }
   }
  }
}
