using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
  private float startpos;
  Camera m_MainCamera;
  public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
    m_MainCamera = Camera.main;
    m_MainCamera.enabled = true;
    startpos = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    float dist = (m_MainCamera.transform.position.x * parallaxEffect);
    transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}

