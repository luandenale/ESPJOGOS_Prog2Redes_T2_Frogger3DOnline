using UnityEngine;

public class ObstacleDetector : MonoBehaviour
{
    [SerializeField] private LayerMask _obstacleLayer;

    private RaycastHit _raycastHit;

    public bool hasObjectNORTH 
    {
        get
        {
            if (Physics.Raycast(transform.position, Vector3.forward, out _raycastHit, Mathf.Infinity, _obstacleLayer) && _raycastHit.distance < 1.0f) {
                print("North");
                return true;
            }
            return false;
        }
    }
    public bool hasObjectEAST
    {
        get
        {

            if (Physics.Raycast(transform.position, Vector3.right, out _raycastHit, Mathf.Infinity, _obstacleLayer) && _raycastHit.distance < 1.0f) {
                print("East");
                return true;
            }
            return false;
        }
    }
    public bool hasObjectWEST
    {
        get
        {
            if(Physics.Raycast(transform.position, Vector3.left, out _raycastHit, Mathf.Infinity, _obstacleLayer) && _raycastHit.distance < 1.0f) {
                print("West");
                return true;
            }
            return false;
        }
    }
    public bool hasObjectSOUTH
    {
        get
        {
            if(Physics.Raycast(transform.position, Vector3.back, out _raycastHit, Mathf.Infinity, _obstacleLayer) && _raycastHit.distance < 1.0f) {
                print("South");
                return true;
            }
            return false;
        }
    }
}
