using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public LayerMask vehiclesLayer;
    public bool canDie = false;
    private MeshRenderer[] renderers;


    void Start() {
        renderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other) {

        if (((vehiclesLayer & (1 << other.gameObject.layer)) != 0) && canDie) {
            EndMe();
        }
        
    }

    public void EndMe() {
        DisableScripts();
        //Play Animations before destroing the gameObject.
        DisableMeshRenderers();
    }

    private void DisableScripts() {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        playerMovement.enabled = false;
    }

    private void DisableMeshRenderers() {
        foreach(MeshRenderer mesh in renderers) {
            mesh.enabled = false;
        }
    }
}
