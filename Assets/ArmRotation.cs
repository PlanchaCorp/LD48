using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmRotation : MonoBehaviour
{
  public  Transform anchor;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        var angle = Vector2.Angle(anchor.position,transform.position);
        Debug.Log(angle);
        transform.eulerAngles = new Vector3(0,0, angle);
    }
}
