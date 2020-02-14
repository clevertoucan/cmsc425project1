using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part2EnemyController : MonoBehaviour{
    Rigidbody rb;
    public float rotateSpeed = 2f, maxEnemyPeriod = 5, minEnemyPeriod = 1;
    float startX, startY, startZ, movementPeriod;
    // Start is called before the first frame update
    void Start()
    {
        movementPeriod = Random.Range(minEnemyPeriod, maxEnemyPeriod);
        startY = transform.position.y;
        startX = transform.position.x;
        startZ = transform.position.z;
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(Random.value, Random.value, Random.value) * rotateSpeed;
    }

    private void Update()
    {
        float y = Mathf.Lerp(startY, 5, Mathf.Abs(Mathf.Sin(Time.fixedTime * movementPeriod)));
        transform.position = new Vector3(startX, y, startZ);
    }

}
