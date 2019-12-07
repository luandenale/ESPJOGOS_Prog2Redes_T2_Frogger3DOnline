using UnityEngine;
using UnityEngine.Networking;
public class PlayerDeath : NetworkBehaviour {

    public LayerMask vehiclesLayer;

    [SyncVar]
    public bool canDie;
    private bool lastToDie = false;
    private PlayerMovement playerMovement;
    // NOT USING FOR NOW
    // private MeshRenderer[] renderers;

    private void Start() {
        // // NOT USING FOR NOW
        // renderers = GetComponentsInChildren<MeshRenderer>(true);
        playerMovement = GetComponent<PlayerMovement>();
        canDie = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (isLocalPlayer) {
            if (((vehiclesLayer & (1 << other.gameObject.layer)) != 0) && playerMovement.alive)
                canDie = true;
        }
        if (canDie) {
            var car = other.GetComponent<Car>();
            CmdChangePosScale(other.transform.position, other.bounds.center, other.bounds.extents, car.id);
        }
    }

    [Command]
    public void CmdChangePosScale(Vector3 carPos, Vector3 carBoundsCenter, Vector3 carBoundsExtends, int carId) {
        RpcChangePosScale(carPos, carBoundsCenter, carBoundsExtends, carId);
    }

    //have to pass the info of the car that I need to set death parameters
    [ClientRpc]
    public void RpcChangePosScale(Vector3 carPos, Vector3 carBoundsCenter, Vector3 carBoundsExtends, int carId) {
        var car = Car.GetById(carId);

        playerMovement.alive = false; //para o movimento do PlayerMovement para ele ficar no lugar

        if (GameManager.instance.GameEnded()) {
            lastToDie = true;
        }

        // teleport
        var myBounds = GetComponent<Collider>().bounds;
        Vector3 otherCenterToMyCenter = transform.position - carPos;

        float xDist = Mathf.Abs(otherCenterToMyCenter.x);
        float zDist = Mathf.Abs(otherCenterToMyCenter.z);

        Vector3 idealDist = carBoundsExtends + myBounds.extents;
        Vector3 myPos = transform.position;

        myPos.y = 0.1f;

        if (xDist > zDist) {
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
        else {
            float moveDirection = Mathf.Sign(otherCenterToMyCenter.z);
            myPos.z = carBoundsCenter.z + idealDist.z * moveDirection;

            // me achatar no Z
            Vector3 myScale = transform.localScale;
            myScale.z *= 0.15f;
            transform.localScale = myScale;

            transform.SetParent(car.gameObject.transform);

            car.carryingPlayer = true; //avisa para a instancia do carro que atropelou o player 
                                       //(para que quando o carro seja destruido ele não destrua o player)
        }

        AddPointPostMortem();

        transform.position = myPos;

        EndMe();
        canDie = false;
    }

    private void AddPointPostMortem() {
        if (isLocalPlayer) {
            if (transform.position.z > Score.playerScore.lastPos && !lastToDie) {
                Score.playerScore.UpdateText(5);
            }
        }
        else {
            if (transform.position.z > Score.enemyScore.lastPos && !lastToDie) {
                Score.enemyScore.UpdateText(5);
            }
        }
    }

    private void EndMe()
    {
        if (GameManager.instance.GameEnded() && !GameManager.instance.gameEnded) {
            
            if ((Score.playerScore.points < Score.enemyScore.points))
                GameManager.instance.MatchLost();

            else
                GameManager.instance.MatchWon();

            GetComponent<PlayerCharacter>().CmdToMenu();
        }
    }

    // NOT USING FOR NOW
    // private void DisableMeshRenderers()
    // {
    //     foreach (MeshRenderer mesh in renderers)
    //         mesh.enabled = false;
    // }
}
