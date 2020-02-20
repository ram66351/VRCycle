using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, -(RoadSpawnManager.speed / RoadSpawnManager.speedFactor) * Time.deltaTime); 
        if(Mathf.Abs(transform.position.z) > RoadSpawnManager.destroyDistance)
        {
            DestroyImmediate(gameObject);
        }
    }
}
