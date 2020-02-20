using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SensorValueTransmitter : NetworkBehaviour
{

    [SyncVar(hook = "SendGyro")]
    public Vector3 rotationRate;

    [SyncVar(hook = "SendFloat")]
    public float syncText;

    Gyroscope m_Gyro;

    // Start is called before the first frame update

    void OnGUI()
    {
        
    }

    // Use this for initialization
    void Start()
    {
     
        if (isLocalPlayer)
        {
            m_Gyro = Input.gyro;
            m_Gyro.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            //CmdChangeName(pname);
            rotationRate = Input.gyro.rotationRate;
            
            CmdSendRotationRate(rotationRate);

            syncText += 1f;
            CmdChangeTextFloat(syncText);
            Debug.Log("running in !");
        }
    }

    

    [Command]
    public void CmdSendRotationRate(Vector3 v)
    {
        rotationRate = v;
    }

    public void SendGyro(Vector3 rr)
    {
        rotationRate = rr;
    }

    public void SendFloat(float f)
    {
        syncText = f;
    }

    [Command]
    public void CmdChangeTextFloat(float f)
    {
        syncText = f;
    }

    [ClientRpc]
    public void RpcChangeName(string v)
    {
        Debug.Log("Changing val " + v);
        //val = v;
    }
}
