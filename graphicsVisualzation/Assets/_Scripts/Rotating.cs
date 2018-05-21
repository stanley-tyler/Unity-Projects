using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotatingAxis {
   x, y, z
}
public class Rotating : MonoBehaviour {
    public float rotationSpeed = 5.0f;
    public RotatingAxis axis = RotatingAxis.y;

    // Use this for initialization
    void Start () {
        
    }

    void Update() {
        switch (axis)
        {
            case RotatingAxis.x:
                transform.Rotate(rotationSpeed*Time.deltaTime,0,0);
                break;
            case RotatingAxis.y:
                transform.Rotate(0,rotationSpeed*Time.deltaTime,0);
                break;
            case RotatingAxis.z:
                transform.Rotate(0,0,rotationSpeed*Time.deltaTime);
                break;
            default:
                break;
        }
    }
}
