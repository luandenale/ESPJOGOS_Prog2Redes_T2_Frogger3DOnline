using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerInstance : NetworkBehaviour 
{

    private void Start()
    {
        if (isServer) {
            //GameManager.RegisterPlayer(this);
        }
    }

}
