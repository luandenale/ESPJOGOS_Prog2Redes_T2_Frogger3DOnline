using UnityEngine.Networking;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class NetworkManagerSingleton : NetworkManager
{
    public static event Action<NetworkConnection> onServerConnect;
    public static event Action<NetworkConnection> onClientConnect;
    public static event Action<NetworkConnection> onClientDisconnect;
    public static event Action<NetworkConnection> onServerDisconnect;

    public static NetworkDiscovery Discovery
    {
        get
        {
            return singleton.GetComponent<NetworkDiscovery>();
        }
    }

    public static NetworkMatch Match
    {
        get
        {
            return singleton.GetComponent<NetworkMatch>() ?? singleton.gameObject.AddComponent<NetworkMatch>();
        }
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        if(conn.address == "localClient")
        {
            return;
        }

        Debug.Log("Client connected! Address: " + conn.address);

        if(onServerConnect != null)
        {
            onServerConnect(conn);
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn) {

        base.OnServerDisconnect(conn);

        onServerDisconnect?.Invoke(conn);

        if (!GameManager.instance.gameEnded || GameManager.instance.GameEnded()) {

            GameManager.instance.uiManager.OnDisconnectInSelection();
            GameManager.instance.disconnectScreen.SetActive(true);
            GameManager.instance.disconnectScreen.GetComponentInChildren<Text>().text = "Lost Connection With the Client\n" +
                                                                                        "Click in the 'Return to Menu' button to go back to the Main Menu";
        }

    }

    public override void OnServerError(NetworkConnection conn, int errorCode) {
        base.OnServerError(conn, errorCode);
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        if (conn.address == "localServer")
        {
            return;
        }

        Debug.Log("Connected to server! Address: " + conn.address);

        onClientConnect?.Invoke(conn);
    }


    public override void OnClientDisconnect(NetworkConnection conn) {

        base.OnClientDisconnect(conn);

        onClientDisconnect?.Invoke(conn);

        if (!GameManager.instance.gameEnded || GameManager.instance.GameEnded()) {

            GameManager.instance.uiManager.OnDisconnectInSelection();
            GameManager.instance.disconnectScreen.SetActive(true);
            GameManager.instance.disconnectScreen.GetComponentInChildren<Text>().text = "Lost Connection With the Server\n" +
                                                                            "Click in the 'Return to Menu' button to go back to the Main Menu";
        }

    }

}
