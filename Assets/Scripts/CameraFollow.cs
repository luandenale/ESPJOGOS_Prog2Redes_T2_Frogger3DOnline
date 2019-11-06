using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    [SerializeField] private GameObject _player;


    private void LateUpdate () 
    {
        transform.position = new Vector3(_player.transform.position.x + 36, transform.position.y, _player.transform.position.z - 30);
    }
}