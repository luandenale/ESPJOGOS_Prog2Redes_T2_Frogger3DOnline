using UnityEngine;

public enum PlayerDirection
{
    NORTH,
    EAST,
    SOUTH,
    WEST
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 100f;
    private Vector3 _endPosition;
    private Quaternion _endRotation;
    private bool _shouldRotate = false;
    private float _standardHeight = 1f;
    private float _doubleOfActualJumpHeight = 2f;

    private PlayerDirection _direction;
    private ObstacleDetector _obstacleDetector;

    public bool alive;
    private void Awake()
    {
        _obstacleDetector = GetComponent<ObstacleDetector>();
        _direction = PlayerDirection.NORTH;
        _endPosition = transform.position;
        _endRotation = transform.rotation;
        alive = true;
    }
    
    private void Update()
    {
        if (alive) {

                if(Vector3.Distance(transform.position, _endPosition) < 0.1f)
                {
                    transform.position = _endPosition;
                    transform.rotation = _endRotation;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, _endPosition, Time.deltaTime * _moveSpeed);
                    transform.rotation = Quaternion.Lerp(transform.rotation, _endRotation, Time.deltaTime * _moveSpeed);
                }
        
                // Check if can move
                if(transform.position.y == 1)
                {
                    DoMove();
                }

                HandleJump();
            }

        }

    private void DoMove()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && !_obstacleDetector.hasObjectNORTH)
        {
            SetRotation(PlayerDirection.NORTH);
            _endPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow) && !_obstacleDetector.hasObjectWEST && transform.position.x > -10f)
        {
            SetRotation(PlayerDirection.WEST);
            _endPosition = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && !_obstacleDetector.hasObjectSOUTH && transform.position.z > 0f)
        {
            SetRotation(PlayerDirection.SOUTH);
            _endPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && !_obstacleDetector.hasObjectEAST && transform.position.x < 10f)
        {
            SetRotation(PlayerDirection.EAST);
            _endPosition = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
        }
    }

    private void HandleJump()
    {
        if((_direction == PlayerDirection.NORTH && transform.position.z < _endPosition.z - 0.5f) ||
            (_direction == PlayerDirection.EAST && transform.position.x < _endPosition.x - 0.5f) ||
            (_direction == PlayerDirection.WEST && transform.position.x > _endPosition.x + 0.5f) ||
            (_direction == PlayerDirection.SOUTH && transform.position.z > _endPosition.z + 0.5f))
            _endPosition = new Vector3(_endPosition.x, _doubleOfActualJumpHeight, _endPosition.z);
        else
            _endPosition = new Vector3(_endPosition.x, _standardHeight, _endPosition.z);
    }

    private void SetRotation(PlayerDirection p_targetDirection)
    {
        Vector3 __forwardRotation = new Vector3(0,0,0);
        Vector3 __upwardRotation = new Vector3(0,0,0);
        switch(p_targetDirection)
        {
            case PlayerDirection.NORTH:
                __forwardRotation = new Vector3(0,0,1);
                break;
            case PlayerDirection.EAST:
                __forwardRotation = new Vector3(1,0,0);
                break;
            case PlayerDirection.SOUTH:
                __forwardRotation = new Vector3(0,0,-1);
                __upwardRotation = new Vector3(0,1,0);
                break;
            case PlayerDirection.WEST:
                __forwardRotation = new Vector3(-1,0,0);
                break;
        }
        
        _direction = p_targetDirection;

        _endRotation = Quaternion.LookRotation(__forwardRotation, __upwardRotation);
    }
}
