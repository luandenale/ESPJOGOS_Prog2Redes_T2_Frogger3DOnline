using UnityEngine;
using UnityEngine.Networking;

public class PlayerDeath : NetworkBehaviour
{
    [SerializeField]
    private LayerMask _vehiclesLayer;

    [SyncVar]
    private bool _canDie;

    private const float _scaleFactor = 0.05f;
    private bool _lastToDie;
    
    private PlayerCharacter _playerCharacter;
    private AudioSource _playerAudioSource;
    private Collider _playerCollider;

    private  void Awake()
    {
        _playerCharacter = GetComponent<PlayerCharacter>();
        _playerAudioSource = GetComponent<AudioSource>();
        _playerCollider = GetComponent<Collider>();
        _lastToDie = false;
        
        _canDie = false;
        _playerCharacter.playerAlive = true;
    }

    private void OnTriggerEnter(Collider p_other)
    {
        if (isLocalPlayer)
        {
            if (((_vehiclesLayer & (1 << p_other.gameObject.layer)) != 0) && _playerCharacter.playerAlive)
                _canDie = true;
        }

        if (_canDie)
        {
            int __id;
            
            if (p_other.GetComponent<Car>())
                __id = p_other.GetComponent<Car>().id;
            else
                __id = p_other.GetComponent<Train>().id;

            ChangePosScale(p_other.transform.position, p_other.bounds.center, p_other.bounds.extents, __id);
            _playerAudioSource.PlayOneShot(AudioClipReference.instance.hitSound);
        }
    }

#region FlattenPlayer
    [Command]
    public void CmdChangePosScale(Vector3 p_playerPos, Vector3 p_playerScale, int p_carId, bool p_playerCarried)
    {
        RpcChangePosScale(p_playerPos, p_playerScale, p_carId, p_playerCarried);
    }

    //have to pass the info of the car that I need to set death parameters
    [ClientRpc]
    public void RpcChangePosScale(Vector3 p_carPos, Vector3 p_playerScale, int p_carId, bool p_playerCarried)
    {
        if (!isLocalPlayer)
        {
            _playerCharacter.playerAlive = false;

            if (GameManager.instance.GameEnded())
                _lastToDie = true;

            Vehicle __car = Vehicle.GetById(p_carId);


            if (p_playerCarried)
            {
                __car.carryingPlayer = p_playerCarried;
                transform.SetParent(__car.gameObject.transform);
            }

            transform.position = p_carPos;
            transform.localScale = p_playerScale;

            AddTieBreakPointPostMortem();

            UnloadPlayer();
            
            _canDie = false;
        }
    }
    private void ChangePosScale(Vector3 p_carPos, Vector3 p_vehicleBoundsCenter, Vector3 p_vehicleBoundsExtends, int p_carId)
    {

        Vehicle __vehicle = Vehicle.GetById(p_carId);

        _playerCharacter.playerAlive = false; //para o movimento do PlayerMovement para ele ficar no lugar

        if (GameManager.instance.GameEnded())
            _lastToDie = true;         


        // teleport
        Bounds __myBounds = _playerCollider.bounds;
        Vector3 __otherCenterToMyCenter = transform.position - p_carPos;

        float __xDist = Mathf.Abs(__otherCenterToMyCenter.x);
        float __zDist = Mathf.Abs(__otherCenterToMyCenter.z);

        Vector3 __idealDist = p_vehicleBoundsExtends + __myBounds.extents;
        Vector3 __myPos = transform.position;

        if (__xDist > __zDist)
        {
            float __moveDirection = Mathf.Sign(__otherCenterToMyCenter.x);
            
            __myPos.x = p_vehicleBoundsCenter.x + __idealDist.x * __moveDirection;

            // me achatar no Y

            Vector3 __myScale = transform.localScale;
            __myScale.y *= _scaleFactor;
            transform.localScale = __myScale;

            __myPos.y = 0.1f;
        }
        else
        {
            float __moveDirection = Mathf.Sign(__otherCenterToMyCenter.z);
           
            __myPos.z = p_vehicleBoundsCenter.z + __idealDist.z * __moveDirection;

            // me achatar no Z
            Vector3 __myScale = transform.localScale;
            __myScale.z *= 0.15f;
            transform.localScale = __myScale;

            transform.SetParent(__vehicle.gameObject.transform);

            __vehicle.carryingPlayer = true; //avisa para a instancia do carro que atropelou o player 
                                       //(para que quando o carro seja destruido ele não destrua o player)
        }

        AddTieBreakPointPostMortem();

        transform.position = __myPos;

        UnloadPlayer();
        _canDie = false;

        CmdChangePosScale(transform.position, transform.localScale, __vehicle.id, __vehicle.carryingPlayer);
    }
#endregion

    private void AddTieBreakPointPostMortem()
    {
        if (isLocalPlayer)
        {
            if (transform.position.z > Score.playerScore.lastPos && !_lastToDie)
                Score.playerScore.UpdateText(gameObject.name, 5);
        }
        else
        {
            if (transform.position.z > Score.enemyScore.lastPos && !_lastToDie)
                Score.enemyScore.UpdateText(gameObject.name,5);
        }
    }

    private void UnloadPlayer()
    {
        if (GameManager.instance.GameEnded() && !GameManager.instance.gameEnded)
        {
            if ((Score.playerScore.points < Score.enemyScore.points))
                GameManager.instance.MatchLost();

            else if((Score.playerScore.points > Score.enemyScore.points))
                GameManager.instance.MatchWon();

            else if((Score.playerScore.points == Score.enemyScore.points) && !isLocalPlayer)
                GameManager.instance.MatchWon();

            else if((Score.playerScore.points == Score.enemyScore.points) && isLocalPlayer)
                GameManager.instance.MatchLost();

            _playerCharacter.CmdToMenu();
        }
    }
}
