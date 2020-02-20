using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadSpawnManager : MonoBehaviour
{
    public static float speed;
    public static float speedFactor = 50;
    public static float destroyDistance ;
    public int rpm = 20;
    public float radius = 1.5f; //in feet
    private float circumference; 
    public float roadOffset = 10;
    public int noOfRoadInstances;
    public GameObject roadPrefab;
    public GameObject lastRoad;
    public GameObject firstRoad;
    public float visibleRoadLength;
    public bool bool_createRoadInstance = false;
    public float lengthPerRoad;
    public float roadOverlapFactor = 0.0f;
    public float roadLimit;
    public Vector3 rotationRate;
    public float cycleSpeedScaleFactor = 2;
    public Text labelRPM;
    // Start is called before the first frame update
    void Start()
    {
        destroyDistance = noOfRoadInstances * roadOffset;
        circumference = 2 * 3.14f * radius;
        for(int i=0; i<noOfRoadInstances; i++)
        {
            Debug.Log(noOfRoadInstances +" : "+ noOfRoadInstances);
            GameObject roadClone = Instantiate(roadPrefab, new Vector3(0, 0, i * roadOffset), Quaternion.identity) as GameObject;
            if(i == 0)
            {
                firstRoad = roadClone;
            }
            if (i == noOfRoadInstances -1)
            {
                lastRoad = roadClone;
            }
        }

        lastRoad.GetComponent<Renderer>().material.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        speed = circumference * (rotationRate.z) * cycleSpeedScaleFactor; //feet per min //
        roadLimit = lengthPerRoad * (noOfRoadInstances - 1);
        if (lastRoad != null && lastRoad.transform.position.z < roadLimit - roadOffset)
        {
            bool_createRoadInstance = true;
        }

        if(bool_createRoadInstance)
        {
            GameObject roadClone = Instantiate(roadPrefab, new Vector3(0, 0, (noOfRoadInstances-1) * roadOffset), Quaternion.identity) as GameObject;
            lastRoad.GetComponent<Renderer>().material.color = Color.white;
            lastRoad = roadClone;
            lastRoad.GetComponent<Renderer>().material.color = Color.green;
            bool_createRoadInstance = false;
        }

        if(GameObject.FindGameObjectWithTag("gyro"))
        {
            rotationRate = GameObject.FindGameObjectWithTag("gyro").GetComponent<SensorValueTransmitter>().rotationRate;
            labelRPM.text = "RPM : "+rotationRate.z;
        }
    }

    public void OnSiderValueChanged(float val)
    {
        cycleSpeedScaleFactor = val;
    }
}
