using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _vehiclePrefab;
    public int maxTimeSpawn = 5;
    private float timeToSpawn = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToSpawn -= Time.fixedDeltaTime;

        if (timeToSpawn < 0) {
            timeToSpawn = Random.Range(1f, maxTimeSpawn);
            CreateCar();
        }

    }

    public void CreateCar() {
        GameObject __vehicle = Instantiate(_vehiclePrefab, transform.position, Quaternion.identity, transform);
    }
}
