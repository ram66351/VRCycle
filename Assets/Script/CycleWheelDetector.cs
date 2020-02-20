using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleWheelDetector : MonoBehaviour
{
    // Start is called before the first frame update
    Gyroscope m_Gyro;
    public float angle;
    public float w;
    public WheelCollider wc;
    public float rpm;
    public float cummulativeAngle;
    public float distanceCovered;

    public Vector3 rotationRate;
    public enum MoveDirection
    {
        forward, backward
    }

    public MoveDirection moveDirection;

    public Vector3 Acceleration;
    void Start()
    {
        //Set up and enable the gyroscope (check your device has one)
        m_Gyro = Input.gyro;
        m_Gyro.enabled = true;
        
       // StartCoroutine(DeltaDiff(transform.eulerAngles.x));
    }

    // Update is called once per frame
    void Update()
    {
        float val = Input.gyro.attitude.z;
        w = Input.gyro.attitude.w;
        transform.rotation = new Quaternion(val, 0, 0, Input.gyro.attitude.w);
        angle =  transform.rotation.x;//Clamp0360(transform.eulerAngles.x);
        Acceleration = Input.acceleration;
        rotationRate = Input.gyro.rotationRate;
    }

    IEnumerator DeltaDiff(float angle1)
    {
        yield return new WaitForEndOfFrame();
        float angle2 = transform.eulerAngles.x;
        float diffAngel = angle2 - angle1;
        if (diffAngel > 0)
            moveDirection = MoveDirection.forward;
        else
            moveDirection = MoveDirection.backward;

        cummulativeAngle += diffAngel;
        StartCoroutine(DeltaDiff(transform.eulerAngles.x));
    }

    public float Clamp0360(float eulerAngles)
    {
        float result = eulerAngles - Mathf.CeilToInt(eulerAngles / 360f) * 360f;
        if (result < 0)
        {
            result += 360f;
        }
        return result;
    }

    public  float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 360)
            return angle - 360;
        return angle;
    }
}
