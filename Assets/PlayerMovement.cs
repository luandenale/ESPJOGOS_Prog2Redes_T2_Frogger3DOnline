using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float _moveSpeed = 10f;
    private Vector3 _endPosition;
    private float _standardHeight = 1f;
    private float _doubleOfActualJumpHeight = 2f;

    private void Start()
    {
        _endPosition = transform.position;
    }
    
    private void Update()
    {
        transform.position = Vector3.LerpUnclamped(transform.position, _endPosition, Time.deltaTime *_moveSpeed);
        
        if(Input.GetKeyDown(KeyCode.UpArrow))
            _endPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        
        if(transform.position.z < _endPosition.z - 0.5f)
            _endPosition = new Vector3(_endPosition.x, _doubleOfActualJumpHeight, _endPosition.z);
        else
            _endPosition = new Vector3(_endPosition.x, _standardHeight, _endPosition.z);

    }
}
