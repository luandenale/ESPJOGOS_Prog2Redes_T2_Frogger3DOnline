using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacleLayer;

    public bool hasObjectNORTH { private set; get;}
    public bool hasObjectEAST { private set; get;}
    public bool hasObjectWEST { private set; get;}
    public bool hasObjectSOUTH { private set; get;}

    private void Update() 
    {
        RaycastHit _raycastHit;

        if(Physics.Raycast(transform.position, Vector3.forward, out _raycastHit, Mathf.Infinity, _obstacleLayer) && _raycastHit.distance < 1.5f)
            hasObjectNORTH = true;
        else if(Physics.Raycast(transform.position, Vector3.right, out _raycastHit, Mathf.Infinity, _obstacleLayer) && _raycastHit.distance < 1.5f)
            hasObjectEAST = true;
        else if(Physics.Raycast(transform.position, Vector3.left, out _raycastHit, Mathf.Infinity, _obstacleLayer) && _raycastHit.distance < 1.5f)
            hasObjectWEST = true;
        else if(Physics.Raycast(transform.position, Vector3.back, out _raycastHit, Mathf.Infinity, _obstacleLayer) && _raycastHit.distance < 1.5f)
            hasObjectSOUTH = true;
        else
        {
            hasObjectNORTH = false;
            hasObjectEAST = false;
            hasObjectWEST = false;
            hasObjectSOUTH = false;
        }
    }
}
