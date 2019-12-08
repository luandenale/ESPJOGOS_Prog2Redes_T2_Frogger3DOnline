using UnityEngine;
using UnityEngine.Networking;
public class PlayerDeath : NetworkBehaviour {

    public LayerMask vehiclesLayer;

    [SyncVar]
    public bool canDie;
    private bool lastToDie = false;
    private PlayerMovement _playerMovement;
    private PlayerCharacter _playerCharacter;
    private AudioSource _playerAudioSource;
    private Collider _playerCollider;


    private  void Awake()
    {

        _playerMovement = GetComponent<PlayerMovement>();
        _playerCharacter = GetComponent<PlayerCharacter>();
        _playerAudioSource = GetComponent<AudioSource>();
        _playerCollider = GetComponent<Collider>();
        
        canDie = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (isLocalPlayer) {
            if (((vehiclesLayer & (1 << other.gameObject.layer)) != 0) && _playerMovement.alive)
                canDie = true;
        }
        if (canDie)
        {
            var car = other.GetComponent<Car>();
            ChangePosScale(other.transform.position, other.bounds.center, other.bounds.extents, car.id);

            _playerAudioSource.PlayOneShot(AudioClipReference.instance.hitSound);
        }
    }


    private void ChangePosScale(Vector3 carPos, Vector3 carBoundsCenter, Vector3 carBoundsExtends, int carId) {
        var car = Car.GetById(carId);

        _playerMovement.alive = false; //para o movimento do PlayerMovement para ele ficar no lugar

        if (GameManager.instance.GameEnded()) {
            if (isLocalPlayer) {
                lastToDie = true;
            }
        }

        // teleport
        var myBounds = _playerCollider.bounds;
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

        CmdChangePosScale(transform.position, transform.localScale, car.id, car.carryingPlayer, lastToDie);
    }

    [Command]
    public void CmdChangePosScale(Vector3 playerPos, Vector3 playerScale, int carId, bool playerCarried, bool lastToDie) {
        RpcChangePosScale(playerPos, playerScale, carId, playerCarried, lastToDie);
    }

    //have to pass the info of the car that I need to set death parameters
    [ClientRpc]
    public void RpcChangePosScale(Vector3 carPos, Vector3 playerScale,  int carId, bool playerCarried,  bool lastToDie) {
        if (!isLocalPlayer) {

            if (GameManager.instance.GameEnded()) {
                if (isLocalPlayer) {
                    lastToDie = true;
                }
            }

            var car = Car.GetById(carId);

            _playerMovement.alive = false;

            if (playerCarried) {
                car.carryingPlayer = playerCarried;
                transform.SetParent(car.gameObject.transform);
            }

            transform.position = carPos;
            transform.localScale = playerScale;

            AddPointPostMortem();

            EndMe();
            canDie = false;

        }
    }

    private void AddPointPostMortem() {
        if (isLocalPlayer) {
            if (transform.position.z > Score.playerScore.lastPos && !lastToDie) {
                Score.playerScore.UpdateText(gameObject.name, 5);
            }
        }
        else {
            if (transform.position.z > Score.enemyScore.lastPos && !lastToDie) {
                Score.enemyScore.UpdateText(gameObject.name,5);
            }
        }
    }

    private void EndMe()
    {
        if (GameManager.instance.GameEnded() && !GameManager.instance.gameEnded) {
            
            if ((Score.playerScore.points < Score.enemyScore.points))
                GameManager.instance.MatchLost();

            else if((Score.playerScore.points > Score.enemyScore.points))
                GameManager.instance.MatchWon();

            else if((Score.playerScore.points == Score.enemyScore.points) && !lastToDie) {
                GameManager.instance.MatchWon();
            }

            else if((Score.playerScore.points == Score.enemyScore.points) && lastToDie) {
                GameManager.instance.MatchLost();
            }

            _playerCharacter.CmdToMenu();
        }
    }
}
