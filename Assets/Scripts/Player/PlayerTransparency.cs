using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class PlayerTransparency : NetworkBehaviour
{
    public Material[] TransparentMaterials;

    private void Start()
    {
        if (!isLocalPlayer) {
            var renderer = GetComponentInChildren<Renderer>();
            var materials = renderer.sharedMaterials;
            for (int i = 0; i < renderer.sharedMaterials.Length; i++) 
                materials[i] = TransparentMaterials[i];

            renderer.sharedMaterials = materials;
        }
    }

}
