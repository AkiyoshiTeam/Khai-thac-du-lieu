using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connect : MonoBehaviour
{

    public string ip = "192.168.43.1";
    public int port = 25001;

    // Use this for initialization
    void OnGUI()
    {
        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            if (GUI.Button(new Rect(10, 20, 100, 25), "Start Client"))
            {
                Network.Connect(ip, port);
            }
            if (GUI.Button(new Rect(10, 50, 100, 25), "Start Server"))
            {
                Network.InitializeServer(10, port);
            }
        }
        else
        {
            if (Network.peerType == NetworkPeerType.Client)
            {
                GUI.Label(new Rect(10, 20, 100, 25), "Client");
                if (GUI.Button(new Rect(10, 50, 100, 25), "Logout"))
                {
                    Network.Disconnect(250);
                }
            }
            if (Network.peerType == NetworkPeerType.Server)
            {
                GUI.Label(new Rect(10, 20, 100, 25), "Server");
                GUI.Label(new Rect(10, 50, 100, 25), "Connections: "+Network.connections.Length);
                if (GUI.Button(new Rect(10, 80, 100, 25), "Logout"))
                {
                    Network.Disconnect(250);
                }
            }
        }
    }

}
