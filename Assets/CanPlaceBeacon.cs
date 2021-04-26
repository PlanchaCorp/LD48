using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanPlaceBeacon : MonoBehaviour
{
    // Start is called before the first frame update
    BeaconGenerator beaconGenerator;
    void Start()
    {
        beaconGenerator = GameObject.Find("BeaconContainer").GetComponent<BeaconGenerator>();
    }

    void OnTriggerEnter2D(Collider2D colision){
      beaconGenerator.canPlace = true;
    }
    void OnTriggerExit2D(Collider2D colision){
      beaconGenerator.canPlace = false;
    }
}
