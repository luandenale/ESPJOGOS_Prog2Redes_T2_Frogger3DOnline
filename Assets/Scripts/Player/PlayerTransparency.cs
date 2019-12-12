using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

public class PlayerTransparency : NetworkBehaviour
{
    public Material[] TransparentMaterialsChad;
    public Material[] TransparentMaterialsVirgin;

    private void Awake()
    {
        GameManager.instance.onGameStarts += SetPlayerTransparency;
    }

    private void SetPlayerTransparency()
    {
        if (!isLocalPlayer)
        {
            PlayerCharacter _player = GetComponent<PlayerCharacter>();
            
            Renderer __renderer = GetComponentInChildren<Renderer>(true);
            Material[] __materials = __renderer.sharedMaterials;

            if (_player.Character == Character.Chad)
            {
                for (int i = 0; i < __renderer.sharedMaterials.Length; i++)
                    __materials[i] = TransparentMaterialsChad[i];
            }
            else if (_player.Character == Character.Virgin)
            {
                for (int i = 0; i < __renderer.sharedMaterials.Length; i++)
                    __materials[i] = TransparentMaterialsVirgin[i];
            }

            __renderer.sharedMaterials = __materials;
        }
    }
}
