using UnityEngine;

public class AutoRotateModel : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed = 10f;
    
    private void Update()
    {
        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
    }
}
