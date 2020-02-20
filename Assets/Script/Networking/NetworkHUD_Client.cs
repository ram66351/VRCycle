using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UnityEngine.Networking
{
    [AddComponentMenu("Network/NetworkManagerHUD")]
    [RequireComponent(typeof(NetworkManager))]
    public class NetworkHUD_Client : MonoBehaviour
    {
        public NetworkManager manager;
        [SerializeField] public bool showGUI = true;
        public InputField input_ip;
        public InputField input_port;
        public GameObject Canvas_obj;
        // Start is called before the first frame update
        void Start()
        {
            manager = GetComponent<NetworkManager>();
            input_ip.text = "172.17.2.199";
            input_port.text = "7777";
        }

        // Update is called once per frame
        void Update()
        {
            if (!NetworkClient.active)
            {
                Canvas_obj.SetActive(true);
            }           
            else
            {
                Canvas_obj.SetActive(false);
            }             
        }

        public void StartClient()
        {
            manager.networkAddress = input_ip.text;
            manager.networkPort = int.Parse(input_port.text);
            manager.StartClient();
        }
    }
}
