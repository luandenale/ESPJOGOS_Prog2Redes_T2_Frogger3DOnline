using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class PlayerTransparency : NetworkBehaviour
{
    public Material[] TransparentMaterialsChad;
    public Material[] TransparentMaterialsVirgin;

    private void Start()
    {
        if (!isLocalPlayer) {
            print("GotCalled");
            PlayerCharacter player = GetComponent<PlayerCharacter>();
            
            var renderer = GetComponentInChildren<Renderer>();
            var materials = renderer.sharedMaterials;

            if (player.character == Character.Chad) {
                for (int i = 0; i < renderer.sharedMaterials.Length; i++)
                    materials[i] = TransparentMaterialsChad[i];
            }
            else if (player.character == Character.Virgin) {
                for (int i = 0; i < renderer.sharedMaterials.Length; i++)
                    materials[i] = TransparentMaterialsVirgin[i];
            }

            renderer.sharedMaterials = materials;
        }
    }

}
