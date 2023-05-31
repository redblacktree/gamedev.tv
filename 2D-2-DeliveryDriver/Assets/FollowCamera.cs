using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameObject car;

    void Start() {
        car = GameObject.Find("Car");
    }
    
    void LateUpdate()
    {
        Vector3 carPos = car.transform.position;
        // note that we don't change the z position of the camera
        transform.position = new Vector3(carPos.x, carPos.y, transform.position.z);        
    }
}
