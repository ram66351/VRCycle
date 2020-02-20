using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[AddComponentMenu("Network/NetworkManagerHUD")]
[RequireComponent(typeof(NetworkManager))]
public class UnetHUD_Server : MonoBehaviour
{
    public NetworkManager manager;
    public GameObject serverConfigUI;
    public Text ServerIp_Label;
    public InputField port;
    public Image statusImage;
    void Start()
    {
        manager = GetComponent<NetworkManager>();
        NetworkServer.Reset();
        ServerIp_Label.text = IPManager.GetLocalIPAddress();
        port.text = NetworkManager.singleton.networkPort + "";
    }

    void Update()
    {
        if (!NetworkServer.active)
        {
            serverConfigUI.SetActive(true);
            statusImage.color = Color.red;
        }
        else
        {
            serverConfigUI.SetActive(false);
            statusImage.color = Color.green;
        }
    }

    public void ResetServer()
    {
        NetworkServer.Reset();
    }

    public void StartServer()
    {
        NetworkServer.Reset();
        manager.networkPort = int.Parse(port.text);
        manager.StartServer();
    }
}
