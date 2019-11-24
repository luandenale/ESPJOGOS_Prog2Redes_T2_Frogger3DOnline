﻿using UnityEngine;
using UnityEngine.Networking;
public class PlayerDeath : NetworkBehaviour
{
    public LayerMask vehiclesLayer, groundLayer;
    [SyncVar]
    public bool canDie;

    private MeshRenderer[] renderers;


    void Start() {
        renderers = GetComponentsInChildren<MeshRenderer>();
        canDie = true;
    }

    void Update() {
        
    }

    private void OnTriggerEnter(Collider other) {

        if (isLocalPlayer){

            if (((vehiclesLayer & (1 << other.gameObject.layer)) != 0) && canDie && GetComponent<PlayerMovement>().alive) {
                ChangePosScale(other.gameObject);

            }

        }

    }

    public void ChangePosScale(GameObject carObj) {

        Collider other = carObj.GetComponent<Collider>();
        GetComponent<PlayerMovement>().alive = false; //para o movimento do PlayerMovement para ele ficar no lugar

        // teleport
        var otherBounds = other.bounds;
        var myBounds = GetComponent<Collider>().bounds;
        Vector3 otherCenterToMyCenter = transform.position - other.transform.position;

        float xDist = Mathf.Abs(otherCenterToMyCenter.x);
        float zDist = Mathf.Abs(otherCenterToMyCenter.z);

        Vector3 idealDist = otherBounds.extents + myBounds.extents;
        Vector3 myPos = transform.position;

        if (xDist > zDist) {
            float moveDirection = Mathf.Sign(otherCenterToMyCenter.x);
            myPos.x = otherBounds.center.x + idealDist.x * moveDirection;

            // me achatar no Y
            const float scaleFactor = 0.05f;

            Vector3 myScale = transform.localScale;
            myScale.y *= scaleFactor;
            transform.localScale = myScale;

            //myPos.y -= (1 - scaleFactor) * myBounds.extents.y;
            myPos.y = 0.1f; //TODO verificar outra solução com o bruno

        }
        else {
            float moveDirection = Mathf.Sign(otherCenterToMyCenter.z);
            myPos.z = otherBounds.center.z + idealDist.z * moveDirection;

            // me achatar no Z
            Vector3 myScale = transform.localScale;
            myScale.z *= 0.15f;
            transform.localScale = myScale;

            transform.SetParent(other.gameObject.transform);
        }

        transform.position = myPos;

        EndMe();

        canDie = false;
    }

    [Command]
    private void CmdSetCanDie() {

    }

    private void EndMe() {
        DisableScripts();
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
