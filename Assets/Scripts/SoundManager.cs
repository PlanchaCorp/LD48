using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource inGameMusic;

    private AudioSource jump;
    private AudioSource death;
    private AudioSource fall;
    private AudioSource steps;
    private AudioSource beaconPlacement;
    private AudioSource connectEnd;

    private static SoundManager instance = null;
    private static SoundManager Instance
    {
      get { return instance;}
    }

    void Awake(){
      if(instance != null &&  instance != this){
        Destroy(gameObject);
      } else {
        instance = this;
      }
      DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
      AudioListener.volume = 0.15f;
        inGameMusic = gameObject.GetComponent<AudioSource>();
        jump = transform.Find("Jump").GetComponent<AudioSource>();
        death = transform.Find("Death").GetComponent<AudioSource>();
        fall = transform.Find("Fall").GetComponent<AudioSource>();
        steps = transform.Find("Steps").GetComponent<AudioSource>();
        beaconPlacement = transform.Find("BeaconPlacement").GetComponent<AudioSource>();
        connectEnd = transform.Find("ConnectEnd").GetComponent<AudioSource>();

    }

    public void Jump() {
        jump.Play();
    }
        public void Death() {
        death.Play();
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
