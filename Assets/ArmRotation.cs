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
        var angle = AngleBetweenVector2(transform.position,anchor.position);
        transform.eulerAngles = new Vector3(0,0, angle + 115);
    }
    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
 {
         Vector2 diference = vec2 - vec1;
         float sign = (vec2.y < vec1.y)? -1.0f : 1.0f;
         return Vector2.Angle(Vector2.right, diference) * sign;
     }
}
