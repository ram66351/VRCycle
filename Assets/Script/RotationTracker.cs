using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTracker : MonoBehaviour
{
    //First we'll need a couple of variables to do the calculation.
    Vector3 rotationLast; //The value of the rotation at the previous update
    public Vector3 rotationDelta; //The difference in rotation between now and the previous update

    public Vector3 localRot;

    //Initialize rotationLast in start, or it will cause an error
    void Start()
    {
        rotationLast = transform.rotation.eulerAngles;
    }

    void Update()
    {
        //Update both variables, so they're accurate every frame.
        rotationDelta = transform.rotation.eulerAngles - rotationLast;
        rotationLast = transform.rotation.eulerAngles;

        localRot = transform.eulerAngles;
        
    }

    //Ta daa!
    public Vector3 angularVelocity
    {
        get
        {
            return rotationDelta;
        }
    }
}
