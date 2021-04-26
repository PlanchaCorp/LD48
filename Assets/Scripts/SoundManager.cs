using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource inGameMusic;

    private AudioSource jump;
    private AudioSource fall;
    private AudioSource steps;
    private AudioSource beaconPlacement;
    private AudioSource connectEnd;

    void Start()
    {
        inGameMusic = gameObject.GetComponent<AudioSource>();
        jump = transform.Find("Jump").GetComponent<AudioSource>();
        fall = transform.Find("Fall").GetComponent<AudioSource>();
        steps = transform.Find("Steps").GetComponent<AudioSource>();
        beaconPlacement = transform.Find("BeaconPlacement").GetComponent<AudioSource>();
        connectEnd = transform.Find("ConnectEnd").GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    public void Jump() {
        jump.Play();
    }
    public void Fall() {
        fall.Play();
    }
    public void Steps() {
        steps.Play();
    }
    public void PlaceBeacon() {
        beaconPlacement.Play();
    }
    public void ConnectEnd() {
      connectEnd.Play();
    }
}
