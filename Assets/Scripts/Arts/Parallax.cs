using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
  private float startpos;
  public float parallaxEffect;
  private Transform cam;

    // Start is called before the first frame update
    void Start()
    {
      cam = Camera.main.transform;
    startpos = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    float dist = (cam.position.x * parallaxEffect);
    transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}
