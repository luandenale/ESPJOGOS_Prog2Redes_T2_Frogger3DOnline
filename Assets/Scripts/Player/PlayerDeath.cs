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
            
            GetComponent<PlayerMovement>().alive = false; //para o movimento do PlayerMovement para ele ficar no lugar
            transform.SetParent(other.gameObject.transform);
            EndMe();

        }
        

    }

    public void EndMe() {
        DisableScripts();
        //Play Animations before destroing the gameObject.
        //DisableMeshRenderers(); Todo Retirado por enquanto para mecher com o movemento pos morte do personagem

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
