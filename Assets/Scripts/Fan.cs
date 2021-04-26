using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
  [SerializeField]
  private float ROTATION_SPEED;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, ROTATION_SPEED * Time.deltaTime));
    }
}
