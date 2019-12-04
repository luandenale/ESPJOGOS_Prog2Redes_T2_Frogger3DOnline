using UnityEngine;
using UnityEngine.Networking;
public class PlayerDeath : NetworkBehaviour {

    public LayerMask vehiclesLayer;

    [SyncVar]
    public bool canDie;

    // NOT USING FOR NOW
    // private MeshRenderer[] renderers;

    private void Start()
    {
        // // NOT USING FOR NOW
        // renderers = GetComponentsInChildren<MeshRenderer>(true);
        canDie = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isLocalPlayer)
        {
            if (((vehiclesLayer & (1 << other.gameObject.layer)) != 0) && GetComponent<PlayerMovement>().alive)
                canDie = true;
        }
        if (canDie)
        {
            var car = other.GetComponent<Car>();
            CmdChangePosScale(other.transform.position, other.bounds.center, other.bounds.extents, car.id);
        }
    }

    [Command]
    public void CmdChangePosScale(Vector3 carPos, Vector3 carBoundsCenter, Vector3 carBoundsExtends, int carId)
    {
        RpcChangePosScale(carPos, carBoundsCenter, carBoundsExtends, carId);
    }

    //have to pass the info of the car that I need to set death parameters
    [ClientRpc]
    public void RpcChangePosScale(Vector3 carPos, Vector3 carBoundsCenter, Vector3 carBoundsExtends, int carId)
    {

        var car = Car.GetById(carId);
        
        GetComponent<PlayerMovement>().alive = false; //para o movimento do PlayerMovement para ele ficar no lugar

        // teleport
        var myBounds = GetComponent<Collider>().bounds;
        Vector3 otherCenterToMyCenter = transform.position - carPos;

        float xDist = Mathf.Abs(otherCenterToMyCenter.x);
        float zDist = Mathf.Abs(otherCenterToMyCenter.z);

        Vector3 idealDist = carBoundsExtends + myBounds.extents;
        Vector3 myPos = transform.position;

        myPos.y = 0.1f;
       
        if (xDist > zDist)
        {
            float moveDirection = Mathf.Sign(otherCenterToMyCenter.x);
            myPos.x = carBoundsCenter.x + idealDist.x * moveDirection;

            // me achatar no Y
            const float scaleFactor = 0.05f;

            Vector3 myScale = transform.localScale;
            myScale.y *= scaleFactor;
            transform.localScale = myScale;

            //myPos.y -= (1 - scaleFactor) * myBounds.extents.y;
            myPos.y = 0.1f;

        }
        else
        {
            float moveDirection = Mathf.Sign(otherCenterToMyCenter.z);
            myPos.z = carBoundsCenter.z + idealDist.z * moveDirection;

            // me achatar no Z
            Vector3 myScale = transform.localScale;
            myScale.z *= 0.15f;
            transform.localScale = myScale;

            transform.SetParent(car.gameObject.transform);
        }

        transform.position = myPos;

        EndMe();
        canDie = false;
    }

    private void EndMe()
    {
        DisableScripts();
    }

    private void DisableScripts()
    {
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        playerMovement.enabled = false;
    }

    // NOT USING FOR NOW
    // private void DisableMeshRenderers()
    // {
    //     foreach (MeshRenderer mesh in renderers)
    //         mesh.enabled = false;
    // }
}
